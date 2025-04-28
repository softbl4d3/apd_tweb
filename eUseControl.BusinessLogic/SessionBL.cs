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
        private readonly IngredientApi _ingredientApi = new IngredientApi();
        private readonly DishApi _dishApi = new DishApi();
        private readonly TableApi _tableApi = new TableApi();

        //--------------------------------------- Users--------------------------------------------------//

        public IUser User => new UserWrapper(_userApi);
        private class UserWrapper : IUser
        {
            private readonly UserApi _api;
            public UserWrapper(UserApi api) => _api = api;
            public List<EmpDTO> GetAllEmployee() => _api.GetAllEmployeeAction();

            public RegEmpResp RegisterEmployee(UserDTO data) => _api.RegisterEmployeeAction(data);

        }

        //--------------------------------------- Ingredients --------------------------------------------------//
        public IIngredient Ingredient => new IngredientWrapper(_ingredientApi);
        private class IngredientWrapper : IIngredient
        {
            private readonly IngredientApi _api;
            public IngredientWrapper(IngredientApi api) => _api = api;
            public List<IngridientDTO> GetAllIngredients() => _api.GetAllIngredientsAction();
            public AdminResp AddIngredient(IngridientDTO ingrid) => _api.AddIngredientAction(ingrid);

            public AdminResp EditIngredient(IngridientDTO ingrid) => _api.EditIngredientAction(ingrid);
            public IngridientDTO GetIngredientById(int Id) => _api.GetIngredietByIdAction(Id);
            public AdminResp DeleteIngredient(int Id) => _api.DeleteIngredientAction(Id);
        }
        //--------------------------------------- Dishes --------------------------------------------------//

        public IDish Dish => new DishWrapper(_dishApi);
        private class DishWrapper : IDish
        {
            private readonly DishApi _api;

            public DishWrapper(DishApi api) => _api = api;

            public AdminResp EditDish(DishDTO dish) => _api.EditDishAction(dish);
            public List<DishDTO> GetAllDishes() => _api.GetAllDishesAction();
            public AdminResp AddDish(DishDTO dish) => _api.AddDishAction(dish);
            public DishDTO GetDishById(int Id) => _api.GetDishByIdAction(Id);
            public AdminResp DeleteDish(int Id) => _api.DeleteDishAction(Id);
        }

        //--------------------------------------- Tables --------------------------------------------------//

        public ITable Table => new TableWrapper(_tableApi);
        private class TableWrapper : ITable
        {
            private readonly TableApi _api;

            public TableWrapper(TableApi api) => _api = api;
            public AdminResp AddTable(TableDTO Table) => _api.AddTable(Table);
            public AdminResp EditTable(TableDTO Table) => _api.EditTable(Table);
            public AdminResp DeleteTable(int Id) => _api.DeleteTable(Id);
            public List<TableDTO> GetAllTables() => _api.GetAllTables();
            public TableDTO GetTableById(int Id) => _api.GetTableById(Id);
        }
    }
}
