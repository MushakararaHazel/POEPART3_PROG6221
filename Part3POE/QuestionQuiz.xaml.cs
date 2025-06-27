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

namespace Part3POE
{
    /// <summary>
    /// Interaction logic for QuestionQuiz.xaml
    /// </summary>
    public partial class QuestionQuiz : Window
    {
            private class Question
            {
                public string Text { get; set; }
                public List<string> Options { get; set; }
                public int CorrectIndex { get; set; }
                public string Explanation { get; set; }
            }

            private List<Question> questions;
            private int currentIndex = 0;
            private int score = 0;

            public QuestionQuiz()
            {
                InitializeComponent();
                LoadQuestions();
                ShowQuestion();
            }

            private void LoadQuestions()
            {
                questions = new List<Question>
            {
                new Question
                {
                    Text = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    CorrectIndex = 2,
                    Explanation = "Correct! Reporting phishing emails helps prevent scams."
                },
                new Question
                {
                    Text = "True or False: Using '123456' as a password is safe.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndex = 1,
                    Explanation = "Correct! It's one of the most common and insecure passwords."
                },
                new Question
                {
                    Text = "What is a secure way to create a password?",
                    Options = new List<string> { "Use your birthdate", "Use your name", "Use a random combination of letters, numbers and symbols", "Use 'password'" },
                    CorrectIndex = 2,
                    Explanation = "Correct! Complex passwords are harder to guess."
                },
                new Question
                {
                    Text = "Which of the following is a type of social engineering?",
                    Options = new List<string> { "Firewall", "Phishing", "Encryption", "Antivirus" },
                    CorrectIndex = 1,
                    Explanation = "Correct! Phishing is a common social engineering tactic."
                },
                new Question
                {
                    Text = "When should you update your software?",
                    Options = new List<string> { "Only when it crashes", "Only once a year", "Whenever updates are available", "Never" },
                    CorrectIndex = 2,
                    Explanation = "Correct! Updates often include important security patches."
                },
                new Question
                {
                    Text = "True or False: It’s safe to use public Wi-Fi for online banking.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndex = 1,
                    Explanation = "Correct! Public Wi-Fi is not secure for sensitive transactions."
                },
                new Question
                {
                    Text = "Which one is a good practice to secure your account?",
                    Options = new List<string> { "Use same password everywhere", "Use 2FA", "Write passwords on a sticky note", "Ignore security alerts" },
                    CorrectIndex = 1,
                    Explanation = "Correct! Two-factor authentication adds an extra layer of security."
                },
                new Question
                {
                    Text = "What should you do before clicking a link in an email?",
                    Options = new List<string> { "Click quickly", "Forward to friends", "Hover to check the URL", "Reply to verify" },
                    CorrectIndex = 2,
                    Explanation = "Correct! Always check the URL before clicking."
                },
                new Question
                {
                    Text = "What does a padlock icon in the browser mean?",
                    Options = new List<string> { "Website is locked", "It’s a scam", "Secure connection (HTTPS)", "You need a password" },
                    CorrectIndex = 2,
                    Explanation = "Correct! It means the site uses encryption (HTTPS)."
                },
                new Question
                {
                    Text = "Which action helps you stay safe from malware?",
                    Options = new List<string> { "Click ads", "Download pirated files", "Install security updates", "Ignore antivirus alerts" },
                    CorrectIndex = 2,
                    Explanation = "Correct! Updates patch vulnerabilities that malware exploits."
                }
            };
            }

            private void ShowQuestion()
            {
                if (currentIndex < questions.Count)
                {
                    var q = questions[currentIndex];
                    txtQuestion.Text = $"Question {currentIndex + 1}: {q.Text}";
                    lstOptions.ItemsSource = q.Options;
                    lstOptions.SelectedIndex = -1;
                    txtFeedback.Text = string.Empty;
                    btnNext.IsEnabled = false;
                }
                else
                {
                    ShowResult();
                }
            }

            private void btnSubmit_Click(object sender, RoutedEventArgs e)
            {
                if (lstOptions.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select an answer.");
                    return;
                }

                var current = questions[currentIndex];

                if (lstOptions.SelectedIndex == current.CorrectIndex)
                {
                    score++;
                    txtFeedback.Text = current.Explanation;
                    txtFeedback.Foreground = Brushes.Green;
                }
                else
                {
                    txtFeedback.Text = $"Incorrect. {current.Explanation}";
                    txtFeedback.Foreground = Brushes.Red;
                }

                btnNext.IsEnabled = true;
            }

            private void btnNext_Click(object sender, RoutedEventArgs e)
            {
                currentIndex++;
                ShowQuestion();
            }

            private void btnClose_Click(object sender, RoutedEventArgs e)
            {
                Close();
            }

            private void ShowResult()
            {
                txtQuestion.Text = $"You scored {score} out of {questions.Count}.";
                txtFeedback.Text = score >= 8
                    ? "Great job! You’re a cybersecurity pro!"
                    : "Keep learning to stay safe online!";

                lstOptions.ItemsSource = null;
                btnSubmit.IsEnabled = false;
                btnNext.IsEnabled = false;
            }
        }
    }


