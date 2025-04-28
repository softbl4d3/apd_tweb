using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Resps;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IDish
    {
        AdminResp AddDish(DishDTO dish);

        AdminResp EditDish(DishDTO dish);
        AdminResp DeleteDish(int Id);

        DishDTO GetDishById(int Id);
        List<DishDTO> GetAllDishes();

    }
}
