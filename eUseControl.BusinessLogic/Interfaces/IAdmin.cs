using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Entities.Resps;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IAdmin : ICrudIngredientAdmin, ICrudDishAdmin, ICrudTablesAdmin
    {
        List<EmpDTO> GetAllEmployee();
    }

    public interface ICrudIngredientAdmin
    {
        AdminResp AddIngredient(IngridientDTO ingrid);
        AdminResp EditIngredient(IngridientDTO ingrid);
        AdminResp DeleteIngredient(int Id);

        List<IngridientDTO> GetAllIngredients();


        IngridientDTO GetIngredientById(int ingId);


    }

    public interface ICrudTablesAdmin
    {
       //AdminResp AddTable(TableDTO table);
    }

    public interface ICrudDishAdmin
    {
        AdminResp AddDish(DishDTO dish);
        AdminResp DeleteDish(int Id);

        DishDTO GetDishById(int Id);
        List<DishDTO> GetAllDishes();

    }

}
