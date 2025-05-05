using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Resps;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IIngredient
    {
        AdminResp AddIngredient(IngridientDTO ingrid);
        AdminResp EditIngredient(IngridientDTO ingrid);
        AdminResp DeleteIngredient(int Id);

        List<IngridientDTO> GetAllIngredients();


        IngridientDTO GetIngredientById(int ingId);
    }
}
