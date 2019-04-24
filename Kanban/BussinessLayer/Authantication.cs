
﻿using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kanban.BL
{
    [Serializable]
    class Authantication
    {
        public static Dictionary<string,User> userRegisterd = new Dictionary<string, User>();
        public static Validation v = new Validation();

        public User signUp(string email, string password)
        {
            if (v.validateUserInfo(email, password))
            {
                User newUser = new User(email, password,new Board(), false);
                userRegisterd = FileLogger.Read();
                if(userRegisterd == null)
                {
                    userRegisterd = new Dictionary<string, User>();
                }
                userRegisterd.Add(email, newUser);//saving the user
                FileLogger.write(userRegisterd);
                string msg = "new user registerd - " + email;
                FileLogger.WriteInformationToLog(msg);
                return newUser;
            }
            return null;
        }

        public User login(string email, string password)
        {
            if (v.validateCredentials(email,password))
            {
                try
                {
                    userRegisterd = FileLogger.Read();
                    User userInSystem = userRegisterd[email];
                    userInSystem.isconnected = true;
                    string msg = email + " logged in to the system";
                    FileLogger.WriteInformationToLog(msg);
                    return userInSystem;
                }catch(Exception ex) //when we will write the UI it will apear on the user's screen
                {
                    Log.Error(ex, "exception in login function");
                }
                
            }
            return null;
        }

        public static void logOut(User userToLogOut)
        {
            userToLogOut.isconnected = false;
            string msg = userToLogOut.getEmail() + " logged out from the to the system";
            FileLogger.WriteInformationToLog(msg);
        }
    }
}