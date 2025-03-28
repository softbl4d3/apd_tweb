using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TWeb48.Models;

namespace eUseControl.Web.Models
{
    // Models/Order.cs
    public class Order : BaseDbItem
    {
        public int OrderNumber { get; set; }
        public int TableId { get; set; }
        public int WaiterId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? CompletedTime { get; set; }
        public string Status { get; set; } // "New", "InProgress", "Ready", "Completed", "Cancelled"
        public decimal TotalAmount { get; set; }
        public string Comment { get; set; }
    }
}