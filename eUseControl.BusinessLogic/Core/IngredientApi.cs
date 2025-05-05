using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
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
    public class IngredientApi
    {
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

                    if (existingingridient != null)
                    {
                        return new AdminResp
                        {
                            Status = false,
                            Message = $"Ingridient already exists"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new AdminResp
                {
                    Status = false,
                    Message = $"error, ex = {ex.Message}"
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
                        .Include(x=>x.Ingredients)
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
            List<IngridientDTO> ingredientsDTO = ing.Select(x => new IngridientDTO
            {
                Id = x.Id,
                Name = x.Name,
                Amount = x.Amount,
                Status = (IngridStaus)x.Status
            }).ToList();

            return ingredientsDTO;
        }
    }
}
