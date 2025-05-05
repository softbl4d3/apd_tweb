using System;
using System.Web;
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
    public class SessionBL : UserApi, IUser
    {
        public List<EmpDTO> GetAllEmployee() => GetAllEmployeeAction();

        public AdminResp Login(UserDTO user) => LoginAction(user);

        public HttpCookie GenCookie(string loginCredential) => Cookie(loginCredential);

        public RegEmpResp RegisterEmployee(UserDTO data) =>RegisterEmployeeAction(data);
        
    }
}
