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
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = emal.Text;
            string password = pswd.Password;
            User user = Authantication.login(email, password);
            if (user != null)
            {
                KanbanWindow kanban = new KanbanWindow();
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
