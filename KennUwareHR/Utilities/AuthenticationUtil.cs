using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KennUwareHR.Contexts;

namespace KennUwareHR.Utilities
{
    public class AuthenticationUtil
    {
        
        private readonly HRContext _context;

        public AuthenticationUtil(HRContext context)
        {
            _context = context;
        }

        public String Salt()
        {
            return "";
        }

        public String Hash()
        {
            return "";
        }

        public bool CheckHash()
        {
            return false;
        }

        public void SaveHash()
        {
            return;
        }
    }
}
