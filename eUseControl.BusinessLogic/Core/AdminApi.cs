using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Entities.Resps;
using eUseControl.Domain.Enums;

namespace eUseControl.BusinessLogic.Core
{
    public class AdminApi
    {
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
        public AdminResp AddIngridientAction(IngridientDTO ing)
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
            catch(Exception ex)
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
        public List<IngridientDTO> GetAllIngridientsAction()
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
                Name = x.Name,
                Amount = x.Amount,
                Status = (IngridStaus)x.Status
            }).ToList();

            return ingredientsDTO;
        }

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
            catch(Exception ex)
            {
                Console.Write( $"Имя: {ex.Message}");
                
                return new List<DishDTO>();
            }
            
        }

    }
}
