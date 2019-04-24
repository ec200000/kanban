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

        public EditTask(BL.Task task)
        {
            InitializeComponent();

            this.task = task;
            this.VM = new TaskContext(task); //format the VM

            this.DataContext = this.VM;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(!VM.EditTask(task)) MessageBox.Show("There is a problem with the things you entered");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            KanbanWindow kanban = new KanbanWindow(); //opening the kanban window
            kanban.Show();
            Close();
        }
    }
}
