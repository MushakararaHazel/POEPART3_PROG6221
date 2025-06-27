using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3POE.Model
{

    public static class ActivityLogManager
    {
        private static List<string> log = new List<string>();

        public static void AddLog(string description)
        {
            string timestamped = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {description}";
            log.Add(timestamped);
        }
        public static List<string> GetLog()
        {
            return new List<string>(log); // Return a copy to protect internal list
        }
    }
}
