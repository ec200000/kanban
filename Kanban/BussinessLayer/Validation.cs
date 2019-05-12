using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Serilog;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Kanban.BL
{
    class Validation
    {
        private const int DESCRIPTION_LENGTH = 300;
        private const int TITLE_LENGTH = 50;
        private const int MAX_PASSWORD_LENGTH = 20;
        private const int MIN_PASSWORD_LENGTH = 4;

        /* this function is for the login validation - it validates that the user is registered and 
         * his login details are correct (the email and the password are matching)*/
        public bool validateCredentials(string email, string password)
        {
            if (!isCredaentialsValid(email, password))
            {
                return false;
            }
            return true;
        }

        //this is an helper function - it checks if the user is registerd, which means, it checks if the user info is 
        //in UserData.txt file
        private bool isCredaentialsValid(string email, string password)
        {
            string path = Directory.GetCurrentDirectory() + "\\UserData.txt";
            try
            {
                Dictionary<string, User> usersDictionary = FileLogger.Read();
                if (usersDictionary != null && usersDictionary.Count != 0)
                {
                    if (email == null || password == null || !usersDictionary.ContainsKey(email))
                    {
                        FileLogger.WriteErrorToLog("The user is NOT REGISTERD!");
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex.ToString() + " Something went wrong in function isCredaentialsValid()!");
                return false;
            }
            return true;
        }

        /*In signup - user can register with unique email, password that includes at least one capital character, one small character and a number.*/
        public bool validateUserInfo(string email, string password)
        {
            if (email != null && isEmailAlreadyInSystem(email))
            {
                if (password != null && !passwordValidation(password) | !IsValidEmail(email))
                {
                    return false;
                }
            }
            else
                return false;
            return true;
        }

        //checking that the user's email is unique(not in the system)
        private bool isEmailAlreadyInSystem(string email)
        {
            string path = Directory.GetCurrentDirectory() + "\\UserData.txt";
            try
            {
                Dictionary<string, User> usersDictionary = FileLogger.Read();
                if (usersDictionary != null && usersDictionary.Count != 0)
                {
                    if (usersDictionary.ContainsKey(email))
                    {
                        FileLogger.WriteErrorToLog("The email is already in the system!");
                        MessageBox.Show("The email is already in the system!");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex.ToString() + " Something went wrong in function isEmailAlreadyInSystem()!");
                MessageBox.Show(" Something went wrong in function isEmailAlreadyInSystem()!");
                return false;
            }
            return true;
        }

        //checks that the string that the user entered in the email field is in email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex + " in isValidEmailFunction");
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //checking that the password includes at least one capital character, one small character and a number
        private bool passwordValidation(string password)
        {
            bool capitalL = false, lowerL = false, number = false;
            if (password.Length > MAX_PASSWORD_LENGTH | password.Length < MIN_PASSWORD_LENGTH)//incorrect length
            {
                string msg = "While registering, user's password is not in the right length - must be " + MIN_PASSWORD_LENGTH + "-" + MAX_PASSWORD_LENGTH + " chars!";
                FileLogger.WriteErrorToLog(msg);
                MessageBox.Show(msg);
                return false;
            }
            for (int i = 0; i < password.Length; i++)
            {
                if (capitalL & lowerL & number) //the password contains at least one lower char, upper and number
                    break;
                if (password.ElementAt(i) >= 'a' & password.ElementAt(i) <= 'z')
                {
                    lowerL = true;
                }
                if (password.ElementAt(i) >= 'A' & password.ElementAt(i) <= 'Z')
                {
                    capitalL = true;
                }
                if (password.ElementAt(i) >= '0' & password.ElementAt(i) <= '9')
                {
                    number = true;
                }
            }
            if (!capitalL | !lowerL | !number)//the password does not at least one of the three needed charcters
            {
                FileLogger.WriteErrorToLog("While registering, user's password does not contains upper case char OR lower case char OR a number!");
                return false;
            }
            return true;
        }

        //this function validates the task info - description, title, due date and creation time
        //A task must have everything except description
        public bool validateTaskInfo(string title, string description, DateTime dueDate)
        {
            if (!checkDescriptionLength(description))
                return false;
            if (!checkTitleLength(title))
                return false;
            if (!checkDueDateExsictence(dueDate))
                return false;
            return true;
        }

        //every task must have a title
        public bool checkTitleLength(string title)
        {
            if (title == null || title.Length > TITLE_LENGTH | title.Length == 0) //title length is over the limit or does not exists
            {
                string msg = "While task creating, task's description length must be at most " + TITLE_LENGTH + "characters!";
                FileLogger.WriteErrorToLog(msg);
                return false;
            }

            return true;
        }

        //description has a limit of characters 
        public bool checkDescriptionLength(string description)
        {
            if (description == null || description.Length > DESCRIPTION_LENGTH)//description length is over the limit
            {
                string msg = "While task creating, task's description length must be at most " + DESCRIPTION_LENGTH + "characters!";
                FileLogger.WriteErrorToLog(msg);
                return false;
            }

            return true;
        }

        //every task must have due date
        public bool checkDueDateExsictence(DateTime dueDate)
        {
            if (dueDate == null || dueDate.ToString().Length == 0)
            {
                FileLogger.WriteErrorToLog("While task creating, task must have due date!");
                return false;
            }

            return true;
        }

        //Every task must have creation time
        public bool checkCreationTimeExsictence(string creationTime)
        {
            if (creationTime == null || creationTime.Length == 0)
            {
                FileLogger.WriteErrorToLog("While task creating, task must have creation time!");
                return false;
            }

            return true;
        }

        
        public bool checkSpaceInColumn(Column column)
        {
            // >=, because if for example we have 10 tasks and than the user limited to 5, we can't add more even though the number of tasks in column doesn't equal to the limit
            if (column == null || column.GetNumOfTasks() >= column.maxNumOfTaskInColumn)
            {
                FileLogger.WriteErrorToLog("There is no space in the column!");
                return false;
            }
            return true;
        }

        //checks task's parameters
        public bool validateTaskInfoIsNotNull(Task task)
        {
            if (task.GetTitle() == null || task.GetDescription() == null || task.GetDueDate() == null || task.GetCreationTime() == null)
            {
                FileLogger.WriteErrorToLog("There are some of the task's parameters that are null!");
                return false;
            }
            return true;
        }

        //checks if the column is already exsits
        public bool validateColumnInfo(string title, Board b)
        {
            Dictionary<string, Column> d = b.boardColumns;
            if (title.Equals(""))
            {
                FileLogger.WriteErrorToLog("Column name can't be empty!");
                return false;
            }
            foreach (string s in d.Keys)
            {
                if (s.Equals(title))
                {
                    return false;
                }
                    
            }
            return true;
        }
        /*public static void Main()
        {
            //Console.WriteLine(isCredaentialsValid("danaK@g", "123"));
            //Console.WriteLine(validateUserInfo("alex@g", "12345Ap&"));
            //Console.WriteLine(validateTaskInfo("dfd", "good", "04/04/2019", "10:41:02"));
            Console.ReadKey();
        }*/
    }
}