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

namespace Kanban.PresentationLayer
{
    public partial class Login : Window
    {
        UserWindowDataContext VM;

        public Login()
        {
            InitializeComponent();

            this.VM = new UserWindowDataContext(); //format the VM

            this.DataContext = this.VM;

        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            InterfaceLayer.InterfaceLayerUser user = VM.Login();
            string email = VM.Email;
            if (user!=null)//checking if the login succeed
            {
                KanbanWindow kanban = new KanbanWindow(email); //opening the kanban window
                kanban.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Wrong Email and/or Password");
            }
        }
    }
}
