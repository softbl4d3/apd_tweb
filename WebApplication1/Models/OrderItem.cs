﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TWeb48.Models;

namespace eUseControl.Web.Models
{
    public class OrderItem : BaseDbItem
    {
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public string Status { get; set; } // Новое поле
    }
}