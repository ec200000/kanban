using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.BL
{
    public class Column
    {
        //private const int MAX_TASKS_IN_COLUMN = 5;
        public int maxNumOfTaskInColumn;
        public List<Task> tasks;

        public Column(List<Task> tasks)
        {
            this.maxNumOfTaskInColumn = int.MaxValue;//the default is infinity
            this.tasks = tasks;
        }

        public Column()
        {
            this.maxNumOfTaskInColumn = int.MaxValue;
            this.tasks = new List<Task>();
        }

        public bool setMaxNumOfTaskInColumn(int newLimit)
        {
            if (newLimit < tasks.Count)
                return false;
            this.maxNumOfTaskInColumn = newLimit;
            FileLogger.write(Authantication.userRegisterd);
            return true;
        }

        public List<Task> getTasks()
        {
            return tasks;
        }

        public bool AddTask(Task task) //add a task to the column
        {
            int empty = GetNumOfTasks();
            if (empty == maxNumOfTaskInColumn)//reached the limit
            {
                FileLogger.WriteErrorToLog("no more space to add the task - " + task.GetTitle() + "in the " + task.GetCurrentColumn() + " column");
                return false;
            }
            else
            {
                this.tasks.Add(task);
                return true;
            }
        }
        public int GetNumOfTasks()
        {
            return this.tasks.Count;
        }

        public void SortByDueDate()
        {
            this.tasks.Sort((a, b) => DateTime.Compare(a.GetDueDate().Date, b.GetDueDate().Date));
        }

        public bool RemoveTask(Task task)
        {
            if (!this.tasks.Remove(this.tasks.Find(t => t.Equals(task))))//if task doesn't exist
            {
                FileLogger.WriteErrorToLog("Can't earse the task because it does not exist!");
                return false;
            }
            return true;
        }
        public void SortByCreationTime()
        {
            this.tasks.Sort((a, b) => TimeSpan.Compare(DateTime.Parse(a.GetCreationTime()).TimeOfDay, DateTime.Parse(b.GetCreationTime()).TimeOfDay));
        }
        public int IsTaskHere(Task task)//checking if a task is in this column
        {
            return this.tasks.FindIndex(a => a.Equals(task));
        }
        public void Print()
        {
            foreach (Task t in tasks)
            {
                t.Print();
            }
        }
        /*static void Main(string[] args)
        {
            Task t1 = new Task("title", "blalala", "11/3/2020", "27/3/2019");
            Task t2 = new Task("title2", "blalala2", "11/3/2018", "20/3/2019");
            Task t3 = new Task("title3", "blalala3", "11/3/2019", "10/3/2019");
            Task t4 = new Task("title3", "blalala3", "11/3/2019", "10/3/2019");
            Task[] tasks = new Task[5];
            tasks[0] = t1;
            tasks[1] = t2;
            tasks[2] = t3;
            Column column = new Column(tasks);
            Console.WriteLine(column.GetNumOfTasks());
            Console.WriteLine(column.RemoveTask(t3));
            Console.WriteLine(column.RemoveTask(t4));
            Console.WriteLine(column.AddTask(t4));
            int n = column.GetNumOfTasks();
            column.SortByDueDate();
            Console.WriteLine(n);
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(column.getTasks()[i].GetDueDate());
            }
            column.SortByCreationTime();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(column.getTasks()[i].GetCreationTime());
            }
            Console.ReadKey();
        }*/
    }

}