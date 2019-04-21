using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.BL
{
    public class Column
    {
        private const int MAX_TASKS_IN_COLUMN = 5;

        public Task[] tasks;

        public Column(Task[] tasks)
        {
            this.tasks = tasks;
        }

        public Column()
        {
            this.tasks = new Task[MAX_TASKS_IN_COLUMN];
        }

        public Task[] getTasks()
        {
            return tasks;
        }

        public bool AddTask(Task task) //add a task to the column
        {
            int empty = GetNumOfTasks();
            int index = -1;
            if (empty == MAX_TASKS_IN_COLUMN) return false; //if the number of tasks is 5
            else
            {
                for (int i = 0; i < MAX_TASKS_IN_COLUMN & index < 0; i++)
                {
                    if (tasks[i] == null)
                    {
                        index = i;
                    }
                }
                tasks[index] = task; //entering the task in the index index
                return true;
            }
        }
        public int GetNumOfTasks()
        {
            int counter = 0;
            for (int i = 0; i < MAX_TASKS_IN_COLUMN; i++)
            {
                if (tasks[i] != null)
                {
                    counter++; //counting number of tasks
                }
            }
            return counter;
        }

        public void SortByDueDate()
        {
            string[] arr = new string[GetNumOfTasks()];
            int[] indexes = new int[GetNumOfTasks()];
            Task[] temp = new Task[MAX_TASKS_IN_COLUMN];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = tasks[i].GetDueDate();
            }
            Array.Sort(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[i].Equals(tasks[j].GetDueDate())) indexes[i] = j;
                }
            }
            for (int j = 0; j < arr.Length; j++)
            {
                temp[j] = tasks[indexes[j]];
            }
            tasks = temp;
        }

        public bool RemoveTask(Task task)
        {
            bool exist = false;
            for (int i = 0; i < MAX_TASKS_IN_COLUMN & !exist; i++)
            {
                if (tasks[i] != null && tasks[i].Equals(task)) //if the task exists
                {
                    Task[] copy = new Task[MAX_TASKS_IN_COLUMN];
                    for (int j = 0; j < i; j++)
                    {
                        copy[j] = tasks[j];
                    }

                    for (int j = i; j < MAX_TASKS_IN_COLUMN - 1; j++)
                    {
                        copy[j] = tasks[j + 1];
                    }
                    tasks = copy; //removing the task and creating an array without it
                    return true;
                }
            }
            return false; //if task doesn't exist
        }
        public void SortByCreationTime()
        {
            string[] arr = new string[GetNumOfTasks()];
            int[] indexes = new int[GetNumOfTasks()];
            Task[] temp = new Task[MAX_TASKS_IN_COLUMN];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = tasks[i].GetCreationTime();
            }
            Array.Sort(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[i].Equals(tasks[j].GetCreationTime())) indexes[i] = j;
                }
            }
            for (int j = 0; j < arr.Length; j++)
            {
                temp[j] = tasks[indexes[j]];
            }
            tasks = temp;
        }
        public int IsTaskHere(Task task)
        { //checking if a task is in this column
            for (int i = 0; i < MAX_TASKS_IN_COLUMN; i++)
            {
                if (tasks[i].Equals(task)) //if the task is here
                {
                    return i;
                }
            }
            return -1;
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
