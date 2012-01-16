using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
/// 
namespace Cecelia {
    public class Users {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
        public Role Role { get; set; }

        public Users() {

        }
        public Users(int Id, string userName, string password, string UserName, Role role) {
            this.Id = Id;
            this.UserName = userName;
            this.Password = password;
            this.UserName = userName;
            this.Role = role;
        }

        
    }

    public enum Role {
        Admin,
        User,
        Viewer,
    }
}