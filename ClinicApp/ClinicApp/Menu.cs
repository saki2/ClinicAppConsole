using ClinicApp.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ClinicApp
{
    public enum Role
    {
        Admin, Secretary, Doctor, Patient,
    }
    public class Menu
    {
        // Shows the user options for signing up or signing in
        public static void InitialDialog()
        {
            Console.Write("To sing up write \"R\", to sign in write \"L\", to exit write \"X\": \n (R/L/X) >> ");
            string choice = Console.ReadLine();
            if (choice.ToUpper() == "R")
            {
                RegistrationDialog();
            }
            else if (choice.ToUpper() == "L")
            {
                LoginDialog();
            }
            else if (choice.ToUpper() == "X") {
                return;

            } else {
                Console.WriteLine("Invalid option, try again");
                InitialDialog();
            }
        }

        //==================================================================================================================
        // Registration functions
        static void RegistrationDialog()
        {
            Console.Write("Username: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Chose role: Doctor(d), Admin(a), Secretary(s), Patient(p)");
            Console.Write("(d/a/s/p) >> ");
            string roleStr = Console.ReadLine();
            int role;

           
            switch (roleStr.ToLower())
            {
                case "a": role = 0; break;
                case "s": role = 1; break;
                case "d": role = 2; break;
                case "p": role = 3; break;
                default: 
                    Console.WriteLine("\nWrong input, try again");
                    InitialDialog();
                    return;
            }

            Console.Write("Password: ");
            string password = MaskPassword();
            Console.Write("\nRepeat password: ");
            string passwordCheck = MaskPassword();

           
            bool valid = ValidateRegistration(userName, password, passwordCheck);
            if (valid)
            {
                Console.WriteLine($"\nWelcome {userName}");
                string lastLine = File.ReadLines(SystemFunctions.UsersFilePath).Last();
                string[] tmp = lastLine.Split('|');
                int id = Convert.ToInt32(tmp[0]) + 1;

                string hashedPassword = HashPassword(password);

                // Creates user and adds them to the dictionary Users in SystemFunctions
                User newUser = new User(id, userName, hashedPassword, role);
                SystemFunctions.Users.Add(userName, newUser);

                // Writes the information about the user to the database
                string newLine = Convert.ToString(id) + "|" + userName + "|" + hashedPassword + "|" + Convert.ToString(role);
                using (StreamWriter sw = File.AppendText(SystemFunctions.UsersFilePath))
                {
                    sw.WriteLine(newLine);
                }

                // Shows dialog for the role of the user
                RoleDialog(newUser);
            }
            else
            {
                InitialDialog();
                return;
            }

        }

        private static bool ValidateRegistration(string userName, string password, string passwordCheck)
        {
            if (passwordCheck != password)
            {
                Console.WriteLine("Passwords don't match, try again");
                return false;
            }
            User tmp = null;
            if (SystemFunctions.Users.TryGetValue(userName, out tmp))
            {
                Console.WriteLine("Username taken, try again");
                return false;
            }
            // TODO: add password checks, for example if password lenght < 8 return false etc.
            return true;
        }

        // TODO: each member should implement their each version for registration because each
        // type of user requires different information to be collected
        // Idea: for each registration dialog create a registration validation method
        private static void RegisterAdmin() { }
        private static void RegisterSecretary() { }
        private static void RegisterDoctor() { }
        private static void RegisterPatient() { }




        //==================================================================================================================
        // Login functions


        static void LoginDialog()
        {
            Console.Write("Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string password = MaskPassword();
            User user = null;
            if (SystemFunctions.Users.TryGetValue(userName, out user))
            {
                if (user.Password == HashPassword(password))
                {
                    Console.WriteLine($"\nWelcome {userName}");
                    RoleDialog(user);
                }
                else
                {
                    Console.WriteLine("\nIncorrect password, try again");
                    InitialDialog();
                }
            }
            else
            {
                Console.WriteLine("\nUser does not exist, try again");
               InitialDialog();
            }

        }
        //==================================================================================================================
        // Other functions


        private static void RoleDialog(User user)
        {
            switch (user.Role)
            {
                case Role.Admin: RegisterAdmin(); Admin.AdminMenu(); return;
                case Role.Secretary: RegisterSecretary(); Secretary.SecretaryMenu(); return;
                case Role.Doctor: RegisterDoctor(); Doctor.DoctorMenu(); return;
                case Role.Patient: RegisterPatient(); Patient.PatientMenu(); return;
                default: return;
            }
        }

        private static string MaskPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            while (true)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
                else if (key.Key == ConsoleKey.Enter && password.Length > 0)
                {
                    return password;
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
        }

        protected static string HashPassword(string password)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}

