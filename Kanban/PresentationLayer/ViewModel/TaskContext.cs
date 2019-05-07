using System;
using Kanban.BL;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Kanban.InterfaceLayer;

namespace Kanban.PresentationLayer.ViewModel
{
    public class TaskContext : INotifyPropertyChanged
    {
        User user;
        string username;

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
        DateTime dueDate;
        public DateTime DueDate
        {
            get
            {
                return dueDate.Date;
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

        public TaskContext(Task task, string user)
        {
            this.Title = task.title;
            this.Column = task.currCol;
            this.DueDate = task.dueDate;
            this.CreationTime = task.creationTime;
            this.Description = task.description;
            UserService service = new UserService();
            this.user = service.GetUser(user);
            this.username = user;
        }

        public TaskContext(string user)
        {
            UserService service = new UserService();
            this.user = service.GetUser(user);
            this.username = user;
        }

        public bool EditTask(Task task)
        {
            Validation val = new Validation();
            UserService service = new UserService();
            if (val.validateTaskInfo(Title, Description, DueDate))
            { //update the task
                Task newTask = new Task(Title, description, DueDate, Column, task.creationTime);
                Column currentColumn = user.KanBanBoard.boardColumns[newTask.currCol];
                if (service.EditTask(task, newTask,username,task.currCol))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public bool CreateTask()
        {
            Validation val = new Validation();
            if (val.validateTaskInfo(Title, Description, DueDate))
            {
                UserService service = new UserService();
                if(service.CreateTask(username, Title, Description, DueDate))
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        public bool PromoteTask()
        {
            UserService service = new UserService();
            if(service.PromoteTaskToNextPhase(username,this))
                return true;
            else
                return false;

        }
    }
}
