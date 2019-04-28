using Kanban.BL;
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
    /// Interaction logic for KanbanWindow.xaml
    /// </summary>
    public partial class KanbanWindow : Window
    {
        BoardWindowDataContext VM;
        public KanbanWindow(User user)
        {
            InitializeComponent();

            VM = new BoardWindowDataContext(user);
            this.DataContext = VM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
