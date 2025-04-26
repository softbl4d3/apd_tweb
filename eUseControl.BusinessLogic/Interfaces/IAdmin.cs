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
        AdminResp AddIngridient(IngridientDTO ingrid);
        List<IngridientDTO> GetAllIngridients();


    }

    public interface ICrudTablesAdmin
    {
       //AdminResp AddTable(TableDTO table);
    }

    public interface ICrudDishAdmin
    {
        AdminResp AddDish(DishDTO dish);

        List<DishDTO> GetAllDishes();

    }

}
