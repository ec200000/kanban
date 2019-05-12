using Kanban.BL;
using Kanban.PresentationLayer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kanban
{
    public partial class MainWindow : Window //the signup window
    {
        UserWindowDataContext VM;

        public MainWindow()
        {
            InitializeComponent();

            this.VM = new UserWindowDataContext(); //format the VM

            this.DataContext = this.VM;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.VM.SignUp())
            { //checking if the signup succeed
                Login login = new Login(); //opening the login window
                login.Show();
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Login login = new Login(); //opening the login window
            login.Show();
            Close();
        }
    }
}

