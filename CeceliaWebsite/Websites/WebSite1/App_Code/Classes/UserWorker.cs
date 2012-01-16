using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserWorker
/// </summary>
/// 
namespace Cecelia {
    public class UserWorker {

        public UserWorker() {
            //
            // TODO: Add constructor logic here
            //
        }

        public Users Login(string username, string password) {
            string encPassword = StaticMethods.EncryptPassword(password);
            CeceliaDataProvider dp = new CeceliaDataProvider();

            Users user = dp.Login(username);
            if (user.Password == encPassword) {
                return user;
            } else {
                return null;
            }

        }
        public bool Logout() {
            return true;
        }
        public bool AddUser(Users user) {
            CeceliaDataProvider dp = new CeceliaDataProvider();
            user.Password = StaticMethods.EncryptPassword(user.Password);
            return dp.AddUser(user);
        }
    }
}