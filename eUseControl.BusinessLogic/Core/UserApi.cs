using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Resps;
using System.Net.Http;
namespace eUseControl.BusinessLogic.Core
{
    public class UserApi 
    {
        public RegEmpResp RegisterEmployeeAction(UserDTO data)
        {
            try
            {
                UserDbTable exisingtuser;
                using (var context = new UserContext())
                {
                    exisingtuser = context.Users.FirstOrDefault(u => u.UserName == data.UserName);
                }
                if (exisingtuser != null)
                {
                    return new RegEmpResp
                    {
                        Message = "user already exist",
                        Status = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new RegEmpResp
                {
                    Message = $"Ошибка при регистрации пользователя: {ex.Message}",
                    Status = false
                };
            }
            try
            {
                var user = new UserDbTable
                {
                    UserName = data.UserName,
                    Password = data.Password,
                    Role = data.Role,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                using (var context = new UserContext())
                {
           
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                return new RegEmpResp
                {
                    Message = "Пользователь успешно зарегистрирован",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new RegEmpResp
                {
                    Message = $"Ошибка при регистрации пользователя: {ex.Message}",
                    Status = false
                };
            }
        }

        public List<EmpDTO> GetAllEmployeeAction()
        {
            List<EmpDTO> employeesDTO;

            using (var context = new UserContext())
            {
                var employees = context.Users
                    .Select(u => new
                    {
                        u.UserName,
                        u.Role
                    })
                    .ToList();

                employeesDTO = employees
                    .Select(e => new EmpDTO
                    {
                        UserName = e.UserName,
                        Role = e.Role
                    })
                    .ToList();
            }

            return employeesDTO;
        }
    }
}
