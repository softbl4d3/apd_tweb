using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Enums;


namespace eUseControl.Domain.Entities.Orders
{
    [Table("OrderItems")]

    public class OrderItemDbTable : BaseDbItem
    {
        public OrderDbTable OrderId { get; set; }
        public DishDbTable DishId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public DStatus Status { get; set; } 
    }
}