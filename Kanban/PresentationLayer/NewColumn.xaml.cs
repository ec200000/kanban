using Kanban.BL;
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

namespace Kanban.PresentationLayer.ViewModel
{
    /// <summary>
    /// Interaction logic for NewColumn.xaml
    /// </summary>
    public partial class NewColumn : Window
    {
        BoardWindowDataContext VM;
        User user;
        public NewColumn(User user)
        {
            InitializeComponent();

            this.user = user;
            this.VM = new BoardWindowDataContext(user); //format the VM
            this.DataContext = this.VM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool b = VM.CreateColumn();
            if (!b) MessageBox.Show("There is a problem with the things you entered");
            else {
                KanbanWindow kanban = new KanbanWindow(user); //opening the kanban window
                kanban.Show();
                Close();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            KanbanWindow kanban = new KanbanWindow(user); //opening the kanban window
            kanban.Show();
            Close();
        }
    }
}
