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
        InterfaceLayerTask task;
        InterfaceLayerUser user;
        string email;

        public EditTask(InterfaceLayerTask task, string user)
        {
            InitializeComponent();

            this.task = task;
            UserService service = new UserService();
            this.user = service.GetUser(user);
            this.VM = new TaskContext(task,user); //format the VM
            this.email = user;
            this.DataContext = this.VM;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            if (!VM.EditTask(task)) MessageBox.Show("There is a problem with the things you entered");
            else {
                KanbanWindow kanban = new KanbanWindow(email); //opening the kanban window
                kanban.Show();
                Close();
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            KanbanWindow kanban = new KanbanWindow(email); //opening the kanban window
            kanban.Show();
            Close();
        }
        private void Button_Click_Promote(object sender, RoutedEventArgs e)
        {
            if (!VM.PromoteTask(task)) MessageBox.Show("There is a problem with the things you entered");
            else
            {
                KanbanWindow kanban = new KanbanWindow(email); //opening the kanban window
                kanban.Show();
                Close();
            }
        }
    }
}
