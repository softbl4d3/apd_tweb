using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic.Core;
using eUseControl.Domain.Entities.Resps;
using eUseControl.Domain.Entities.DTO;

namespace eUseControl.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public RegEmpResp RegisterEmployee(UserDTO data)
        {
            return RegisterEmployeeAction(data);
        }
        public List<EmpDTO> GetAllEmployee()
        {
            return GetAllEmployeeAction();
        }
    }
}
