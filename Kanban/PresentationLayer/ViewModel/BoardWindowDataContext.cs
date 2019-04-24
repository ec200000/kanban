using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Kanban.PresentationLayer.ViewModel
{
    public class BoardWindowDataContext : INotifyPropertyChanged
    {
        public BoardWindowDataContext()
        {
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
              //  UpdateFilter();
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

        private ObservableCollection<BoardWindowColumn> columns;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<BoardWindowColumn> Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
               // UpdateFilter();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Columns"));
            }
        }

       /* private void UpdateFilter()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = columns };
            ICollectionView cv = cvs.View;
            cv.Filter = o =>
            {
                BoardWindowColumn p = o as BoardWindowColumn;
                return (p.Content.ToUpper().Contains(SearchTerm.ToUpper()));
            };
            GridView = cv;
        }*/
    }
}
