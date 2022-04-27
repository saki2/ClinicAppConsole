 ﻿using System;
using System.Collections.Generic;
using System.Text;
using ClinicApp.Admin;

namespace ClinicApp.Users
{
    public class Admin
    {
        public static void AdminMenu() 
        {
            while (true) { 
                Console.WriteLine("Admin menu, choose an option");
                Console.WriteLine("1. Manage Clinic Rooms");
                Console.WriteLine("2. Manage Clinic Equipment");
                Console.WriteLine("X to exit");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "1":
                        RoomManagmentMenu();
                        break;
                    case "2":
                        EquipmentManagmentMenu();
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again");
                        break;
                }
            }
        }
        public static void RoomManagmentMenu() 
        {
            while (true) { 
                Console.WriteLine("Manage Rooms");
                Console.WriteLine("1. List all rooms");
                Console.WriteLine("2. Add new room");
                Console.WriteLine("3. Edit an existing room");
                Console.WriteLine("4. Delete a room by ID");
                Console.WriteLine("X to return");
                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "1":
                        ListAllRooms();
                        break;
                    case "2":
                        AddNewRoom();
                        break;
                    case "3":
                        EditRoom();
                        break;
                    case "4":
                        DeleteRoom();
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again");
                        break;
                }
            }
        }
        public static void ListAllRooms() 
        {
            Console.WriteLine("ID | NAME | TYPE");
            foreach (ClinicRoom room in ClinicRoomService.ClinicRooms)
            {
                Console.WriteLine(room.Id + " " + room.Name + " " + room.Type);
            }
        }
        public static void AddNewRoom() 
        {
            string name;
            string type;
            RoomType roomType;
            while (true)
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
                if (name.Contains("|")) 
                {
                    Console.WriteLine("Invalid option, name cannot contain |, try again");
                }
                else { break; }
            }
            while (true) { 
                Console.Write("\nChoose Type (1 for Operations, 2 for Examinations, 3 for Waiting): ");
                type = Console.ReadLine();
                if (type == "1")
                {
                    roomType = RoomType.Operations;
                    break;
                }
                else if (type == "2")
                {
                    roomType = RoomType.Examinations; break;
                }
                else if (type == "3")
                {
                    roomType = RoomType.Waiting;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option, try again");
                    type = Console.ReadLine();
                }
                        
                }
            ClinicRoom room = new ClinicRoom { Name = name, Type = roomType };
            ClinicRoomService.Add(room);
            }
        public static void EditRoom()
        {
            ClinicRoom room;
            string name;
            string type;
            RoomType roomType;
            while (true) 
            {
                Console.WriteLine("Enter ID of the room you want to Edit");
                int id = Convert.ToInt32(Console.ReadLine());
                room = ClinicRoomService.Get(id);
            if (room is null)
            {
                Console.WriteLine("Invalid option, try again");
            }
            else break;
            }
            while (true)
            {
                Console.WriteLine("Enter new name, leave empty for old");
                name = Console.ReadLine();
                if (name.Contains("|"))
                {
                    Console.WriteLine("Invalid option, name cannot contain |, try again");
                }
                else if (name is null)
                {
                    name = room.Name;
                }
                else { break; }
            }
            while (true)
            {
                Console.Write("\nChoose Type (1 for Operations, 2 for Examinations, 3 for Waiting), leave empty for old: ");
                type = Console.ReadLine();
                if (type == "1")
                {
                    roomType = RoomType.Operations;
                    break;
                }
                else if (type == "2")
                {
                    roomType = RoomType.Examinations; break;
                }
                else if (type == "3")
                {
                    roomType = RoomType.Waiting;
                    break;
                }
                else if (type == "") 
                {
                    roomType = room.Type;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option, try again");
                }

            }
            ClinicRoomService.Update(room.Id, name, roomType);
        }
        public static void DeleteRoom()
        {
            ClinicRoom room;
            int id;
            while (true)
            {
                Console.WriteLine("Enter ID of the room you want to Delete");
                id = Convert.ToInt32(Console.ReadLine());
                room = ClinicRoomService.Get(id);
                if (room is null)
                {
                    Console.WriteLine("Invalid option, try again");
                }
                else break;
            }
            ClinicRoomService.Delete(id);
        }
        public static void EquipmentManagmentMenu() 
        {
            Console.WriteLine("Manage Equipment");
        }
    }
}
}