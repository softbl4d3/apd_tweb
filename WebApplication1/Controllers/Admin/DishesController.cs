using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Enums;
using eUseControl.Web.Filters;
using eUseControl.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers.Admin
{
    [RoleAuthorization(URole.Admin)]

    [Route("Admin")]
    
    public class DishesController : Controller
    {

        private readonly IDish _dishLogic;
        private readonly IIngredient _ingLogic;

        public DishesController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _dishLogic = bl.GetDishBL();
            _ingLogic = bl.GetIngredientBL();
        }

        public ActionResult Menu()
        {
            var dishesDTO = _dishLogic.GetAllDishes();

            var dishes = dishesDTO.Select(dish => new DishViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Category = dish.Category,
                Price = dish.Price,
                IsAvailable = dish.IsAvailable
            }).ToList();

            return View("~/Views/Admin/Dish/Menu.cshtml", dishes);
        }

        // GET: Admin/Create
        [HttpGet]
        public ActionResult Create()
        {
            var ingredientsDto = _ingLogic.GetAllIngredients();

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

            return View("~/Views/Admin/Dish/Create.cshtml", model);
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
                                Status = i.Status,
                                Quantity = i.Quantity,
                            })
                            .ToList()
                };

                _dishLogic.AddDish(dishDto);

                return RedirectToAction("Menu");

            }

            return RedirectToAction("Create", model); // Если ошибка, вернуть модель обратно
        }



        public ActionResult Edit(int id)
        {
            var dishDto = _dishLogic.GetDishById(id);
            var allIngredients = _ingLogic.GetAllIngredients();

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
                    Quantity = dishDto.Ingredients.FirstOrDefault(x => x.Id == ingredient.Id)?.Quantity ?? 0,
                    Selected = dishDto.Ingredients.Any(x => x.Id == ingredient.Id)
                }).ToList()
            };

            return View("~/Views/Admin/Dish/Edit.cshtml", dish);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DishViewModel model)
        {
            if (ModelState.IsValid)
            {
                var selectedIngredients = model.Ingredients.Where(i => i.Selected).ToList();

                var dishDto = new DishDTO
                {
                    Id = model.Id,
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
                                Status = i.Status,
                                Quantity = i.Quantity,
                            })
                            .ToList()
                };
                _dishLogic.EditDish(dishDto);
            }
            return RedirectToAction("Menu");
        }


        public ActionResult Delete(int id)
        {

            var resp = _dishLogic.DeleteDish(id);

            if (resp.Status == true)
            {
                return RedirectToAction("Menu");
            }
            else
            {
                return RedirectToAction("Menu");
            }

        }
    }
}