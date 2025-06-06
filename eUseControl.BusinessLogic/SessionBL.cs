﻿using System;
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
        public EmpDTO GetUserByName(string username) => GetUserByNameAction(username);
        public LoginResp Login(UserDTO user) => LoginAction(user);
        public RegEmpResp DeleteUser(string userName) => DeleteUserAction(userName);
        public HttpCookie GenCookie(string loginCredential) => Cookie(loginCredential);
        public LoginResp Edit(UserDTO user) => EditAction(user);
        public RegEmpResp RegisterEmployee(UserDTO data) =>RegisterEmployeeAction(data);

        public EmpDTO GetUserByCookie(string apiCookieValue) => GetUserByCookieAction(apiCookieValue);


    }
}
