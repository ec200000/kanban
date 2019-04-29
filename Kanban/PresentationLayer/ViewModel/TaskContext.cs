using System;
using Kanban.BL;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Kanban.PresentationLayer.ViewModel
{
    public class TaskContext : INotifyPropertyChanged
    {
        User user;

        string title = "";
        public string Title {
            get
            {
                return title;
            }
            set
            {
                title = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
            }
        }
        string description = "";
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }
        string dueDate = "";
        public string DueDate
        {
            get
            {
                return dueDate;
            }
            set
            {
                dueDate = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DueDate"));
            }
        }
        string column = "";
        public string Column
        {
            get
            {
                return column;
            }
            set
            {
                column = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Column"));
            }
        }
        string creationTime = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public string CreationTime
        {
            get
            {
                return creationTime;
            }
            set
            {
                creationTime = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CreationTime"));
            }
        }

        public TaskContext(Task task, User user)
        {
            this.Title = task.title;
            this.Column = task.currCol;
            this.DueDate = task.dueDate;
            this.CreationTime = task.creationTime;
            this.Description = task.description;
            this.user = user;
        }

        public TaskContext(User user)
        {
            this.user = user;
        }

        public bool EditTask(Task task)
        {
            Validation val = new Validation();
            if (val.validateTaskInfo(title, description, dueDate))
            { //update the task
                if (task.GetTitle() != title)
                    task.SetTitle(title);
                if (task.GetDescription() != description)
                    task.SetDescription(description);
                if (task.GetDueDate() != dueDate)
                    task.SetDueDate(dueDate);
                return true;
            }
            else
                return false;
                
        }
        public bool CreateTask()
        {
            Validation val = new Validation();
            if (val.validateTaskInfo(Title, Description, DueDate))
            {
                user.CreateTask(Title, Description, DueDate, Column);
                return true;
            }
            else
                return false;

        }
    }
}
