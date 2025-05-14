using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.Interfaces;
using System.Web.UI.WebControls;

namespace eUseControl.Web.Models
{
    public class MenuViewModel
    {
            public TableViewModel Table { get; set; }
            public List<DishViewModel> Dishes { get; set; }

    }
}
