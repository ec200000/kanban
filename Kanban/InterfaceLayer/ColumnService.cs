using Kanban.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.InterfaceLayer
{
    class ColumnService
    {
        public InterfaceLayerUser GetUser(string userName)
        {
            if (userName != null)
            {
                User user = Authantication.userRegisterd[userName];
                Dictionary<string, InterfaceLayerColumn> boardColumns = new Dictionary<string, InterfaceLayerColumn>();
                foreach (KeyValuePair<string, Column> col in user.KanBanBoard.boardColumns)
                {
                    List<InterfaceLayerTask> tasks = new List<InterfaceLayerTask>();
                    Column c = col.Value;
                    foreach (BL.Task t in c.getTasks())
                    {
                        InterfaceLayerTask taskToAdd = new InterfaceLayerTask(t.title, t.description, t.dueDate, t.creationTime, t.currCol);
                        tasks.Add(taskToAdd);
                    }
                    string colName = col.Key;
                    InterfaceLayerColumn tempCol = new InterfaceLayerColumn(colName, tasks, c.maxNumOfTaskInColumn);
                    boardColumns.Add(colName, tempCol);
                }
                InterfaceLayerBoard board = new InterfaceLayerBoard(boardColumns);
                return new InterfaceLayerUser(user.GetEmail(), board);
            }
            else
                FileLogger.WriteNullObjectExceptionToLogger<string>("GetUser[Service] function");
            return null;
        }

        public bool SetMaxNumOfTaskInColumn(int newLimit, string userName)
        {
            User user = Authantication.userRegisterd[userName];
            foreach (string currCol in user.KanBanBoard.boardColumns.Keys)
            {
                bool b = user.KanBanBoard.boardColumns[currCol].setMaxNumOfTaskInColumn(newLimit);
                if (!b) return false;
            }
            return true;
        }

        public bool AddTask(InterfaceLayerTask t, string currCol, string userName) //add a task to the column
        {
            User user = Authantication.userRegisterd[userName];
            BL.Task task = new BL.Task(t.Title, t.Description, t.DueDate, t.CurrCol);
            return user.KanBanBoard.boardColumns[currCol].AddTask(task);

        }

        public void SortByDueDate(string userName)
        {
            User user = Authantication.userRegisterd[userName];
            foreach (string currCol in user.KanBanBoard.boardColumns.Keys)
            {
                user.KanBanBoard.boardColumns[currCol].SortByDueDate();
            }
        }

        public bool RemoveTask(InterfaceLayerTask t, string userName, string currCol)
        {
            User user = Authantication.userRegisterd[userName];
            BL.Task task = new BL.Task(t.Title, t.Description, t.DueDate, t.CurrCol);
            return user.KanBanBoard.boardColumns[currCol].RemoveTask(task);


        }
        public void SortByCreationTime(string userName)
        {
            User user = Authantication.userRegisterd[userName];
            foreach (string currCol in user.KanBanBoard.boardColumns.Keys)
            {
                user.KanBanBoard.boardColumns[currCol].SortByCreationTime();
            }
        }

        public int IsTaskHere(InterfaceLayerTask t, string currCol, string userName)//checking if a task is in this column
        {
            User user = Authantication.userRegisterd[userName];
            BL.Task task = new BL.Task(t.Title, t.Description, t.DueDate, t.CurrCol);
            return user.KanBanBoard.boardColumns[currCol].IsTaskHere(task);
        }
    }
}
