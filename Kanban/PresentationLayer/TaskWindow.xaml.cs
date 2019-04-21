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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        BL.Task task;
        public TaskWindow(BL.Task task)
        {
            this.task = task;
            InitializeComponent();
            title.Text = task.GetTitle();
            description.Text = task.GetDescription();
            duedate.Text = task.GetDueDate();
            creationtime.Text = task.GetCreationTime();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EditTask edit = new EditTask(task);
            edit.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            KanbanWindow kanban = new KanbanWindow();
            kanban.Show();
            Close();
        }
    }
}
