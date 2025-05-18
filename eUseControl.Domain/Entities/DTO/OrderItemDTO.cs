using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public string WaiterName { get; set; }
        public int TableNumber { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
        public DStatus Status { get; set; }
    }

}
