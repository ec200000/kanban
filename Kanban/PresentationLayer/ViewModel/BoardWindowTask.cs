using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Kanban.BL;

namespace Kanban.PresentationLayer.ViewModel
{
    public class BoardWindowTask
    {
        string title = "";
        public string Title
        {
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
        public BoardWindowTask(Task task)
        {
            this.Title = task.title;
            this.Column = task.currCol;
            this.DueDate = task.dueDate;
            this.CreationTime = task.creationTime;
            this.Description = task.description;
        }
    }
}
