using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Resps;

namespace eUseControl.BusinessLogic
{
    public class IngridientBL : IngredientApi, IIngredient
    {
        public AdminResp AddIngredient(IngridientDTO ingrid) => AddIngredientAction(ingrid);
        public AdminResp EditIngredient(IngridientDTO ingrid) => EditIngredientAction(ingrid);
        public AdminResp DeleteIngredient(int Id) => DeleteIngredientAction(Id);
        public List<IngridientDTO> GetAllIngredients() => GetAllIngredientsAction();
        public IngridientDTO GetIngredientById(int ingId) => GetIngredietByIdAction(ingId);
    }
}
