using Kanban.BL;
using Kanban.PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task = Kanban.BL.Task;

namespace Kanban.InterfaceLayer
{
    public class UserService
    {
        public bool RemoveColumn(string username, string colToRemove)
        {
            User user = Authantication.userRegisterd[username];
            return user.RemoveColumn(colToRemove);
        }

        public Board GetBoard(string userName)
        {
            if (userName!=null)
            {
                User user = Authantication.userRegisterd[userName];
                return user.KanBanBoard;
            }
            else FileLogger.WriteNullObjectExceptionToLogger<string>("GetBoard[Service] function");
            return null;
        }
        public InterfaceLayerUser GetUser(string userName)
        {
            if (userName != null)
            {
                User user = Authantication.userRegisterd[userName];
                Dictionary<string, InterfaceLayerColumn> boardColumns = new Dictionary<string, InterfaceLayerColumn>();
                foreach (KeyValuePair<string,Column> col in user.KanBanBoard.boardColumns)
                {
                    List<InterfaceLayerTask> tasks = new List<InterfaceLayerTask>();
                    Column c = col.Value;
                    foreach(Task t in c.getTasks())
                    {
                        InterfaceLayerTask taskToAdd = new InterfaceLayerTask(t.title, t.description, t.dueDate, t.creationTime, t.currCol);
                        tasks.Add(taskToAdd);
                    }
                    string colName = col.Key;
                    InterfaceLayerColumn tempCol = new InterfaceLayerColumn(colName, tasks, c.maxNumOfTaskInColumn);
                    boardColumns.Add(colName,tempCol);
                }
                InterfaceLayerBoard board = new InterfaceLayerBoard(boardColumns);
                InterfaceLayerUser user1 = new InterfaceLayerUser(user.GetEmail(), board);
                return user1;
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
            User user = Authantication.userRegisterd[userName];
            string currCol = user.KanBanBoard.columnsOrder.ElementAt(0);
            return user.CreateTask(title, description, dueDate, currCol);
        }

        //newCol,prvCol we get from the user in the create New column screen
        public bool CreateColumn(string userName, string newCol, string prvCol)
        {
            User user = Authantication.userRegisterd[userName];
            return user.addNewColumnToBoard(prvCol, newCol);
        }

        public bool PromoteTaskToNextPhase(string userName,InterfaceLayerTask task)
        {
            User user = Authantication.userRegisterd[userName];
            string currCol = task.CurrCol;
            string targetCol = user.KanBanBoard.columnsOrder.Find(currCol).Next.Value;
            if (targetCol == null)
            {
                FileLogger.WriteErrorToLog("the task is in the last column - can't promote the task!");
                return false;
            }
            BL.Task t = new BL.Task(task.Title, task.Description, task.DueDate, task.CurrCol, task.CreationTime);
            return user.PromoteTaskToNextPhase(t, currCol, targetCol);
        }

   

        public bool EditTask(InterfaceLayerTask old, InterfaceLayerTask newTask, string userName, string currCol)
        {
            User user = Authantication.userRegisterd[userName];
            BL.Task old1 = new BL.Task(old.Title, old.Description, old.DueDate, old.CurrCol, old.CreationTime);
            BL.Task newTask1 = new BL.Task(newTask.Title, newTask.Description, newTask.DueDate, newTask.CurrCol, old.CreationTime);
            return user.EditTask(old1, newTask1, currCol);
        }

        public bool replaceColumnsPosition(string userName , string colBefore, string colToMove)
        {
            User user = Authantication.userRegisterd[userName];
            return user.replaceColumnsPosition(colBefore, colToMove);
        }
    }
}