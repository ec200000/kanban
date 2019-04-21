using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;


namespace Kanban.BL
{
    public class User
    {
        [JsonProperty]
        private string userEmail;//writing private fields to JSON
        [JsonProperty]
        private string userPassword;//writing private fields to JSON
        public Board KanBanBoard;
        public bool isconnected;

        public User(string userEmail, string userPassword, Board KanBanBoard, bool isconnected) //constructor
        {
            this.userEmail = userEmail;
            this.userPassword = userPassword;
            this.KanBanBoard = KanBanBoard;
            this.isconnected = isconnected;
        }

        public bool CreateTask(string title, string description, string dueDate, string currCol)
        {
            Dictionary<string, Column> kanbancolumns = KanBanBoard.boardColumns;
            Validation val = new Validation();
            Column col = kanbancolumns[currCol];
            if (val.validateTaskInfo(title, description, dueDate) & val.checkSpaceInColumn(col)) //checks if the task info is vaild and if there is space for another task in the column and create new task
            {
                Task task = new Task(title, description, dueDate);
                if(!val.checkCreationTimeExsictence(task.GetCreationTime()))//after creating task checking that it has creation time
                {
                    FileLogger.WriteErrorToLog("no creation time in new task");
                    return false;
                }
                col.AddTask(task); //add task to backlog column
                col.SortByCreationTime();
                kanbancolumns.Remove(currCol); //remove previous column
                kanbancolumns.Add(currCol, col); //add new column
                FileLogger.write(Authantication.userRegisterd); //update dictionary
                return true;
            }
            else
            {
                return false;
            }
        }
        
       public bool PromoteTaskToNextPhase(Task task, string source, string target)
        {
            if(task != null)
            {
                Dictionary<string, Column> kanbancolumns = KanBanBoard.boardColumns;
                Validation val = new Validation();
                int index = kanbancolumns[source].IsTaskHere(task);
                if (index != -1) //check if the task is in the column 
                {

                    if (val.checkSpaceInColumn(kanbancolumns[target])) //if there is space in the next Column- promote task to the next one
                    {
                        Column sourceCol = kanbancolumns[source];
                        sourceCol.RemoveTask(task); //remove task from source column
                        kanbancolumns.Remove(source); //remove previous column
                        sourceCol.SortByCreationTime();
                        kanbancolumns.Add(source, sourceCol); //add new column 
                        Column targetCol = kanbancolumns[target];
                        targetCol.AddTask(task);  //add task to target column
                        targetCol.SortByCreationTime();
                        kanbancolumns.Remove(target); //remove previous column
                        kanbancolumns.Add(target, targetCol); //add new column
                        FileLogger.write(Authantication.userRegisterd); //write to file
                        return true;
                    }
                    else return false;//there is no space in the target column
                }

            }
            else
            {
                FileLogger.WriteNullObjectExceptionToLogger<Task>("PromoteTaskToNextPhase function"); 
            }
            return false; //if the task is in last column there is no next column 

        }

        public string getEmail()
        {
            return this.userEmail;
        }

        public string GetPassword()//just for the test function
        {
            return this.userPassword;
        }

        public bool changeDescription(Task task, string newDes, string currCol)
        {
            Dictionary<string, Column> kanbancolumns = KanBanBoard.boardColumns;
            Column currentColumn;
            Validation val = new Validation();
            if (task != null)
            {
                if (val.checkDescriptionLength(newDes)) //if the new description length is vaild - change the old description and return true  
                {
                    int index = kanbancolumns[currCol].IsTaskHere(task);
                    if (index != -1)
                    {
                        currentColumn = kanbancolumns[currCol];

                        kanbancolumns.Remove(currCol); //remove previous column
                        Task[] tasks = currentColumn.getTasks();
                        tasks[index].SetDescription(newDes); //update task
                        kanbancolumns.Add(currCol, currentColumn); //add new column
                        FileLogger.write(Authantication.userRegisterd); //write to file
                        return true;
                    }
                    else
                    {
                        string msg = "the task does not appear in any of the board's columns OR te user wanted to change task in DONE column!";
                        FileLogger.WriteErrorToLog(msg);
                        return false; //if the task is not in the columns return false
                    }
                }
            }
            else
            {
                FileLogger.WriteNullObjectExceptionToLogger<Task>("changeDescription function");
            }
            return false; //if the new description is invaild - return false
        }

        public bool changeTitle(Task task, string newTitle, string currCol) //if the new title length is vaild - change the old title and return true 
        {
            Dictionary<string, Column> kanbancolumns = KanBanBoard.boardColumns;
            Column currentColumn;
            Validation val = new Validation();
            if (task != null)
            {
                if (val.checkTitleLength(newTitle))
                {
                    int index = kanbancolumns[currCol].IsTaskHere(task);
                    if (index != -1)
                    {
                        currentColumn = kanbancolumns[currCol];
                        kanbancolumns.Remove(currCol); //remove previous column
                        Task[] tasks = currentColumn.getTasks();
                        tasks[index].SetTitle(newTitle); //update task
                        kanbancolumns.Add(currCol, currentColumn); //add new column
                        FileLogger.write(Authantication.userRegisterd); //write to file 
                        return true;
                    }
                    else
                    {
                        string msg = "the task does not appear in any of the board's columns OR te user wanted to change task in DONE column!";
                        FileLogger.WriteErrorToLog(msg);
                        return false; //if the task is not in the columns return false
                    }
                }
            }
            else
            {
                FileLogger.WriteNullObjectExceptionToLogger<Task>("changeTitle function");
            }
            return false; //if the new title is invaild - return false

        }
        public bool changeDueDate(Task task, string newDate, string currCol)
        {
            Dictionary<string, Column> kanbancolumns = KanBanBoard.boardColumns;
            Column currentColumn;
            Validation val = new Validation();
            if (task != null)
            {
                if (val.checkDueDateExsictence(newDate)) //if the new date is vaild - change the old one and return true
                {
                    int index = kanbancolumns[currCol].IsTaskHere(task);
                    if (index != -1)
                    {
                        currentColumn = kanbancolumns[currCol];
                        kanbancolumns.Remove(currCol); //remove previous column
                        Task[] tasks = currentColumn.getTasks();
                        tasks[index].SetDueDate(newDate); //update task
                        kanbancolumns.Add(currCol, currentColumn); //add new column
                        FileLogger.write(Authantication.userRegisterd);
                        return true;
                    }
                    else
                    {
                        string msg = "the task does not appear in any of the board's columns OR te user wanted to change task in DONE column!";
                        FileLogger.WriteErrorToLog(msg);
                        return false; //if the task is not in the columns return false 
                    }

                }
            }
            else
            {
                FileLogger.WriteNullObjectExceptionToLogger<Task>("changeDueDate function");
            }

            return false; //if the new date is invaild - return false
        }

    }
}
