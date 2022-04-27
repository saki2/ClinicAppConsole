using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicApp.Users
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }
        public User(int id, string userName, string password, int role){
            ID = id;
            UserName = userName;
            Password = password;
            Enum.TryParse(role.ToString(), out Role value);
            Role = value;
            }
    }
}
