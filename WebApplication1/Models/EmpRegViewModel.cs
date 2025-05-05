using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Enums;

namespace eUseControl.Web.Models
{
    public class EmpRegViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public URole Role { get; set; }
    }
}
