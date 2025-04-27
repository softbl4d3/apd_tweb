using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic.Core;
using eUseControl.Domain.Entities.Resps;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Orders;

namespace eUseControl.BusinessLogic
{
    public class SessionBL 
    {
        private readonly UserApi _userApi = new UserApi();
        private readonly AdminApi _adminApi = new AdminApi();

        //--------------------------------------- User-часть --------------------------------------------------//

        public IUser User => new UserWrapper(_userApi);
        private class UserWrapper : IUser
        {
            private readonly UserApi _api;
            public UserWrapper(UserApi api) => _api = api;

            public RegEmpResp RegisterEmployee(UserDTO data) => _api.RegisterEmployeeAction(data);

        }

        //--------------------------------------- Admin-часть --------------------------------------------------//
        public IAdmin Admin => new AdminWrapper(_adminApi);
        private class AdminWrapper : IAdmin
        {
            private readonly AdminApi _api;
            public AdminWrapper(AdminApi api) => _api = api;
            // -- Ingredients -- 
            public List<IngridientDTO> GetAllIngredients() => _api.GetAllIngredientsAction();
            public AdminResp AddIngredient(IngridientDTO ingrid) => _api.AddIngredientAction(ingrid);

            public AdminResp EditIngredient(IngridientDTO ingrid) => _api.EditIngredientAction(ingrid);
            public IngridientDTO GetIngredientById(int Id) => _api.GetIngredietByIdAction(Id);
            public AdminResp DeleteIngredient(int Id) => _api.DeleteIngredientAction(Id);

            // -- Dishes --
            public List<DishDTO> GetAllDishes() => _api.GetAllDishesAction();
            public AdminResp AddDish(DishDTO dish) => _api.AddDishAction(dish);
            public List<EmpDTO> GetAllEmployee() => _api.GetAllEmployeeAction();
            public DishDTO GetDishById(int Id) => _api.GetDishByIdAction(Id);
            public AdminResp DeleteDish(int Id) => _api.DeleteDishAction(Id);
        }
    }
}
