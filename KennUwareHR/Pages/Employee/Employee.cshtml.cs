using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KennUwareHR.Contexts;
using System.Net.Http;
using System.Text;
using KennUwareHR.Pages_Account;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;

namespace KennUwareHR.Pages.Employee
{
    public class EmployeePageModel : PageModel
    {
        public SelectList RegionNameSL { get; set; }

        public void PopulateRegionDropDownList(HRContext _context,
            object selectedRegion = null)
        {
            var RegionsQuery = from r in _context.Regions 
                                                 orderby r.Name // Sort by name.
                                                 select r;

            RegionNameSL = new SelectList(RegionsQuery.AsNoTracking(),
                                          "Id", "Name", selectedRegion);
        }

        public SelectList RoleNameSL { get; set; }

        public void PopulateRoleDropDownList(HRContext _context,
            object selectedRole = null)
        {
            var RoleQuery = from r in _context.Roles 
                                              orderby r.Title // Sort by name.
                                              select r;

            RoleNameSL = new SelectList(RoleQuery.AsNoTracking(),
                                        "Id", "Title", selectedRole);
        }

        public SelectList PositionNameSL { get; set; }

        public void PopulatePositionDropDownList(HRContext _context,
            object selectedDepartment = null)
        {
            var PositionQuery = from r in _context.Positions
                                                  orderby r.Title // Sort by name.
                                                  select r;

            PositionNameSL = new SelectList(PositionQuery.AsNoTracking(),
                                            "Id", "Title", selectedDepartment);
        }

        public SelectList DepartmentNameSL { get; set; }

        public void PopulateDepartmentDropDownList(HRContext _context,
            object selectedDepartment = null)
        {
            var DepartmentQuery = from r in _context.Departments
                                                    orderby r.Name // Sort by name.
                                                    select r;

            DepartmentNameSL = new SelectList(DepartmentQuery.AsNoTracking(),
                                              "Id", "Name", selectedDepartment);
        }


        public SelectList ManagerNameSL { get; set; }
        public void PopulateManagerDropDownList(HRContext _context,
            object selectedEmployee = null)
        {
            var ManagerQuery = from r in _context.Employees
                                                    orderby r.LastName // Sort by name.
                                                    select r;

            ManagerNameSL = new SelectList((from r in ManagerQuery.ToList()
                                                     select new
                                                     {
                                                         Id = r.Id,
                                                         FullName = r.LastName + ", " + r.FirstName
                                                     }), "Id", "FullName", selectedEmployee);
        }


        public async Task<NewUser> CreateUser(String userName, String password)
        {

            HttpClient client = new HttpClient();

            var content = new StringContent("{\"username\":\"" + userName + "\",\"password\": \"" + 
            password + "\", \"type\": \"employee\"}",
                                    Encoding.UTF8, 
                                    "application/json");

            var response = await client.PostAsync("https://api-gateway-343.herokuapp.com/auth/create", content);

            var responseString = await response.Content.ReadAsStringAsync();

            NewUser user;
            Response resp;

            try {
                resp = JsonConvert.DeserializeObject<Response>(responseString);
                
            } catch (JsonReaderException) 
            {
                return new NewUser { Id = 0, UserName = "", Password = "" };
            }

            var jwt = resp.Token;

            string[] parts = jwt.Split(".".ToCharArray());
            var header = parts[0];
            var payload = parts[1];
            var signature = parts[2];//Base64UrlEncoded signature from the token

            byte[] bytesToSign = Encoding.UTF8.GetBytes(string.Join(".", header, payload));

            byte[] secret = Encoding.UTF8.GetBytes("secretkey");

            var alg = new HMACSHA256(secret);
            var hash = alg.ComputeHash(bytesToSign);

            var computedSignature = Base64UrlEncode(hash);


            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadToken(jwt) as JwtSecurityToken;

            var userId = token.Claims.First(claim => claim.Type == "id").Value;

            user = new NewUser
                    {
                        Id = Convert.ToInt32(userId),
                        UserName = userName,
                        Password = password,
                    };
            return user;
        }

        private static string Base64UrlEncode(byte[] input) {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }

    }

    public class NewUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
