using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Enums;

namespace eUseControl.Web.Models
{
    public class IngredientSelectionViewModel
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public int Quantity { get; set; }
        public IngridStaus Status { get; set; }
        public bool Selected { get; set; }
    }
}
