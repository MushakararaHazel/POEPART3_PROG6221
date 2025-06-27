using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3POE
{
    public  class SentimentAnalyzer
    {
        public string AnalyzeSentiment(string input)
        {
            input = input.ToLower();

            if (ContainsNegativeWords(input)) return "worried";
            if (ContainsFrustrationWords(input)) return "frustrated";
            if (ContainsPositiveInterestWords(input)) return "curious";

            return "neutral";
        }
        private bool ContainsNegativeWords(string input)
        {
            string[] negativeWords = { "worried", "concerned", "scared", "nervous", "afraid" };
            return negativeWords.Any(word => input.Contains(word));
        }

        private bool ContainsFrustrationWords(string input)
        {
            string[] frustrationWords = { "frustrated", "angry", "annoyed", "upset", "mad" };
            return frustrationWords.Any(word => input.Contains(word));
        }
        private bool ContainsPositiveInterestWords(string input)
        {
            string[] interestWords = { "curious", "interested", "wondering", "tell me", "explain" };
            return interestWords.Any(word => input.Contains(word));
        }

        public string GetResponseForSentiment(string sentiment, string topic)
        {
            switch (sentiment)
            {
                case "worried":
                    return $"I understand your concerns about {topic}. Let me help you feel more secure...";
                case "frustrated":
                    return $"I hear your frustration about {topic}. Cybersecurity can be challenging, but I'm here to help...";
                case "curious":
                    return $"That's great you're curious about {topic}! Here's what you should know...";
                default:
                    return $"Regarding {topic}, here's some important information...";
            }
        }
    }
}
    
