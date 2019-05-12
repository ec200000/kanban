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
        InterfaceLayerUser user;
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
        public void ShowTheard(InterfaceLayerUser user)
        {
            BindableCollection<BoardWindowTask> tasks = new BindableCollection<BoardWindowTask>();
            InterfaceLayerBoard board = user.Board;
            foreach (InterfaceLayerColumn col in user.Board.boardColumns.Values)
            {
                foreach (InterfaceLayerTask t in col.tasks)
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
                return (p.Title.ToUpper().Contains(SearchTerm.ToUpper()) || p.Description.ToUpper().Contains(SearchTerm.ToUpper()));
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
            if (val.validateColumnInfo(Column, service.GetBoard(username)))
            {
                if (service.CreateColumn(username, Column, PrevColumn))
                {
                    this.user = service.GetUser(username);
                    ShowTheard(user);
                    return true;
                }
                    
                else
                    return false;
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
            UserService ser = new UserService();
            if (ser.replaceColumnsPosition(username, PrevColumn, Column)){
                ShowTheard(user);
                return true;
            }
            else
                return false;
        }

        public void SortByCreationTime(string userName)
        {
            UserService serv = new UserService();
            InterfaceLayerUser user = serv.GetUser(userName);
            ColumnService colserv = new ColumnService();
            colserv.SortByCreationTime(userName);
            ShowTheard(user);
        }

        public void SortByDueDate(string userName)
        {
            UserService serv = new UserService();
            InterfaceLayerUser user = serv.GetUser(userName);
            ColumnService colserv = new ColumnService();
            colserv.SortByDueDate(userName);
            ShowTheard(user);
        }

        public void RemoveColumn(string x) {
            UserService ser = new UserService();
            bool b = ser.RemoveColumn(username, x);
            if(b)
            {
                this.user = ser.GetUser(username);
                ShowTheard(user);
            }
            else
            {
                MessageBox.Show("something went wrong");
            }
        }
    }
}
