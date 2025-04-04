using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.RespStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.Interfaces
{
    public interface ISession
    {
        RespUserActionStatus RegisterEmpoyee(UserDTO data);
        bool Login(UserDTO data);
    }
}
