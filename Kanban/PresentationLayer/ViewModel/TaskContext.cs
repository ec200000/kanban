using System;
using Kanban.BL;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Kanban.InterfaceLayer;
using System.Windows;

namespace Kanban.PresentationLayer.ViewModel
{
    public class TaskContext : INotifyPropertyChanged
    {
        InterfaceLayerUser user;
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

        public TaskContext(InterfaceLayerTask task, string user)
        {
            this.Title = task.Title;
            this.Column = task.CurrCol;
            this.DueDate = task.DueDate;
            this.CreationTime = task.CreationTime;
            this.Description = task.Description;
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

        public bool EditTask(InterfaceLayerTask task)
        {
            Validation val = new Validation();
            UserService service = new UserService();
            if (val.validateTaskInfo(Title, Description, DueDate))
            { //update the task
                InterfaceLayerTask newTask = new InterfaceLayerTask(Title, description, DueDate, task.CreationTime, Column);
                InterfaceLayerColumn currentColumn = user.Board.boardColumns[newTask.CurrCol];
                if (service.EditTask(task, newTask,username,task.CurrCol))
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
        public bool PromoteTask(InterfaceLayerTask task)
        {
            UserService service = new UserService();
            if(service.PromoteTaskToNextPhase(username,task))
                return true;
            else
                return false;

        }
    }
}
