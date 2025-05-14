using System;
using System.Collections.Generic;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Enums;
using eUseControl.Web.Models;

public class OrderViewModel
{
	public int TableNumber { get; set; }
	public List<DishSelectViewModel> Dishes { get; set; }
	public string Note { get; set; }
	public decimal TotalAmount { get; set; }
	public string Waiter { get; set; }
	public DStatus Status { get; set; }
}

public class DishSelectViewModel
{

	public int Id { get; set; }
	public string Name { get; set; }

	public string Description { get; set; }
	public string Note { get; set; }
	public DCategory Category { get; set; }
	public decimal Price { get; set; }
	public int Quantity { get; set; }
	public bool IsAvailable { get; set; }
	
}


