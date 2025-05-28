using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Helpers
{
    public class LoginHelper
    {
        public static string HashGen(string password)
        {
            var salt = Encoding.Default.GetBytes("tung2000pol");
            using (var rfc2898 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                var hash = rfc2898.GetBytes(32);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}