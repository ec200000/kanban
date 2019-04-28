using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Kanban.BL;
using Caliburn.Micro;

namespace Kanban.PresentationLayer.ViewModel
{
    public class BoardWindowDataContext : INotifyPropertyChanged
    {
        public BoardWindowDataContext(User user)
        {
            ShowTheard(user);
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
            foreach (Column c in user.KanBanBoard.boardColumns.Values)
            {
                foreach (Task t in c.getTasks())
                {
                    if(t!=null) tasks.Add(new BoardWindowTask(t));
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
    }
}
