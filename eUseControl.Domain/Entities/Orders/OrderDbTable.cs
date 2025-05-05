using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Enums;


namespace eUseControl.Domain.Entities.Orders
{
    [Table("Orders")]

    public class OrderDbTable : BaseDbItem
    {
        public int TableId { get; set; }
        public int WaiterId { get; set; }
        public DateTime CompletedTime { get; set; }
        public DStatus Status { get; set; } 
        public decimal TotalAmount { get; set; }
        public string Note { get; set; }
        public  List<OrderItemDbTable> OrderItems { get; set; }
    }
}