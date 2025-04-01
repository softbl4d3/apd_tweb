using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.DTO
{
    public class CreateOrderDTO
    {
        public int TableId { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
        public string Comment { get; set; }
    }

    public class OrderItemDTO
    {
        public int DishId { get; set; }
        public int Quantity { get; set; } = 1;
        public string Note { get; set; }
    }
}
