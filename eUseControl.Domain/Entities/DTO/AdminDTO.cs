using eUseControl.Domain.Entities.Orders;
using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace eUseControl.Domain.Entities.DTO
{
    public class AdminDTO
    {
        public List<TableDbTable> Tables { get; set; }
        public List<OrderDbTable> CurrentOrders { get; set; }
    }
}