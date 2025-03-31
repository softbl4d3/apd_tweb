using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eUseControl.Web.DTO
{
    public class AdminMenu
    {
        public List<DishEdit> Dishes { get; set; }
        public List<string> AvailableCategories { get; set; } // Для выпадающего списка
    }

    public class DishDetailsModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string ImageUrl { get; set; }
    }

    public class DishEdit
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
        public string Ingredients { get; set; }
        public bool IsAvailable { get; set; }
    }
}