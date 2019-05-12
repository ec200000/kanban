using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Kanban.BL;
using Kanban.InterfaceLayer;

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
        public BoardWindowTask(InterfaceLayerTask task)
        {
            this.Title = task.Title;
            this.Column = task.CurrCol;
            this.DueDate = task.DueDate;
            this.CreationTime = task.CreationTime;
            this.Description = task.Description;
        }

    }
}
