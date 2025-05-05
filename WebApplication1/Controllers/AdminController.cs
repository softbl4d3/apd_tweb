using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Web.Models;
using eUseControl.Domain.Enums;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic;
using System.Drawing.Printing;
using eUseControl.Domain.Entities.Orders;
using System.Net.NetworkInformation;
using System.Web.UI.WebControls;

namespace eUseControl.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ITable _tableLogic;
        public AdminController()
        { 
            var bl = new BusinessLogic.BusinessLogic();
            _tableLogic = bl.GetTableBL();
        }

        // GET: /Admin/RegisterEmployee
        public ActionResult RegisterEmployee()
        {
            return View();
        }

        // POST: /Admin/RegisterEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterEmployee(EmpRegViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Role = (URole)model.Role
                };
                var responce = _session.User.RegisterEmployee(user);
                if (responce.Status == true)
                {
                    return RedirectToAction("EmployeeList");
                }
                else
                {
                    return RedirectToAction("RegisterEmployee");
                }
            }

                // Если есть ошибки валидации – вернуть представление с сообщениями
                return View(model);
        }

        // Пример метода для вывода списка сотрудников
        public ActionResult EmployeeList()
        {
            var employeesDTO = _session.User.GetAllEmployee();

            var employees = employeesDTO.Select(user => new EmployeeViewModel
            {
                UserName = user.UserName,
                Role = user.Role
            }).ToList();
            return View(employees);
        }

        public ActionResult Dashboard()
        {

            List<TableDTO> tableDto = _tableLogic.GetAllTables();

            List<TableViewModel> tables = tableDto.Select(t => new TableViewModel{

            TableNumber = t.TableNumber,
            Capacity = t.Capacity,
            Zone = t.Zone,
            Status = t.Status,

            }).ToList();

            return View(tables);
        }
        [HttpGet]
        public ActionResult TableAdd()
        {

            return View();
        }
        [HttpPost]
        public ActionResult TableAdd(TableViewModel table)
        {
            TableDTO tableDto = new TableDTO
            {
                TableNumber = table.TableNumber,
                Capacity = table.Capacity,
                Zone = table.Zone,
                Status = TStatus.Free
            };
            var resp = _tableLogic.AddTable(tableDto);
            if (resp.Status == true)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View(table);
            }
                
        }
        [HttpGet]
        public ActionResult TableEdit(int TableNumber)
        {
            TableDTO tableDto = _tableLogic.GetTableById(TableNumber);
            TableViewModel table = new TableViewModel
            {
                TableNumber = tableDto.TableNumber,
                Capacity = tableDto.Capacity,
                Zone = tableDto.Zone,
                Status = tableDto.Status,
            };
            
            return View(table);
        }

        [HttpPost]
        public ActionResult TableEdit(TableViewModel t)
        {
            var tableDto = new TableDTO { 
                TableNumber = t.TableNumber,
                Capacity = t.Capacity,
                Status = t.Status,
                Zone = t.Zone,
            };

            var resp = _tableLogic.EditTable(tableDto);
            if (resp.Status == true)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View(t);
            }
        }

        public ActionResult TableDelete(int TableNumber)
        {
            _tableLogic.DeleteTable(TableNumber);
            return RedirectToAction("Dashboard");
            
        }
        // -------------------------------- Ingredients  ----------------------------------
        public ActionResult Ingredients()
        {
            var ingrDTO = _session.Ingredient.GetAllIngredients();

            var igredients = ingrDTO.Select(ing => new IngridientViewModel
            {
                Id = ing.Id,
                Name = ing.Name,
                Amount = ing.Amount,
                Status = ing.Status,

            }).ToList();

            return View(igredients);
        }
        
        public ActionResult IngridientAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IngridientAdd(IngridientViewModel Ingrid)
        {
            if (ModelState.IsValid)
            {
                IngridientDTO Ignridient = new IngridientDTO
                {
                    Name = Ingrid.Name,
                    Amount = Ingrid.Amount,
                    Status = (IngridStaus)Ingrid.Status
                };
                var responce = _session.Ingredient.AddIngredient(Ignridient);
                if (responce.Status == true)
                {
                    return RedirectToAction("Ingredients");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditIngredient(int id)
        {
            IngridientDTO ing = _session.Ingredient.GetIngredientById(id);
            var model = new IngridientViewModel
            {
                Id = ing.Id,
                Name = ing.Name,
                Amount = ing.Amount,
                Status = ing.Status
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult EditIngredient(IngridientViewModel ing)
        {
            IngridientDTO ingDto = new IngridientDTO
            {
                Id = ing.Id,
                Name = ing.Name,
                Amount = ing.Amount,
                Status = ing.Status
            };
            var resp = _session.Ingredient.EditIngredient(ingDto);

            if (resp.Status == true)
            {
                return RedirectToAction("Ingredients");
            }
            else
            {
                return View(ing);
            }
        }

        public ActionResult DeleteIngredient (int id)
        {
            var resp = _session.Ingredient.DeleteIngredient(id);

            if (resp.Status == true)
            {
                return RedirectToAction("Ingredients");
            }
            else
            {
                return RedirectToAction("Ingredients");
            }
        }


        // -------------------------------- Menu  ----------------------------------
        public ActionResult Menu()
        {
            var dishesDTO = _session.Dish.GetAllDishes();

            var dishes = dishesDTO.Select(dish => new DishViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Category = dish.Category,
                Price = dish.Price,
                IsAvailable = dish.IsAvailable
            }).ToList();

            return View("Menu",dishes);
        }

        // GET: Admin/Create
        [HttpGet]
        public ActionResult Create()
        {
            var ingredientsDto = _session.Ingredient.GetAllIngredients();

            var model = new DishViewModel
            {
                Ingredients = ingredientsDto.Select(i => new IngredientSelectionViewModel
                {
                    Name = i.Name,
                    Amount = i.Amount,
                    Status = (IngridStaus)i.Status,
                    Selected = false  
                }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DishViewModel model)
        {
            if (ModelState.IsValid)
            {
                var selectedIngredients = model.Ingredients.Where(i => i.Selected).ToList();

                var dishDto = new DishDTO
                {
                    Name = model.Name,
                    Description = model.Description,
                    Category = model.Category,
                    Price = model.Price,
                    IsAvailable = model.IsAvailable,
                    Ingredients = model.Ingredients
                            .Where(i => i.Selected)
                            .Select(i => new IngridientDTO
                            {
                                Name = i.Name,
                                Amount = i.Amount,
                                Status = i.Status
                            })
                            .ToList()
                 };

                _session.Dish.AddDish(dishDto);

                return RedirectToAction("Menu");

            }

            return View(model); // Если ошибка, вернуть модель обратно
        }


        public ActionResult Edit(int id)
        {
            var dishDto = _session.Dish.GetDishById(id);
            var allIngredients = _session.Ingredient.GetAllIngredients();

            DishViewModel dish = new DishViewModel
            {
                Id = dishDto.Id,
                Name = dishDto.Name,
                Description = dishDto.Description,
                Category = dishDto.Category,
                Price = dishDto.Price,
                IsAvailable = dishDto.IsAvailable,
                Ingredients = allIngredients.Select(ingredient => new IngredientSelectionViewModel
                {
                    Name = ingredient.Name,
                    Amount = dishDto.Ingredients.FirstOrDefault(x => x.Id == ingredient.Id)?.Amount ?? 0,
                    Status = ingredient.Status,
                    Selected = dishDto.Ingredients.Any(x => x.Id == ingredient.Id)
                }).ToList()
            };

            return View(dish);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DishViewModel updatedDish)
        {
            if (ModelState.IsValid)
            {
                DishDTO dishDto = new DishDTO
                {
                    Id = updatedDish.Id,
                    Name = updatedDish.Name,
                    Description = updatedDish.Description,
                    Category = updatedDish.Category,
                    Price = updatedDish.Price,
                    IsAvailable = updatedDish.IsAvailable,
                    Ingredients = updatedDish.Ingredients
                    .Where(i => i.Selected == true)
                    .Select(i => new IngridientDTO
                    {
                        Name = i.Name,
                        Amount = i.Amount,
                        Status = i.Status
                    }).ToList()
                };
                _session.Dish.EditDish(dishDto);
            }
            return View(updatedDish);
        }

        // POST: Admin/ToggleStatus/5
        //[HttpPost]
        //public ActionResult ToggleStatus(int id)
        //{
        //    var dish = _dishes.First(d => d.Id == id);
        //    dish.IsAvailable = !dish.IsAvailable;
        //    return RedirectToAction("Menu");
        //}

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            
            var resp = _session.Dish.DeleteDish(id);

            if (resp.Status == true)
            {
                return RedirectToAction("Menu");
            }
            else
            {
                return RedirectToAction("Menu");
            }
        
        }

        //// POST: Admin/Delete/id
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var dish = _dishes.First(d => d.Id == id);
        //    _dishes.Remove(dish);
        //    return RedirectToAction("Menu");
        //}


    }
}