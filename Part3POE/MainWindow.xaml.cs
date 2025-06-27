using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Part3POE.Model;
using System.Text.RegularExpressions;

using Part3POE.PART2;


namespace Part3POE
{
        public partial class MainWindow : Window
        {
            private List<string> activityLog = new List<string>();

            public MainWindow()
            {
                InitializeComponent();
            }

            private void Send_Click(object sender, RoutedEventArgs e)
            {
                string input = txtUserInput.Text.Trim();
                if (string.IsNullOrWhiteSpace(input)) return;

                AddMessage($"User: \"{input}\"", Brushes.Red);
                string response = AnalyzeInput(input.ToLower());
                AddMessage($"Chatbot: {response}", Brushes.Green);

                txtUserInput.Clear();
            }

            private void AddMessage(string message, Brush color)
            {
                var block = new TextBlock
                {
                    Text = message,
                    Foreground = color,
                    FontSize = 14,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 5, 0, 5)
                };
                chatStack.Children.Add(block);
            }

        private string AnalyzeInput(string input)
        {
            input = input.ToLower().Trim();

            // ✅ Clean up punctuation (optional)
            input = Regex.Replace(input, @"[^\w\s]", "");

            // ✅ TASKS
            if (input.Contains("add task") || input.Contains("task"))
            {
                string topic = ExtractTopic(input, "add task", "task");
                activityLog.Add($"[Task] {DateTime.Now:HH:mm} - '{topic}' created.");

                var taskList = new List<TaskItem>
        {
            new TaskItem { Description = topic, IsCompleted = false }
        };

                var taskWindow = new TaskManagerWindow(taskList);
                taskWindow.ShowDialog();

                return $"Task added: '{topic}'. Want a reminder for it?";
            }

            // ✅ REMINDERS
            if (input.Contains("remind me") || input.Contains("set reminder"))
            {
                string topic = ExtractTopic(input, "remind me to", "set reminder to", "reminder");
                string msg = $"Reminder set for '{topic}' on tomorrow's date.";
                activityLog.Add($"[Reminder] {DateTime.Now:HH:mm} - '{topic}' set.");
                return msg;
            }

            // ✅ QUIZ
            if (input.Contains("quiz") || input.Contains("start quiz") || input.Contains("take quiz"))
            {
                activityLog.Add($"[Quiz] {DateTime.Now:HH:mm} - Quiz started.");
                var quiz = new QuestionQuiz();
                quiz.ShowDialog();
                return "Quiz launched. Good luck!";
            }

            // ✅ CYBERSECURITY
            if (input.Contains("password"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Asked about passwords.");
                return "Always use strong, unique passwords and enable two-factor authentication (2FA).";
            }
            if (input.Contains("phishing"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Asked about phishing.");
                return "Phishing is a scam where attackers trick you via fake emails or links.";
            }
            if (input.Contains("wifi"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Asked about public Wi-Fi.");
                return "Avoid entering sensitive info on public Wi-Fi unless you're using a VPN.";
            }
            if (input.Contains("2fa") || input.Contains("authentication"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Asked about 2FA.");
                return "2FA adds an extra layer of protection beyond just your password.";
            }
            if (input.Contains("cybersecurity info") || input.Contains("open cybersecurity"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Opened Cyber Info Page.");
                var infoPage = new CyberSecurityInfoPage();
                infoPage.ShowDialog();
                return "Opening cybersecurity info page...";
            }

            // ✅ ACTIVITY LOG
            if (input.Contains("activity log") || input.Contains("what have you done"))
            {
                return BuildActivityLog();
            }

            // ❌ UNKNOWN INPUT
            return "Sorry, I didn't quite get that. Try asking about a task, quiz, or cybersecurity topic.";
        }



        private string ExtractTopic(string input, params string[] triggers)
        {
            foreach (var trigger in triggers)
            {
                if (input.Contains(trigger))
                {
                    string[] parts = input.Split(new[] { trigger }, StringSplitOptions.None);
                    if (parts.Length > 1)
                        return parts[1].Trim();
                }
            }
            return "unspecified action";
        }


        private string RespondToCyberSecurityTopic(string input)
        {
            if (input.Contains("phishing"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Asked about phishing.");
                return "Phishing is a type of scam where fake messages trick you into revealing personal info.";
            }
            if (input.Contains("wifi"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Asked about public Wi-Fi.");
                return "Avoid logging into sensitive accounts over public Wi-Fi unless you're using a VPN.";
            }
            if (input.Contains("password"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Asked about passwords.");
                return "Use long, unique passwords. Consider a password manager.";
            }
            if (input.Contains("2fa") || input.Contains("authentication"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Asked about 2FA.");
                return "2FA adds an extra layer of security by requiring something you know AND something you have.";
            }
            if (input.Contains("malware"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Asked about malware.");
                return "Malware is malicious software. Use antivirus and avoid suspicious downloads.";
            }
            if (input.Contains("social engineering"))
            {
                activityLog.Add($"[Cyber] {DateTime.Now:HH:mm} - Asked about social engineering.");
                return "This technique manipulates people into giving up confidential info. Stay cautious!";
            }

            return "Cybersecurity is important! Ask me about phishing, 2FA, passwords, or malware.";
        }


       

            private string BuildActivityLog()
            {
                if (activityLog.Count == 0) return "No actions logged yet.";

                string log = "Here’s a summary of recent actions:\n";
                int count = Math.Min(activityLog.Count, 10);
                for (int i = activityLog.Count - count; i < activityLog.Count; i++)
                {
                    log += $"{i - (activityLog.Count - count) + 1}. {activityLog[i]}\n";
                }
                return log.Trim();
            }

            // Manual Button Clicks
            private void StartQuiz_Click(object sender, RoutedEventArgs e)
            {
                activityLog.Add("Quiz manually started.");
                var quiz = new QuestionQuiz();
                quiz.ShowDialog();
            }

            private void OpenTaskManager_Click(object sender, RoutedEventArgs e)
            {
                activityLog.Add("Opened Task Manager manually.");
                var taskWin = new TaskManagerWindow(new List<TaskItem>());
                taskWin.ShowDialog();
            }

            private void OpenCyberInfo_Click(object sender, RoutedEventArgs e)
            {
                activityLog.Add("Opened Cyber Info page manually.");
                var info = new CyberSecurityInfoPage();
                info.ShowDialog();
            }

            private void ShowActivityLog_Click(object sender, RoutedEventArgs e)
            {
                string summary = BuildActivityLog();
                AddMessage($"Chatbot: {summary}", Brushes.Green);
            }
        }
    }
