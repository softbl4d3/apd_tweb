using System.Collections.Generic;
using System;
using eUseControl.Web.Models;

public class WaiterOrderViewModel
{
    public int OrderId { get; set; }
    public int TableNumber { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderComment { get; set; }
    public List<OrderItemViewModel> Items { get; set; }
}


