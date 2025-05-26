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
using eUseControl.Helpers;
using System.Web;
using static System.Collections.Specialized.BitVector32;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Net;
using eUseControl.Domain.Enums;
namespace eUseControl.BusinessLogic.Core
{
    public class UserApi 
    {
        internal RegEmpResp RegisterEmployeeAction(UserDTO data) 
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
                    Password = LoginHelper.HashGen(data.Password),
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
        internal LoginResp EditAction(UserDTO user)
        {
            UserDbTable userDb;
            try
            {
                using (var context = new UserContext())
                {
                    userDb = context.Users.FirstOrDefault(u => u.UserName == user.UserName);
                }
                if (userDb == null)
                {
                    return new LoginResp { Status = false, Message = "User not found" };
                }
            }
            catch (Exception ex)
            {
                return new LoginResp
                {
                    Status = false,
                    Message = $"err : {ex.Message}",
                };
            }
            using (var context = new UserContext())
            {
                userDb.Role = user.Role;
                userDb.UserName = user.UserName;
                userDb.Password = LoginHelper.HashGen(user.Password);
                userDb.UpdatedAt = DateTime.Now;
                context.Entry(userDb).State = EntityState.Modified;
                context.SaveChanges();
            }
            return new LoginResp { Status = true, Role = userDb.Role };
        }
        internal EmpDTO GetUserByNameAction(string username)
        {
            UserDbTable userDb;
            try
            {
                using (var context = new UserContext())
                {
                    userDb = context.Users.FirstOrDefault(u => u.UserName == username);
                }
                if (userDb == null)
                {
                    return new EmpDTO
                    {
                        UserName = null,
                        Role = URole.None,
                    };
                }
            }
            catch (Exception ex)
            {
                return new EmpDTO
                {
                    UserName = null,
                    Role = URole.None,
                };
            }
            return new EmpDTO
            {
                UserName = userDb.UserName,
                Role = userDb.Role,
            };
        }
        internal List<EmpDTO> GetAllEmployeeAction()
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
        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var context = new UserContext())
            {
                SessionDbTable curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in context.Sessions where e.UserName == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in context.Sessions where e.UserName == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new UserContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    context.Sessions.Add(new SessionDbTable
                    {
                        UserName = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    context.SaveChanges();
                }
            }

            return apiCookie;
        }
        internal LoginResp LoginAction(UserDTO user)
        {
            UserDbTable userDb;
            try 
            {
                var pass = LoginHelper.HashGen(user.Password);
                using (var context = new UserContext())
                {
                    userDb = context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == pass);
                }
                if (userDb == null)
                {
                    return new LoginResp { Status = false, Message = "The Username or Password is Incorrect" };
                }
            }
            catch(Exception ex) 
            {
                return new LoginResp
                {
                    Status = false,
                    Message = $"err : {ex.Message}",
                };
            }



            return new LoginResp { Status = true, Role = userDb.Role};
        }
        internal RegEmpResp DeleteUserAction(string username)
        {
            UserDbTable userDb;

            using (var context = new UserContext())
            {
                userDb = context.Users.FirstOrDefault(u => u.UserName == username);
            }

            if (userDb == null)
            {
                return new RegEmpResp { Message = "User not found", Status = false };
            }

            using (var context = new UserContext())
            {
                context.Users.Attach(userDb); 
                context.Users.Remove(userDb);
                context.SaveChanges();
            }

            return new RegEmpResp { Message = "User deleted", Status = true };
        }
        internal EmpDTO GetUserByCookieAction(string cookie)
        {
            SessionDbTable session;
            UserDbTable cu;

            using (var db = new UserContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {

                cu = db.Users.FirstOrDefault(u => u.UserName == session.UserName);

            }

            if (cu == null) return null;


            EmpDTO EmpDTO = new EmpDTO
            {
                UserName = cu.UserName,
                Role = cu.Role

            };

            return EmpDTO;
        }
    }
}
