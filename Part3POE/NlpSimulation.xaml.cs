using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Part3POE;                     // For QuestionQuiz
using Part3POE.Model;              // For TaskManagerWindow
using Part3POE.PART2;              // For CyberSecurityInfoPage

namespace Part3POE
{
    public partial class NlpSimulation : Window
    {
        private List<string> activityLog = new List<string>();

        public NlpSimulation()
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
            TextBlock block = new TextBlock
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
            if (input.Contains("remind me") || input.Contains("set reminder"))
            {
                string topic = ExtractTopic(input, "remind me to", "set reminder to");
                string msg = $"Reminder set for '{topic}' on tomorrow's date.";
                activityLog.Add($"Reminder set for '{topic}' on tomorrow.");
                return msg;
            }

            if (input.Contains("add a task") || input.Contains("add task"))
            {
                string topic = ExtractTopic(input, "add a task to", "add task to");
                activityLog.Add($"Task added: '{topic}' (no reminder set).");

                // 🟩 Launch Task Window
                TaskManagerWindow taskWindow = new TaskManagerWindow(new List<TaskItem>());
                taskWindow.ShowDialog();


                return $"Task added: '{topic}'. Would you like to set a reminder for this task?";
            }

            if (input.Contains("quiz") || input.Contains("start quiz"))
            {
                activityLog.Add("Quiz started - 5 questions answered.");

                // 🟩 Launch Quiz Window
                QuestionQuiz quiz = new QuestionQuiz();
                quiz.ShowDialog();

                return "Launching the quiz now!";
            }

            // ✅ Cybersecurity Info Handling
            if (input.Contains("phishing"))
            {
                activityLog.Add("User asked about phishing.");
                return "Phishing is a scam where attackers trick you into giving personal information via fake emails or messages.";
            }
            if (input.Contains("wifi") || input.Contains("public wifi"))
            {
                activityLog.Add("User asked about Wi-Fi safety.");
                return "Avoid accessing sensitive accounts on public Wi-Fi. Always use a VPN or mobile data when possible.";
            }
            if (input.Contains("password"))
            {
                activityLog.Add("User asked about passwords.");
                return "Use strong, unique passwords and enable two-factor authentication (2FA) where possible.";
            }
            if (input.Contains("2fa") || input.Contains("two-factor") || input.Contains("authentication"))
            {
                activityLog.Add("User asked about 2FA.");
                return "2FA (Two-Factor Authentication) adds a second layer of security to protect your accounts from unauthorized access.";
            }
            if (input.Contains("malware") || input.Contains("virus"))
            {
                activityLog.Add("User asked about malware.");
                return "Malware is malicious software designed to harm or exploit systems. Use antivirus software and avoid downloading unknown files.";
            }
            if (input.Contains("social engineering"))
            {
                activityLog.Add("User asked about social engineering.");
                return "Social engineering involves manipulating people into giving up confidential info. Be cautious of unexpected requests for personal data.";
            }

            // ✅ Launch full Cyber Info Page only when clearly asked
            if (input.Contains("cybersecurity info") || input.Contains("open cybersecurity info") || input.Contains("more about cybersecurity"))
            {
                activityLog.Add("Cybersecurity info page opened.");
                CyberSecurityInfoPage infoPage = new CyberSecurityInfoPage();
                infoPage.ShowDialog();
                return "Opening cybersecurity information page...";
            }


            if (input.Contains("activity log") || input.Contains("what have you done"))
            {
                return BuildActivityLogSummary();
            }

            return "I'm not sure how to help with that. Try asking me to add a task, set a reminder, start a quiz, or open cybersecurity info.";
        }

        private string ExtractTopic(string input, params string[] triggers)
        {
            foreach (var trigger in triggers)
            {
                if (input.Contains(trigger))
                {
                    return input.Split(new[] { trigger }, StringSplitOptions.None)[1].Trim();
                }
            }
            return "unspecified action";
        }

        private string BuildActivityLogSummary()
        {
            if (activityLog.Count == 0)
                return "No recent actions found.";

            string summary = "Here’s a summary of recent actions:\n";
            int count = Math.Min(activityLog.Count, 10); // show last 10
            for (int i = activityLog.Count - count; i < activityLog.Count; i++)
            {
                summary += $"{i - (activityLog.Count - count) + 1}. {activityLog[i]}\n";
            }

            return summary.Trim();
        }
    }
}
