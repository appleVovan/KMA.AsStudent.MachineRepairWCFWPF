using System;
using MacineRepairTool.DBConnector;
using MacineRepairTool.Models;

namespace Controller
{
    public static class LoginController
    {
        //User can enter name and password and click login
        //After login you are redirected to AdminController or ClientController using GetUserAndRedirect
        public static User CheckLoginAndPassword(string login, string password)
        {
            //EntityWrapper.CreateDefaultDB();
            var encriptedPassword = User.Encrypt(password);
            return EntityWrapper.GetModel<User>(t => t.Password == encriptedPassword && t.Login == login);
        }

        public static bool GetUserAndRedirect(string login, string password)
        {

            var user = CheckLoginAndPassword(login, password);
            if (user == null)
            {
                //TODO Show message "Login or Password is incorect"
                return false;
            }
            switch (user.UserType)
            {
                case UserType.Client:
                    ClientController.Init(user as Client);
                    break;
                case UserType.Partner:
                    AdminController.Init(user);
                    break;
                default:
                    //TODO Show message "Unknown type of user"
                    return false;
            }
            return true;
        }
    }
}
