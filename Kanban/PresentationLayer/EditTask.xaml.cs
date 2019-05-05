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
    public partial class EditTask : Window
    {
        TaskContext VM;
        BL.Task task;
        User user;
        string email;

        public EditTask(BL.Task task, string user)
        {
            InitializeComponent();

            this.task = task;
            Service service = new Service();
            this.user = service.GetUser(user);
            this.VM = new TaskContext(task,user); //format the VM
            this.email = user;
            this.DataContext = this.VM;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(!VM.EditTask(task)) MessageBox.Show("There is a problem with the things you entered");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            KanbanWindow kanban = new KanbanWindow(email); //opening the kanban window
            kanban.Show();
            Close();
        }
    }
}
