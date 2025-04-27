using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Enums;

namespace eUseControl.Web.Models
{
    public class IngridientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public IngridStaus Status { get; set; }
    }
}
