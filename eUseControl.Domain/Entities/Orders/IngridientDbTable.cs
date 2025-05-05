using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.Orders
{
    [Table("Ingridients")]

    public class IngridientDbTable : BaseDbItem
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public IngridStaus Status { get; set; }
        public List<DishDbTable> Dishes { get; set; }
    }
}
