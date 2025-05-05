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
                Ingredients = new List<IngridientDbTable>()
            };

            try
            {
                using (var context = new OrderContext())
                {
                    // Проверка: блюдо с таким названием уже существует?
                    var existingDish = context.Dishes.FirstOrDefault(d => d.Name == newDish.Name);
                    if (existingDish != null)
                    {
                        return new AdminResp
                        {
                            Status = false,
                            Message = "Dish already exists"
                        };
                    }

                    // Теперь проходим по каждому ингредиенту по имени
                    foreach (var ingredientDto in dish.Ingredients)
                    {
                        // Находим ингредиент в базе по имени
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

                        // Связываем ингредиент и блюдо
                        if (ingredientInDb.Dishes == null)
                            ingredientInDb.Dishes = new List<DishDbTable>();

                        ingredientInDb.Dishes.Add(newDish);
                        newDish.Ingredients.Add(ingredientInDb);
                    }

                    // Добавляем блюдо в базу
                    context.Dishes.Add(newDish);
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
                using(var context = new OrderContext())
                {
                    var existDish = context.Dishes.FirstOrDefault(d => d.Id == dishDto.Id);
                    
                    if(existDish == null)
                    {
                        return new AdminResp
                        {
                            Status = false,
                            Message = "dish is no exist"
                        };
                    }
                }

            }

            catch(Exception ex)
            {
                return new AdminResp
                {
                    Status = false,
                    Message = $"Something went wrong:{ex.Message}"
                };
            }
            try
            {
                DishDbTable dishDb = new DishDbTable
                {
                    Id = dishDto.Id,
                    Name = dishDto.Name,
                    Description = dishDto.Description,
                    Category = dishDto.Category,
                    IsAvailable = dishDto.IsAvailable,
                    Price = dishDto.Price,
                    Ingredients = dishDto.Ingredients.Select(i => new IngridientDbTable
                    {
                        Id = i.Id,
                        Amount = i.Amount,
                        Name = i.Name,
                        Status = i.Status,
                    }).ToList(),

                    UpdatedAt = DateTime.Now,
                    
                };

                using(var context = new OrderContext())
                {
                    context.Dishes.AddOrUpdate(dishDb);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return new AdminResp
                {
                    Status = false,
                    Message = $"Something went wrong:{ex.Message}"
                };
            }

            return new AdminResp
            {
                Status = true,
                Message =  "superguuud"
            };
        }

        public List<DishDTO> GetAllDishesAction()
        {
            try
            {
                List<DishDbTable> DishesDb;
                using (var context = new OrderContext())
                {
                    DishesDb = context.Dishes.ToList();
                }
                List<DishDTO> Dishes = DishesDb.Select(x => new DishDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Category = x.Category,
                    Price = x.Price,
                    IsAvailable = x.IsAvailable,
                }).ToList();
                return Dishes;
            }
            catch (Exception ex)
            {
                Console.Write($"Имя: {ex.Message}");

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
                         .Include(d => d.Ingredients)
                         .FirstOrDefault(d=> d.Id == Id);
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
                        Ingredients = dishDb.Ingredients != null
                            ? dishDb.Ingredients.Select(x => new IngridientDTO
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Amount = x.Amount,
                                Status = x.Status,
                            }).ToList()
                            : new List<IngridientDTO>(),

                    };

                    return dishDto;
                }
                else { return new DishDTO(); }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return new DishDTO();
            }

        }
        public AdminResp DeleteDishAction(int Id)
        {
            try
            {
                DishDbTable ExistingDish;
                using (var context = new OrderContext())
                {
                    ExistingDish = context.Dishes.FirstOrDefault(i => i.Id == Id);

                    if (ExistingDish != null)
                    {

                        context.Dishes.Remove(ExistingDish);
                        context.SaveChanges();

                        return new AdminResp
                        {
                            Status = true
                        };
                    }
                    else
                    {
                        return new AdminResp
                        {
                            Status = false,
                            Message = "item is no exist"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new AdminResp
                {
                    Status = false,
                    Message = $"something went wrong, message = {ex.Message}"
                };
            }

        }
    }
}

