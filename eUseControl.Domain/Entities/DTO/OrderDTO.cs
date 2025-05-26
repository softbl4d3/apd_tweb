using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.DTO
{
    public class OrderDTO
    {
        public string WaiterName { get; set; }
        public int TableNumber { get; set; }
        public string Note { get; set; }
        
        public decimal TotalAmount { get; set; }

        public DStatus Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }

        //public TableDbTable TableId { get; set; }
        //public UserDbTable WaiterId { get; set; }
        //public DateTime CompletedTime { get; set; }
        //public DStatus Status { get; set; }
        //public decimal TotalAmount { get; set; }
        //public string Note { get; set; }
        //public List<OrderItemDbTable> OrderItems { get; set; }

    }
