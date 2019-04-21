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
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        BL.Task task;
        public EditTask(BL.Task task)
        {
            InitializeComponent();
            this.task = task;
            title.Text = task.title;
            description.Text = task.description;
            duedate.Text = task.dueDate;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string tite = title.Text;
            string disc = description.Text;
            string date = duedate.Text;
            BL.Validation val = new BL.Validation();
            if (val.validateTaskInfo(tite, disc, date)) {
                task.SetDescription(disc);
                task.SetDueDate(date);
                task.SetTitle(tite);
            }
            else
                MessageBox.Show("There is a problem with the thing you entered");
        }
    }
}
