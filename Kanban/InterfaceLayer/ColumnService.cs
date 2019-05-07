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

        public void SetMaxNumOfTaskInColumn(int newLimit, string userName)
        {
            User user = GetUser(userName);
            foreach (string currCol in user.KanBanBoard.boardColumns.Keys)
            {
                user.KanBanBoard.boardColumns[currCol].setMaxNumOfTaskInColumn(newLimit);
            }
        }

        public bool AddTask(BL.Task task, string currCol, string userName) //add a task to the column
        {
            User user = GetUser(userName);
            return user.KanBanBoard.boardColumns[currCol].AddTask(task);

        }

        public void SortByDueDate(string userName)
        {
            User user = GetUser(userName);
            foreach (string currCol in user.KanBanBoard.boardColumns.Keys)
            {
                user.KanBanBoard.boardColumns[currCol].SortByDueDate();
            }
        }

        public bool RemoveTask(BL.Task task, string userName, string currCol)
        {
            User user = GetUser(userName);
            return user.KanBanBoard.boardColumns[currCol].RemoveTask(task);


        }
        public void SortByCreationTime(string userName)
        {
            User user = GetUser(userName);
            foreach (string currCol in user.KanBanBoard.boardColumns.Keys)
            {
                user.KanBanBoard.boardColumns[currCol].SortByCreationTime();
            }
        }

        public int IsTaskHere(BL.Task task, string currCol, string userName)//checking if a task is in this column
        {
            User user = GetUser(userName);
            return user.KanBanBoard.boardColumns[currCol].IsTaskHere(task);
        }
    }
}
