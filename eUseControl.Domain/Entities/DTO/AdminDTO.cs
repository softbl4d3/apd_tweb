using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace eUseControl.Domain.Entities.DTO
{
    public class AdminDTO
    {
        public List<Table> Tables { get; set; }
        public List<Order> CurrentOrders { get; set; }
    }
}