using Kanban.BL;
using Kanban.InterfaceLayer;
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

namespace Kanban.PresentationLayer
{
    /// <summary>
    /// Interaction logic for NewTask.xaml
    /// </summary>
    public partial class NewTask : Window
    {
        TaskContext VM;
        User user;
        string email;

        public NewTask(string user)
        {
            InitializeComponent();

            UserService service = new UserService();
            this.user = service.GetUser(user);
            this.VM = new TaskContext(user); //format the VM
            this.DataContext = this.VM;
            this.email = user;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool b = VM.CreateTask();
            if (!b) MessageBox.Show("There is a problem with the things you entered");
            else
            {
                KanbanWindow kanban = new KanbanWindow(email); //opening the kanban window
                kanban.Show();
                Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            KanbanWindow kanban = new KanbanWindow(email); //opening the kanban window
            kanban.Show();
            Close();
        }
    }
}
