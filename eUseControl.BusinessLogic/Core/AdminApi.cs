using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.BusinessLogic.Services;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Entities.Resps;
using eUseControl.Domain.Enums;

namespace eUseControl.BusinessLogic.Core
{
    public class AdminApi
    {
        // ----------------------Employee ----------------------------------------------------------------------------------------------------------
        public List<EmpDTO> GetAllEmployeeAction()
        {
            List<EmpDTO> employeesDTO;

            using (var context = new UserContext())
            {
                var employees = context.Users
                    .Select(u => new
                    {
                        u.UserName,
                        u.Role
                    })
                    .ToList();

                employeesDTO = employees
                    .Select(e => new EmpDTO
                    {
                        UserName = e.UserName,
                        Role = e.Role
                    })
                    .ToList();
            }

            return employeesDTO;
        }
        // ----------------------Ingredient ----------------------------------------------------------------------------------------------------------

        public AdminResp AddIngredientAction(IngridientDTO ing)
        {
            IngridientDbTable ingridient = new IngridientDbTable
            {
                Name = ing.Name,
                Amount = ing.Amount,
                Status = (IngridStaus)ing.Status,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            try
            {
                IngridientDbTable existingingridient;
                using (var context = new OrderContext())
                {
                    existingingridient = context.Ingridients.FirstOrDefault(i => i.Name == ingridient.Name);
                }
            }
            catch (Exception ex)
            {
                return new AdminResp
                {
                    Status = false,
                    Message = $"Ingridient already exists, ex = {ex.Message}"
                };
            }
            try
            {
                using (var context = new OrderContext())
                {
                    context.Ingridients.Add(ingridient);
                    context.SaveChanges();
                }

                return new AdminResp
                {
                    Status = true,
                    Message = "item added succesfully"
                };
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

        public AdminResp EditIngredientAction(IngridientDTO ing)
        {
            IngridientDbTable existingIng;
            bool beforeStatus;

            try
            {
                using (var context = new OrderContext())
                {
                    existingIng = context.Ingridients.FirstOrDefault(i => i.Id == ing.Id);

                    if (existingIng.Status == IngridStaus.Unavailable) { beforeStatus = false; }
                    else { beforeStatus = true; }


                    if (existingIng == null)
                    {
                        return new AdminResp { Status = false, Message = "item no exist" };
                    }

                    existingIng.Name = ing.Name;
                    existingIng.Amount = ing.Amount;
                    existingIng.Status = ing.Status;
                    existingIng.UpdatedAt = DateTime.Now;


                    context.Ingridients.AddOrUpdate(existingIng);
                    context.SaveChanges();

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


            var _dishService = new DishService();
            if (_dishService.ShouldUpdateDish(beforeStatus, existingIng.Status))
            {
                using (var context = new OrderContext())
                {

                    List<DishDbTable> dishes = context.Dishes
                        .Include(d => d.Ingredients)
                        .Where(d => d.Ingredients.Any(i => i.Id == existingIng.Id))
                        .ToList();

                    foreach (DishDbTable dish in dishes)
                    {
                        dish.IsAvailable = _dishService.CheckDishAvailability(dish);
                    }
                    context.SaveChanges();
                }
            }



            return new AdminResp { Status = true };

        }

        public AdminResp DeleteIngredientAction(int Id)
        {
            try
            {
                IngridientDbTable ExistingIng;
                using (var context = new OrderContext())
                {
                    ExistingIng = context.Ingridients.FirstOrDefault(i => i.Id == Id);

                    if (ExistingIng != null)
                    {

                        context.Ingridients.Remove(ExistingIng);
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


            return new AdminResp
            {
                Status = true
            };
        }
        public IngridientDTO GetIngredietByIdAction(int Id)
        {
            try
            {
                IngridientDbTable ingDb;
                using (var context = new OrderContext())
                {
                    ingDb = context.Ingridients.FirstOrDefault(i => i.Id == Id);
                }
                if (ingDb != null)
                {
                    IngridientDTO ingDto = new IngridientDTO
                    {
                        Id = ingDb.Id,
                        Name = ingDb.Name,
                        Amount = ingDb.Amount,
                        Status = ingDb.Status,

                    };
                    return ingDto;
                }
                else { return new IngridientDTO(); }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return new IngridientDTO();
            }
        }
        public List<IngridientDTO> GetAllIngredientsAction()
        {
            List<IngridientDbTable> ing;
            try
            {
                using (var context = new OrderContext())
                {
                    ing = context.Ingridients.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting ingredients: {ex.Message}");
                return new List<IngridientDTO>();
            }

            // Преобразование списка
            var ingredientsDTO = ing.Select(x => new IngridientDTO
            {
                Id = x.Id,
                Name = x.Name,
                Amount = x.Amount,
                Status = (IngridStaus)x.Status
            }).ToList();

            return ingredientsDTO;
        }
        // ----------------------Dish ----------------------------------------------------------------------------------------------------------

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
                        .FirstOrDefault(i => i.Id == Id);
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
