using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.Resps
{
    public class LoginResp
    {
        public bool Status { get; set; }
        public URole Role { get; set; }
        public string Message { get; set; }
    }
}
