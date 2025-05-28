using System.Collections.Generic;
using System;
using eUseControl.Web.Models;
using eUseControl.Domain.Enums;

public class WaiterOrderViewModel
{
    public int OrderId { get; set; }
    public int TableNumber { get; set; }
    public DStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderComment { get; set; }
    public string WaiterName { get; set; }
    public DateTime CompletedTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemViewModel> Items { get; set; }
}


