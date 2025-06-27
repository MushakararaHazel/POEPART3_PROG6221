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
    /// Interaction logic for ActivityLogWindow.xaml
    /// </summary>
    public partial class ActivityLogWindow : Window
    {
            public ActivityLogWindow(List<string> activityLog)
            {
                InitializeComponent();

                // Limit to last 10 entries for clarity
                int logCount = activityLog.Count;
                int startIndex = logCount > 10 ? logCount - 10 : 0;
                var recentLogs = activityLog.GetRange(startIndex, logCount - startIndex);

                foreach (var entry in recentLogs)
                {
                    lstActivityLog.Items.Add(entry);
                }

                if (lstActivityLog.Items.Count == 0)
                {
                    lstActivityLog.Items.Add("No actions have been logged yet.");
                }
            }

            private void Close_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
        }
    }


