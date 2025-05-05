using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eUseControl.Domain.Entities.DTO;
using eUseControl.Domain.Entities.User;

namespace eUseControl.Web.Extension
{
    public static class HttpContextExtensions
    {
        public static UserDTO GetMySessionObject(this HttpContext current)
        {
            return (UserDTO)current?.Session["__SessionObject"];
        }

        public static void SetMySessionObject(this HttpContext current, UserDTO profile)
        {
            current.Session.Add("__SessionObject", profile);
        }
    }
}