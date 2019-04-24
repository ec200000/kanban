﻿using Kanban.PresentationLayer.ViewModel;
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
    public partial class TaskWindow : Window
    {
        TaskContext VM;
        BL.Task task;

        public TaskWindow(BL.Task task)
        {
            InitializeComponent();

            this.task = task;
            this.VM = new TaskContext(task); //format the VM

            this.DataContext = this.VM;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EditTask edit = new EditTask(task); //open the task edit window
            edit.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            KanbanWindow kanban = new KanbanWindow(); //return to the kanban window
            kanban.Show();
            Close();
        }
    }
}
