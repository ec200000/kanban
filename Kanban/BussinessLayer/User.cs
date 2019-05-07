﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Kanban.PresentationLayer.ViewModel;
using System.Windows;

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

        public bool CreateTask(string title, string description, DateTime dueDate, string currCol)
        {
            Dictionary<string, Column> kanbancolumns = KanBanBoard.boardColumns;
            //kanbancolumns.First();
            Validation val = new Validation();
            Column col = kanbancolumns[currCol];
            if (val.validateTaskInfo(title, description, dueDate) & val.checkSpaceInColumn(col)) //checks if the task info is vaild and if there is space for another task in the column and create new task
            {
                Task task = new Task(title, description, dueDate, currCol);
                if (!val.checkCreationTimeExsictence(task.GetCreationTime()))//after creating task checking that it has creation time
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

        public bool RemoveColumn(string colToRemove)
        {
            if (KanBanBoard.boardColumns.Count == 0)//there are no columns to erase
            {
                return false;
            }
            try
            {
                KanBanBoard.columnsOrder.Remove(colToRemove);
                KanBanBoard.boardColumns.Remove(colToRemove);
                FileLogger.write(Authantication.userRegisterd); //update dictionary
            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex + "in removeColumn function");
                return false;
            }
            return true;
        }

        public bool addNewColumnToBoard(string colBefore, string newCol)
        {
            try
            {
                if (KanBanBoard.boardColumns.ContainsKey(newCol))//can't add column with the same name
                {
                    FileLogger.WriteErrorToLog("can't create column because there is already one with the same name!");
                    return false;
                }
                if (colBefore.ToLower().Equals("empty"))//if the user wants to create a first new column
                {
                    KanBanBoard.columnsOrder.AddFirst(newCol);
                    KanBanBoard.boardColumns.Add(newCol, new Column());
                    FileLogger.write(Authantication.userRegisterd); //update dictionary
                }
                else
                {
                    LinkedListNode<string> colBeforeNode = KanBanBoard.columnsOrder.Find(colBefore);
                    if (colBeforeNode == null)//couldn't find the column that the user entered
                    {
                        FileLogger.WriteNullObjectExceptionToLogger<string>("function addNewColumnToBoard");
                        return false;
                    }
                    KanBanBoard.columnsOrder.AddAfter(colBeforeNode, newCol);
                    KanBanBoard.boardColumns.Add(newCol, new Column());
                    FileLogger.write(Authantication.userRegisterd); //update dictionary
                }

            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex + " in adaddNewColumnToBoard");
                return false;
            }
            return true;
        }

        public bool swapColumnsPosition(string colBefore, string colToMove)
        {
            try
            {
                if (colToMove == null | colBefore == null)
                {
                    FileLogger.WriteNullObjectExceptionToLogger<string>("swapColumnsPosition function");
                    return false;
                }
                RemoveColumn(colToMove);
                addNewColumnToBoard(colBefore, colToMove);
            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex + "in swapColumnsPosition function");
                return false;
            }

            return true;
        }

        public bool PromoteTaskToNextPhase(Task task, string source, string target)
        {
            if (task != null)
            {
                Dictionary<string, Column> kanbancolumns = KanBanBoard.boardColumns;
                Validation val = new Validation();
                if (kanbancolumns[source].tasks.Find(t => t.Equals(task)) != null) //check if the task is in the column 
                {
                    if (val.checkSpaceInColumn(kanbancolumns[target])) //if there is space in the next Column- promote task to the next one
                    {
                        MessageBox.Show("here");
                        Column sourceCol = kanbancolumns[source];
                        sourceCol.RemoveTask(task); //remove task from source column
                        kanbancolumns.Remove(source); //remove previous column
                        sourceCol.SortByCreationTime();
                        kanbancolumns.Add(source, sourceCol); //add new column 
                        Column targetCol = kanbancolumns[target];
                        task.currCol = target;
                        targetCol.AddTask(task);  //add task to target column
                        targetCol.SortByCreationTime();
                        kanbancolumns.Remove(target); //remove previous column
                        kanbancolumns.Add(target, targetCol); //add new column
                        FileLogger.write(Authantication.userRegisterd); //write to file
                        return true;
                    }
                    else return false;//there is no space in the target column
                }
                else return false;
            }
            else
            {
                FileLogger.WriteNullObjectExceptionToLogger<Task>("PromoteTaskToNextPhase function");
            }
            return false; //if the task is in last column there is no next column 

        }

        public string GetEmail()
        {
            return this.userEmail;
        }

        public string GetPassword()//just for the test function
        {
            return this.userPassword;
        }

        private bool DeleteTask(Task task)
        {
            if (task != null)
            {
                Dictionary<string, Column> kanbancolumns = KanBanBoard.boardColumns;
                Validation val = new Validation();
                int index = kanbancolumns[task.currCol].IsTaskHere(task);
                if (index != -1) //check if the task is in the column 
                {
                    Column sourceCol = kanbancolumns[task.currCol];
                    sourceCol.RemoveTask(task); //remove task from source column
                    kanbancolumns.Remove(task.currCol); //remove previous column
                    sourceCol.SortByCreationTime();
                    kanbancolumns.Add(task.currCol, sourceCol); //add new column 
                    FileLogger.write(Authantication.userRegisterd); //write to file
                    return true;
                }
                else
                {
                    FileLogger.WriteNullObjectExceptionToLogger<Task>("DeleteTask function");
                    return false;
                }

            }
            else
            {
                FileLogger.WriteNullObjectExceptionToLogger<Task>("DeleteTask function");
                return false;
            }
        }
        public bool DeleteTask(object task)
        {
            if (task is Task)
            {
                return DeleteTask((Task)task);
            }
            else
            {
                FileLogger.WriteNullObjectExceptionToLogger<Task>("DeleteTask function");
                return false;
            }
        }

        public string getNextColumn(string currCol)
        {
            return this.KanBanBoard.columnsOrder.Find(currCol).Next.Value;
        }

        public bool EditTask(Task oldTask, Task newTask, string currCol)
        {
            if (this.KanBanBoard.boardColumns[currCol].RemoveTask(oldTask) &&
                                this.KanBanBoard.boardColumns[currCol].AddTask(newTask))
            {
                FileLogger.write(Authantication.userRegisterd); //write to file
                return true;
            }
            else
            {
                FileLogger.WriteErrorToLog("couldn't edit the task - " + newTask.GetTitle() + " in column - " + currCol);
            }
            return false;

        }
    }
}
