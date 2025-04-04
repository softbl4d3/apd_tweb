using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.DTO
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public URole Role { get; set; }
    }
}
