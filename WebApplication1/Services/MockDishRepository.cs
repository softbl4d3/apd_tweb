using eUseControl.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Services
{
    // Repositories/MockDishRepository.cs
    public class MockDishRepository
    {
        private List<Dish> _dishes = new List<Dish>
    {
        new Dish
        {
            Id = 1,
            Name = "Карбонара",
            Category = "Горячее",
            Price = 650,
            IsAvailable = true,
            Ingredients = "Паста, бекон, сливки, яйцо"
        },
        new Dish
        {
            Id = 2,
            Name = "Цезарь",
            Category = "Салаты",
            Price = 5252,
            IsAvailable = false,
            Ingredients = "Курица, салат, сухарики, соус"
        }
    };

        public List<Dish> GetAll() => _dishes;

        public Dish GetById(int id) => _dishes.FirstOrDefault(d => d.Id == id);

        public void Update(Dish dish)
        {
            var existing = GetById(dish.Id);
            if (existing != null)
            {
                existing.Name = dish.Name;
                existing.Description = dish.Description;
                existing.Category = dish.Category;
                existing.Price = dish.Price;
                existing.ImageUrl = dish.ImageUrl;
                existing.Ingredients = dish.Ingredients;
                existing.IsAvailable = dish.IsAvailable;
            }
        }
    }
}