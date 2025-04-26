using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.DTO
{
    public class DishDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DCategory Category { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public List<IngridientDTO> Ingredients { get; set; }
    }
}
