using System.Windows;

namespace Kanban.PresentationLayer.ViewModel
{
    /// <summary>
    /// Interaction logic for NewColumn.xaml
    /// </summary>
    public partial class NewColumn : Window
    {
        BoardWindowDataContext VM;
        string email;
        public NewColumn(string user)
        {
            InitializeComponent();
            this.VM = new BoardWindowDataContext(user); //format the VM
            this.DataContext = this.VM;
            this.email = user;
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            bool b = VM.CreateColumn();
            if (!b) MessageBox.Show("There is a problem with the things you entered");
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
    }
}
