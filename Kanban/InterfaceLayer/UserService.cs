using Kanban.BL;
using Kanban.PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.InterfaceLayer
{
    public class UserService
    {
        public User GetUser(string userName)
        {
            if (userName != null)
            {
                return Authantication.userRegisterd[userName];
            }
            else
                FileLogger.WriteNullObjectExceptionToLogger<string>("GetUser[Service] function");
            return null;
        }

        public bool login(string Username, string Password)
        {
            try
            {
                if (Authantication.login(Username, Password) != null)
                {
                    return true;
                }
                else
                {
                    FileLogger.WriteErrorToLog(Username + " unable to login");
                    return false;
                }

            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex.ToString());
                return false;
            }
        }

        public bool signUp(string Username, string Password)
        {
            try
            {
                if (Authantication.signUp(Username, Password) != null)
                {
                    return true;
                }
                else
                {
                    FileLogger.WriteErrorToLog(Username + " unable to register");
                    return false;
                }

            }
            catch (Exception ex)
            {
                FileLogger.WriteErrorToLog(ex.ToString());
                return false;
            }
        }

        public bool CreateTask(string userName, string title, string description, DateTime dueDate)
        {
            User user = GetUser(userName);
            string currCol = user.KanBanBoard.columnsOrder.ElementAt(0);
            return user.CreateTask(title, description, dueDate, currCol);
        }

        //newCol,prvCol we get from the user in the create New column screen
        public bool CreateColumn(string userName, string newCol, string prvCol)
        {
            User user = GetUser(userName);
            return user.addNewColumnToBoard(prvCol, newCol);
        }

        public bool PromoteTaskToNextPhase(string userName, TaskContext currTask)
        {
            User user = GetUser(userName);
            string currCol = currTask.Column;
            string targetCol = user.KanBanBoard.columnsOrder.Find(currCol).Next.Value;
            if (targetCol == null)
            {
                FileLogger.WriteErrorToLog("the task is in the last column - can't promote the task!");
                return false;
            }
            BL.Task task = new BL.Task(currTask.Title, currTask.Description, currTask.DueDate, currCol);
            return user.PromoteTaskToNextPhase(task, currCol, targetCol);
        }

        public bool SwapColumnsPosition(string colBefore, string colToMove, string userName)
        {
            User user = GetUser(userName);
            return user.swapColumnsPosition(colBefore, colToMove);
        }

        public bool EditTask(BL.Task old, BL.Task newTask, string userName, string currCol)
        {
            User user = GetUser(userName);
            return user.EditTask(old, newTask, currCol);
        }
    }
}