using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Entities.Resps;

namespace eUseControl.BusinessLogic.Core
{
    public class DishApi
    {
        public AdminResp AddDishAction(DishDTO dish)
        {
            DishDbTable newDish = new DishDbTable
            {
                Name = dish.Name,
                Description = dish.Description,
                Category = dish.Category,
                Price = dish.Price,
                IsAvailable = dish.IsAvailable,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Ingredients = new List<DishIngredients>()
            };

            try
            {
                using (var context = new OrderContext())
                {
                    var existingDish = context.Dishes.FirstOrDefault(d => d.Name == newDish.Name);
                    if (existingDish != null)
                    {
                        return new AdminResp
                        {
                            Status = false,
                            Message = "Dish already exists"
                        };
                    }

                    context.Dishes.Add(newDish);

                    foreach (var ingredientDto in dish.Ingredients)
                    {
                        var ingredientInDb = context.Ingridients
                            .FirstOrDefault(i => i.Name == ingredientDto.Name);

                        if (ingredientInDb == null)
                        {
                            return new AdminResp
                            {
                                Status = false,
                                Message = $"Ingredient '{ingredientDto.Name}' not found"
                            };
                        }

                        var dishIngredient = new DishIngredients
                        {
                            Dish = newDish,
                            Ingredient = ingredientInDb,
                            Quantity = ingredientDto.Quantity 
                        };

                        newDish.Ingredients.Add(dishIngredient);
                    }


                    context.SaveChanges();
                }

                return new AdminResp
                {
                    Status = true,
                    Message = "Dish added successfully"
                };
            }
            catch (Exception ex)
            {
                return new AdminResp
                {
                    Status = false,
                    Message = $"Something went wrong: {ex.Message}"
                };
            }
        }

        public AdminResp EditDishAction(DishDTO dishDto)
        {
            try
            {
                using (var context = new OrderContext())
                {
                    var existingDish = context.Dishes
                        .Include(d => d.Ingredients)
                        .FirstOrDefault(d => d.Id == dishDto.Id);

                    if (existingDish == null)
                    {
                        return new AdminResp { Status = false, Message = "Dish does not exist" };
                    }

                    existingDish.Name = dishDto.Name;
                    existingDish.Description = dishDto.Description;
                    existingDish.Category = dishDto.Category;
                    existingDish.IsAvailable = dishDto.IsAvailable;
                    existingDish.Price = dishDto.Price;
                    existingDish.UpdatedAt = DateTime.Now;

                    context.DishIngredients.RemoveRange(existingDish.Ingredients);

                    if (dishDto.Ingredients != null)
                    {
                        foreach (var ingredientDto in dishDto.Ingredients)
                        {
                            var ingredient = context.Ingridients.FirstOrDefault(n=>n.Name == ingredientDto.Name);
                            if (ingredient != null)
                            {
                                existingDish.Ingredients.Add(new DishIngredients
                                {
                                    Dish = existingDish,
                                    Ingredient = ingredient,
                                    Quantity = ingredientDto.Quantity
                                });
                            }
                        }
                    }

                    context.SaveChanges();
                    return new AdminResp { Status = true, Message = "Dish updated successfully" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing dish: {ex}");
                return new AdminResp { Status = false, Message = $"Error: {ex.Message}" };
            }
        }

        public List<DishDTO> GetAllDishesAction()
        {
            try
            {
                List<DishDbTable> DishesDb;
                using (var context = new OrderContext())
                {
                    DishesDb = context.Dishes
                        .Include("Ingredients")
                        .ToList();
                }
                var dishes = DishesDb.Select(x => new DishDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Category = x.Category,
                    Price = x.Price,
                    IsAvailable = x.IsAvailable,
                    Ingredients = x.Ingredients?.Select(i => new IngridientDTO
                    {
                        Id = i.Ingredient?.Id ?? 0,
                        Name = i.Ingredient?.Name ?? "Unknown ingredient",
                        Quantity = i.Quantity
                    }).ToList() ?? new List<IngridientDTO>()
                }).ToList();

                return dishes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении блюд: {ex.Message}");
                Console.WriteLine(ex.ToString());
                return new List<DishDTO>();
            }
        }
        public DishDTO GetDishByIdAction(int Id)
        {
            try
            {
                DishDbTable dishDb;
                using (var context = new OrderContext())
                {
                    dishDb = context.Dishes
                        .Include("Ingredients")
                        .Include("Ingredients.Ingredient")
                        .FirstOrDefault(d => d.Id == Id);
                }

                if (dishDb != null)
                {
                    DishDTO dishDto = new DishDTO
                    {
                        Id = dishDb.Id,
                        Name = dishDb.Name,
                        Description = dishDb.Description,
                        Category = dishDb.Category,
                        Price = dishDb.Price,
                        IsAvailable = dishDb.IsAvailable,
                        Ingredients = dishDb.Ingredients?.Select(x => new IngridientDTO
                        {
                            Id = x.Ingredient.Id, 
                            Name = x.Ingredient.Name, 
                            Amount = x.Ingredient.Amount, 
                            Status = x.Ingredient.Status, 
                            Quantity = x.Quantity 
                        }).ToList() ?? new List<IngridientDTO>()
                    };

                    return dishDto;
                }
                else
                {
                    return new DishDTO();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace); 
                return new DishDTO();
            }
        }
        public AdminResp DeleteDishAction(int Id)
        {
            try
            {
                using (var context = new OrderContext())
                {
                    var existingDish = context.Dishes
                        .Include(d => d.Ingredients)
                        .FirstOrDefault(d => d.Id == Id);

                    if (existingDish == null)
                    {
                        return new AdminResp
                        {
                            Status = false,
                            Message = "Dish not found"
                        };
                    }

                    var ingredientsToRemove = existingDish.Ingredients.ToList();
                    foreach (var ingredient in ingredientsToRemove)
                    {
                        context.Entry(ingredient).State = EntityState.Deleted;
                    }
                    var orderItemsToRemove = context.OrderItems
                        .Where(oi => oi.DishId.Id == existingDish.Id)
                        .ToList();

                    context.OrderItems.RemoveRange(orderItemsToRemove);
                    context.Dishes.Remove(existingDish);

                    context.SaveChanges();

                    return new AdminResp
                    {
                        Status = true,
                        Message = "Dish and its ingredient relations successfully deleted"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AdminResp
                {
                    Status = false,
                    Message = $"Error deleting dish: {ex.Message}"
                };
            }
        }
    }
}

