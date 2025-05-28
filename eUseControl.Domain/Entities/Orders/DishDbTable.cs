using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.Orders
{
    [Table("Dishes")]

    public class DishDbTable : BaseDbItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DCategory Category { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public List<DishIngredients> Ingredients { get; set; }
    }
}