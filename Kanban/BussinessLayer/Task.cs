using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.BL
{
    class Task
    {
        public string title;
        public string description;
        public string dueDate;
        public string creationTime;

        public Task(string title, string description, string dueDate)
        {
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
            this.creationTime = DateTime.Now.ToString("HH:mm:ss");
        }

        public string GetTitle()
        {
            return title;
        }

        public void SetTitle(String title)
        {
            this.title = title;
        }

        public string GetDescription()
        {
            return description;
        }

        public void SetDescription(String description)
        {
            this.description = description;
        }

        public string GetDueDate()
        {
            return dueDate;
        }

        public void SetDueDate(String dueDate)
        {
            this.dueDate = dueDate;
        }

        public string GetCreationTime()
        {
            return creationTime;
        }

        public bool Equals(Task other) //checking if 2 tasks are the same
        {
            Validation val = new Validation();
            if (other != null && val.validateTaskInfoIsNotNull(other) && val.validateTaskInfoIsNotNull(this))
            {
                if (other.GetCreationTime().Equals(this.creationTime) & other.GetTitle().Equals(this.title) & other.GetDescription().Equals(this.description) & other.GetDueDate().Equals(this.dueDate))
                    return true;
            }
            else
            {
                FileLogger.WriteNullObjectExceptionToLogger<Task>("in function Equals");
            }
            return false;
        }
        public void Print()
        {
            Console.WriteLine(this.title + this.description + this.creationTime + this.dueDate);
        }
        /*static void Main(string[] args)
        {
            Task t1 = new Task("title", "blalala", "11/3/2020", "27/3/2019");
            Task t2 = new Task("title2", "blalala2", "11/3/2018", "20/3/2019");
            Task t3 = new Task("title3", "blalala3", "11/3/2019", "10/3/2019");
            Task t4 = new Task("title3", "blalala3", "11/3/2019", "10/3/2019");
            Console.WriteLine(t1.GetTitle());
            Console.WriteLine(t2.GetDueDate());
            Console.WriteLine(t3.GetDescription());
            Console.WriteLine(t1.GetCreationTime());
            Console.WriteLine(t3.Equals(t4));
            t2.SetDescription("new");
            t1.SetTitle("check");
            Console.WriteLine(t2.GetDescription());
            Console.WriteLine(t1.GetTitle());
            Console.ReadKey();
        }*/

    }

}