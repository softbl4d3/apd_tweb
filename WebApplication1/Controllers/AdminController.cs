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

namespace eUseControl.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly SessionBL _session;
        public AdminController()
        {
            var bl = new eUseControl.BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
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
            var employeesDTO = _session.Admin.GetAllEmployee(); // List<UserDTO>

            var employees = employeesDTO.Select(user => new EmployeeViewModel
            {
                UserName = user.UserName,
                Role = user.Role
            }).ToList();
            return View(employees);
        }

        public ActionResult Dashboard()
        {

            var model = new AdminDTO
            {
                Tables = GetMockTables(),
                //CurrentOrders = GetMockOrders()
            };

            return View(model);
        }
        private List<TableDbTable> GetMockTables() // Заглушка для списка столов
        {
            return new List<TableDbTable>
        {
            new TableDbTable { Id = 1, TableNumber = 42, Capacity = 4, Status = TStatus.Free, Zone = TZone.none },
            new TableDbTable { Id = 2, TableNumber = 233, Capacity = 2, Status = TStatus.Occupied, Zone = TZone.none },
            new TableDbTable { Id = 3, TableNumber = 3, Capacity = 6, Status = TStatus.Reserved, Zone = TZone.vip}
        };
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
                var responce = _session.Admin.AddIngridient(Ignridient);
                if (responce.Status == true)
                {
                    return RedirectToAction("Menu");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        public ActionResult Menu()
        {
            var dishesDTO = _session.Admin.GetAllDishes();

            var dishes = dishesDTO.Select(dish => new DishViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Category = dish.Category,
                Price = dish.Price,
                IsAvailable = dish.IsAvailable
            }).ToList();

            return View(dishes);
        }

        // GET: Admin/Create
        [HttpGet]
        public ActionResult Create()
        {
            var ingredientsDto = _session.Admin.GetAllIngridients();

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

                _session.Admin.AddDish(dishDto);

                return RedirectToAction("Menu");

            }

            return View(model); // Если ошибка, вернуть модель обратно
        }

        // GET: Admin/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var dish = _dishes.FirstOrDefault(d => d.Id == id);
        //    return View(dish);
        //}

        // POST: Admin/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Dish updatedDish)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existing = _dishes.First(d => d.Id == updatedDish.Id);
        //        _dishes.Remove(existing);
        //        _dishes.Add(updatedDish);
        //        return RedirectToAction("Menu");
        //    }
        //    return View(updatedDish);
        //}

        // POST: Admin/ToggleStatus/5
        //[HttpPost]
        //public ActionResult ToggleStatus(int id)
        //{
        //    var dish = _dishes.First(d => d.Id == id);
        //    dish.IsAvailable = !dish.IsAvailable;
        //    return RedirectToAction("Menu");
        //}

        //// GET: Admin/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var dish = _dishes.First(d => d.Id == id);
        //    _dishes.Remove(dish);
        //    return RedirectToAction("Menu");
        //}

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