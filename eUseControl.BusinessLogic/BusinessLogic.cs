using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic;

namespace eUseControl.BusinessLogic
{
    public class BusinessLogic
    {

        public SessionBL GetSessionBL()
        {
            return new SessionBL();
        }

        public ITable GetTableBL()
        {
            return new TableBL();
        }

        public IDish GetDishBL()
        {
            return new DishBL();
        }

        public IIngredient GetIngredientBL()
        {
            return new IngridientBL(); 
        }

        public IOrder GetOrderBL()
        {
            return new OrderBL();
        }

    }
}
