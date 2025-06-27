using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

    public partial class TaskManagerWindow : Window
    {
        private List<TaskItem> _tasks;

        public TaskManagerWindow(List<TaskItem> tasks)
        {
            InitializeComponent();
            _tasks = tasks;
            RefreshList();
        }

        private void RefreshList()
        {
            lstTasks.ItemsSource = null;
            lstTasks.ItemsSource = _tasks; // List<TaskItem>
        }


        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            DateTime? reminder = dateReminder.SelectedDate;

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            TaskItem newTask = new TaskItem
            {
                Title = title,
                Description = description,
                ReminderDate = reminder,
                IsCompleted = false
            };

            _tasks.Add(newTask);
            ActivityLogManager.AddLog($"Task added: '{title}'");
            RefreshList();

            txtTitle.Clear();
            txtDescription.Clear();
            dateReminder.SelectedDate = null;
        }
        private void EditTask_Click(object sender, RoutedEventArgs e)
{
    if (lstTasks.SelectedItem is TaskItem selectedTask)
    {
        string newTaskName = Microsoft.VisualBasic.Interaction.InputBox("Edit task:", "Edit Task", selectedTask.Name);
        if (!string.IsNullOrWhiteSpace(newTaskName))
        {
            selectedTask.Name = newTaskName;
            RefreshList();
        }
    }
}

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedItem is TaskItem selectedTask)
            {
                selectedTask.IsCompleted = true;
                ActivityLogManager.AddLog($"Marked task '{selectedTask.Title}' as completed.");
                RefreshList();
            }
            else
            {
                MessageBox.Show("Please select a task to mark as complete.");
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedItem is TaskItem selectedTask)
            {
                _tasks.Remove(selectedTask);
                ActivityLogManager.AddLog($"Deleted task '{selectedTask.Title}'.");
                RefreshList();
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Return to main page

        }
    }
    }
