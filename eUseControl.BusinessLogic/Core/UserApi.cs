using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic.DBModel;
using System.Net.Http;
namespace eUseControl.BusinessLogic.Core
{
    public class UserApi : ISession
    {
        public HttpResponseMessage RegisterEmpoyee(UserDTO data)
        {
            using (var context = new UserContext())
            {
                var user = new UserDbTable
                {
                    UserName = data.UserName,
                    Password = data.Password,
                    Role = data.Role,
                    CreatedAt= DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                context.Users.Add(user);
                context.SaveChanges();
            }
            return null;
        }
        public bool Login(UserDTO data)
        {
            using (var context = new UserContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserName == data.UserName && u.Password == data.Password);
                if (user != null)
                {
                    new UserDTO
                    {
                        UserName = user.UserName,
                        Password = user.Password,
                        Role = user.Role
                    };
                }
                return false;
            }
        }
    }
}
