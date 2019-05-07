using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Kanban.BL;
using Caliburn.Micro;
using Kanban.InterfaceLayer;
using System.Windows;

namespace Kanban.PresentationLayer.ViewModel
{
    public class BoardWindowDataContext : INotifyPropertyChanged
    {
        User user;
        string username;
        public BoardWindowDataContext(string user)
        {
            UserService service = new UserService();
            this.user = service.GetUser(user);
            this.username = user;
            ShowTheard(this.user);
        }

        string searchTerm = "";
        public string SearchTerm
        {
            get
            {
                return searchTerm;
            }
            set
            {
                searchTerm = value;
                UpdateFilter();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SearchTerm"));
            }
        }

        private TaskContext selected;
        public TaskContext Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
            }
        }

        private ICollectionView gridView;
        public ICollectionView GridView
        {
            get
            {
                return gridView;
            }
            set
            {
                gridView = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GridView"));
            }
        }

        private BindableCollection<BoardWindowTask> tasks;

        public event PropertyChangedEventHandler PropertyChanged;

        public BindableCollection<BoardWindowTask> Tasks
        {
            get
            {
                return tasks;
            }
            set
            {
                tasks = value;
                UpdateFilter();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Columns"));
            }
        }
        public void ShowTheard(User user)
        {
            BindableCollection<BoardWindowTask> tasks = new BindableCollection<BoardWindowTask>();
            foreach (Column col in user.KanBanBoard.boardColumns.Values)
            {
                foreach (Task t in col.getTasks())
                {
                    if (t != null)
                        tasks.Add(new BoardWindowTask(t));
                }

            }
            Tasks = tasks;
        }

        public void UpdateFilter()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = tasks };
            ICollectionView cv = cvs.View;
            cv.Filter = o =>
            {
                BoardWindowTask p = o as BoardWindowTask;
                return (p.Title.ToUpper().Contains(SearchTerm.ToUpper()) & p.Description.ToUpper().Contains(SearchTerm.ToUpper()));
            };
            GridView = cv;
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

        public bool CreateColumn() {
            Validation val = new Validation();
            UserService service = new UserService();
            if (val.validateColumnInfo(Column,user.KanBanBoard.boardColumns))
            {
                service.CreateColumn(username, PrevColumn,Column);
                return true;
            }
            else
                return false;
        }

        string prevcolumn = "";
        public string PrevColumn
        {
            get
            {
                return prevcolumn;
            }
            set
            {
                prevcolumn = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PrevColumn"));
            }
        }

        public bool ReplaceColumns()
        {
            if (user.swapColumnsPosition(Column, PrevColumn)){ 
                return true;
            }
            else
                return false;
        }
    }
}
