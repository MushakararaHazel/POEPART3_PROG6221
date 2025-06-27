using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Part3POE
{
    public  class MemoryService
    {
        private Dictionary<string, string> _userInterests = new Dictionary<string, string>();
        private List<string> _rememberedTopics = new List<string>();

        public void RememberInterest(string topic, string context)
        {
            string lowerTopic = topic.ToLower();
            if (!_userInterests.ContainsKey(lowerTopic))
            {
                _userInterests.Add(lowerTopic, context);
                _rememberedTopics.Add(topic); // Store original casing
            }
        }

        public bool HasInterestIn(string topic)
        {
            return _userInterests.ContainsKey(topic.ToLower());
        }

        public List<string> GetRememberedTopics()
        {
            return new List<string>(_rememberedTopics);
        }

        public void ClearMemory()
        {
            _userInterests.Clear();
            _rememberedTopics.Clear();
        }
    }
}
    

