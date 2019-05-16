using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CandyShop.WebUI.Infrastructure.Abstract;
using System.Web.Security;

namespace CandyShop.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        public bool Authentificate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
                FormsAuthentication.SetAuthCookie(username, false);
            return result;
        }
    }
}