using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Cryptography;
using KennUwareHR.Utilities;
using KennUwareHR.Models;

namespace KennUwareHR.Pages_Account
{
    public class LoginModel : PageModel
    {

        [Required]
        [BindProperty]
        public string Username { get; set; }

        [Required]
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Error { get; set; }
        
        public string Jwt { get; set; }

        public Dictionary<string, string> stuff { get; set; }

        private readonly KennUwareHR.Contexts.HRContext _context;

        public LoginModel(KennUwareHR.Contexts.HRContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string returnURL = "/index") 
        {
            if (!ModelState.IsValid) return Page();

            var values = new Dictionary<string, string>
            {
                { "username", Username },
                { "password", Password }
            };

            HttpClient client = new HttpClient();

            var content = new StringContent("{\"username\":\"" + Username + "\",\"password\": \"" + Password + "\"}",
                                    Encoding.UTF8, 
                                    "application/json");

            var response = await client.PostAsync("https://api-gateway-343.herokuapp.com/auth/login", content);

            var responseString = await response.Content.ReadAsStringAsync();
            
            try {
                Response resp = JsonConvert.DeserializeObject<Response>(responseString);
                

                if (!resp.Status)
                {
                    Error = "Username or Password is incorrect. Please try again";
                    return Page();
                } 
                else
                {
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

                    if (signature != computedSignature)
                    {
                        Error = "Something went wrong. Please try again.";
                        return Page();
                    }

                    var handler = new JwtSecurityTokenHandler();

                    var token = handler.ReadToken(jwt) as JwtSecurityToken;

                    var userID = token.Claims.First(claim => claim.Type == "id").Value;

                    var userType = token.Claims.First(claim => claim.Type == "accountType").Value;

                    if(userType != "employee")
                    {
                        Error = "Username or Password is incorrect. Please try again";
                        return Page();
                    }

                    EmployeeUtil empUtil = new EmployeeUtil(_context);

                    EmployeeAPIDAO userEmployee = empUtil.GetEmployeeByUserId(Convert.ToInt32(userID));

                    var userRole = userEmployee.RoleName;


                    // var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                    
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Username),
                        new Claim(ClaimTypes.Name, jwt),
                        new Claim(ClaimTypes.Role, userRole),
                        new Claim(ClaimTypes.PrimarySid, Convert.ToString(userEmployee.Id))
                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return Redirect(returnURL);
                }
            } catch (JsonReaderException) {
                Error = "Username or Password is incorrect. Please try again";
                return Page();
            }

        }

        private static string Base64UrlEncode(byte[] input) {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }

    }

    class Response
    {
        public bool Status { get; set; }
        public string Token { get; set; }
    }
}