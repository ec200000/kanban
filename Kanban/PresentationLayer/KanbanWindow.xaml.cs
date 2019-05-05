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
            Service service = new Service();
            this.user = service.GetUser(user);
            this.email = user;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            user.DeleteTask(Tasks.SelectedItem);
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
    }
}
