using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Part3POE.Model
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            string reminder = ReminderDate.HasValue ? $" (Reminder: {ReminderDate.Value:yyyy-MM-dd})" : "";
            string status = IsCompleted ? "[Completed]" : "[Pending]";
            return $"{status} {Title}: {Description}{reminder}";
            return Name;
        }
    }

}
