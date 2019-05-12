using Kanban.BL;
using Kanban.PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using Kanban.InterfaceLayer;

namespace Kanban.PresentationLayer
{
    /// <summary>
    /// Interaction logic for KanbanWindow.xaml
    /// </summary>
    public partial class KanbanWindow : Window
    {
        BoardWindowDataContext VM;
        InterfaceLayerUser user;
        string email;
        public KanbanWindow(string user)
        {
            InitializeComponent();

            VM = new BoardWindowDataContext(user);
            this.DataContext = VM;
            UserService service = new UserService();
            this.user = service.GetUser(user);
            this.email = user;
        }
        private void Button_Click_NewCol(object sender, RoutedEventArgs e)
        {
            NewColumn newColumn = new NewColumn(email);
            newColumn.Show();
            Close();
        }
        private void Button_Click_NewTask(object sender, RoutedEventArgs e)
        {
            NewTask newTask = new NewTask(email);
            newTask.Show();
            Close();
        }
        private void Button_Click_Limit(object sender, RoutedEventArgs e)
        {
            string x = Interaction.InputBox("Limit tasks number","Add number","THANK YOU",-1,-1);
            int n = Int32.Parse(x);
            ColumnService service = new ColumnService();
            bool b = service.SetMaxNumOfTaskInColumn(n, email);
            if(!b)
            {
                MessageBox.Show("something went wrong");
            }
        }
        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            string x = Interaction.InputBox("Remove column", "Enter column's name", "THANK YOU", -1, -1);
            VM.RemoveColumn(x);
            Tasks.Items.Refresh();
        }
        private void Button_Click_Filter(object sender, RoutedEventArgs e)
        {
            VM.UpdateFilter();
        }

        private void Button_Click_DueDate(object sender, RoutedEventArgs e)
        {
            VM.SortByDueDate(email);
        }

        private void Button_Click_Creation(object sender, RoutedEventArgs e)
        {
            VM.SortByCreationTime(email);
        }

        private void Button_Click_Replace(object sender, RoutedEventArgs e)
        {
            ReplaceColumns replace = new ReplaceColumns(email);
            replace.Show();
            Close();
        }

        private void Data_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            BoardWindowTask bwk = Tasks.SelectedItem as BoardWindowTask;
            if (bwk != null)
            {
                InterfaceLayerTask task = new InterfaceLayerTask(bwk.Title, bwk.Description, bwk.DueDate, bwk.CreationTime, bwk.Column);
                TaskWindow edit = new TaskWindow(task,email);
                edit.Show();
                Close();
            }
        }
    }
}
