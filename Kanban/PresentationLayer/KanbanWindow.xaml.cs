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
        User user;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TaskContext t = new TaskContext(email);
            Tasks.Items.RemoveAt(Tasks.SelectedIndex);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NewColumn newColumn = new NewColumn(email);
            newColumn.Show();
            Close();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NewTask newTask = new NewTask(email);
            newTask.Show();
            Close();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string x = Interaction.InputBox("Limit tasks number","Add number","THANK YOU",-1,-1);
            char[] c = x.ToCharArray();
            int n = c[0];
            ColumnService service = new ColumnService();
            service.SetMaxNumOfTaskInColumn(n, email);
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string x = Interaction.InputBox("Remove column", "Enter column's name", "THANK YOU", -1, -1);
            bool b = user.RemoveColumn(x);
            if(b)
                VM.ShowTheard(user);
            else
            {
                MessageBox.Show("something went wrong");
            }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            VM.UpdateFilter();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ColumnService service = new ColumnService();
            service.SortByDueDate(email);
            VM.ShowTheard(user);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ColumnService service = new ColumnService();
            service.SortByCreationTime(email);
            VM.ShowTheard(user);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
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
                BL.Task task = new BL.Task(bwk.Title, bwk.Description, bwk.DueDate, bwk.Column, bwk.CreationTime);
                EditTask edit = new EditTask(task,email);
                edit.Show();
                Close();
            }
        }
    }
}
