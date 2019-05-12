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
    /// Interaction logic for ReplaceColumns.xaml
    /// </summary>
    public partial class ReplaceColumns : Window
    {
        BoardWindowDataContext VM;
        string email;
        public ReplaceColumns(string user)
        {
            InitializeComponent();
            this.VM = new BoardWindowDataContext(user); //format the VM
            this.DataContext = this.VM;
            this.email = user;
        }

        private void Button_Click_Replace(object sender, RoutedEventArgs e)
        {
            bool b = VM.ReplaceColumns();
            if (!b) MessageBox.Show("There is a problem with the things you entered");
            else
            {
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
    }
}
