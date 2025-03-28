using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.DTO
{
    public class AdminDashboard
    {
        public List<Table> Tables { get; set; }
        public List<Order> CurrentOrders { get; set; }
    }
}