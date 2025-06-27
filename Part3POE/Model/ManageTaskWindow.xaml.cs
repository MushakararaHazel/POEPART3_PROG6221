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

namespace Part3POE.Model
{
    /// <summary>
    /// Interaction logic for ManageTaskWindow.xaml
    /// </summary>
    public partial class ManageTaskWindow : Window
    {
        private TaskItem task;
        public ManageTaskWindow()
        {
            InitializeComponent();
          

            TitleBox.Text = task.Title;
            DescriptionBox.Text = task.Description;
            ReminderDatePicker.SelectedDate = task.ReminderDate;
            CompleteCheckBox.IsChecked = task.IsCompleted;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            task.Title = TitleBox.Text.Trim();
            task.Description = DescriptionBox.Text.Trim();
            task.ReminderDate = ReminderDatePicker.SelectedDate;
            task.IsCompleted = CompleteCheckBox.IsChecked == true;

            DialogResult = true; // Close the window and return
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
    }

