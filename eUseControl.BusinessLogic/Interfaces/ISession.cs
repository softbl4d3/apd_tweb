using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.Resps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface ISession
    {
        RegEmpResp RegisterEmployee(UserDTO data);

        List<EmpDTO> GetAllEmployee();
        bool Login(UserDTO data);
    }
}
