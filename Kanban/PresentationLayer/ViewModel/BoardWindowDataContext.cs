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
        public BoardWindowDataContext(string user)
        {
            Service service = new Service();
            this.user = service.GetUser(user);
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
        void ShowTheard(User user)
        {
            BindableCollection<BoardWindowTask> tasks = new BindableCollection<BoardWindowTask>();
            foreach (string col in user.KanBanBoard.boardColumns)
            {
                Column col = user.KanBanBoard.boardColumns[colName];
                foreach (Task t in col.getTasks())
                {
                    if (t != null)
                        tasks.Add(new BoardWindowTask(t));
                }

            }
            Tasks = tasks;
        }

        private void UpdateFilter()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = tasks };
            ICollectionView cv = cvs.View;
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
            if (val.validateColumnInfo(Column,user.KanBanBoard.boardColumns))
            {
                user.CreateColumn(Column);
                return true;
            }
            else
                return false;
        }
    }
}
