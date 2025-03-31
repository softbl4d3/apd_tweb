using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TWeb48.Models;

namespace eUseControl.Web.Models
{
    public class Dish : BaseDbItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageUrl { get; set; }
        public string Ingredients { get; set; }
    }
}