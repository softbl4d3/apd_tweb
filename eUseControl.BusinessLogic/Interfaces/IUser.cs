using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Resps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface IUser
    {
        RegEmpResp RegisterEmployee(UserDTO data);
        List<EmpDTO> GetAllEmployee();

        RegEmpResp DeleteUser(string userName);
        LoginResp Login(UserDTO user);
        EmpDTO GetUserByCookie(string apiCookieValue);
    }
}
