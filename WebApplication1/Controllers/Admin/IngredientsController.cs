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
    public class IngredientsController : Controller
    {
        private readonly IIngredient _ingLogic;

        public IngredientsController()
        {
            var bl = new BusinessLogic.BusinessLogic();

            _ingLogic = bl.GetIngredientBL();
            
        }

        public ActionResult Menu()
        {
            var ingrDTO = _ingLogic.GetAllIngredients();

            var igredients = ingrDTO.Select(ing => new IngridientViewModel
            {
                Id = ing.Id,
                Name = ing.Name,
                Amount = ing.Amount,
                Status = ing.Status,

            }).ToList();

            return View("~/Views/Admin/Ingredients/Menu.cshtml", igredients);
        }


        public ActionResult Add()
        {
            return View("~/Views/Admin/Ingredients/Add.cshtml");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IngridientViewModel Ingrid)
        {
            if (ModelState.IsValid)
            {
                IngridientDTO Ignridient = new IngridientDTO
                {
                    Name = Ingrid.Name,
                    Amount = Ingrid.Amount,
                    Status = (IngridStaus)Ingrid.Status
                };
                var responce = _ingLogic.AddIngredient(Ignridient);
                if (responce.Status == true)
                {
                    return RedirectToAction("Menu");
                }
                else
                {
                    return View("~/Views/Admin/Ingredients/Add.cshtml");
                }
            }
            return View("~/Views/Admin/Ingredients/Add.cshtml");
        }

        [HttpGet]

        public ActionResult Edit(int id)
        {
            IngridientDTO ing = _ingLogic.GetIngredientById(id);
            var model = new IngridientViewModel
            {
                Id = ing.Id,
                Name = ing.Name,
                Amount = ing.Amount,
                Status = ing.Status
            };
            return View("~/Views/Admin/Ingredients/Edit.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(IngridientViewModel ing)
        {
            IngridientDTO ingDto = new IngridientDTO
            {
                Id = ing.Id,
                Name = ing.Name,
                Amount = ing.Amount,
                Status = ing.Status
            };
            var resp = _ingLogic.EditIngredient(ingDto);

            if (resp.Status == true)
            {
                return RedirectToAction("Menu");
            }
            else
            {
                return View("~/Views/Admin/Ingredients/Edit.cshtml", ing);
            }
        }


        public ActionResult Delete(int id)
        {
            var resp = _ingLogic.DeleteIngredient(id);

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