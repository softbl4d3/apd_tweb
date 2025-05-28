using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Enums;

namespace eUseControl.BusinessLogic.Services
{
    internal class DishService
    {
        public bool CheckDishAvailability(DishDbTable dish)
        {
            if (dish.Ingredients == null || !dish.Ingredients.Any())
                return false;

            //foreach (var ingredient in dish.Ingredients)
            //{
            //    if (ingredient.Status == IngridStaus.Unavailable)
            //        return false;
            //}

            return true;
        }

        public bool ShouldUpdateDish(bool beforeStatus, IngridStaus currentStatus)
        {
            // Случай 1: Был доступен, стал недоступен
            bool becameUnavailable = beforeStatus && currentStatus == IngridStaus.Unavailable;

            // Случай 2: Был недоступен, стал доступен или мало
            bool becameAvailable = !beforeStatus &&
                                 (currentStatus == IngridStaus.Available ||
                                  currentStatus == IngridStaus.Low);

            return becameUnavailable || becameAvailable;
        }
    }
}
