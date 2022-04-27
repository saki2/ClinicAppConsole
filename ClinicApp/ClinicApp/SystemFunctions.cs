using ClinicApp.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClinicApp
{
    public class SystemFunctions
    {
        // TODO: Create a dictionary of Admins, Secretaries, Doctors and Patients. 
        // TODO: Create a txt file in Data for each user type
        
        // Dictionary of users created for faster and easier acces to information from the database
        public static Dictionary<string, User> Users { get; set; } = new Dictionary<string, User>();

        // User file path may change in release mode, this is the file path in debug mode

        public static string UsersFilePath =  "../../../Data/users.txt";
        public static void LoadData()
        {

            using (StreamReader reader = new StreamReader(UsersFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    User user = ParseUser(line);
                    Users.Add(user.UserName, user);
                }
            }
        }

        private static User ParseUser(string line)
        {
            string[] parameters = line.Split('|');
            User user = new User(Convert.ToInt32(parameters[0]), parameters[1], parameters[2], Convert.ToInt32(parameters[3]));
            return user;
        }
    }
}
