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
    public class DishBL : DishApi, IDish
    {
        public AdminResp AddDish(DishDTO dish) => AddDishAction(dish);
        public AdminResp EditDish(DishDTO dish) => EditDishAction(dish);
        public AdminResp DeleteDish(int Id) => DeleteDishAction(Id);
        public DishDTO GetDishById(int Id) => GetDishByIdAction(Id);
        public List<DishDTO> GetAllDishes() => GetAllDishesAction();

    }
}
