
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Part3POE.PART2
{
    public partial class CyberSecurityInfoPage : Window
    {
        private SentimentAnalyzer _sentimentAnalyzer;
        private MemoryService _memory;
        private Dictionary<string, Action> _keywordHandlers;
        private UserInterface _ui;

        public CyberSecurityInfoPage()
        {
            InitializeComponent();
            _ui = new UserInterface(ChatList);
            _sentimentAnalyzer = new SentimentAnalyzer();
            _memory = new MemoryService();
            _keywordHandlers = InitializeKeywordHandlers();
        }

        private Dictionary<string, Action> InitializeKeywordHandlers()
        {
            return new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
            {
                ["password"] = ShowPasswordTips,
                ["scam"] = ShowScamInfo,
                ["privacy"] = ShowPrivacyInfo,
                ["phishing"] = ShowPhishingInfo,
                ["authentication"] = ShowTwoFactorAuthInfo,
                ["2fa"] = ShowTwoFactorAuthInfo,
                ["virus"] = ShowMalwareInfo,
                ["malware"] = ShowMalwareInfo,
                ["hack"] = ShowHackingPreventionInfo,
                ["social media"] = ShowSocialMediaSafetyInfo,
                ["wifi"] = ShowPublicWifiSafetyInfo,
                ["public wifi"] = ShowPublicWifiSafetyInfo
            };
        }

        private void Ask_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(userInput))
                return;

            _ui.AddUserMessage(userInput);

            string keyword = _keywordHandlers.Keys
                .FirstOrDefault(k => userInput.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);

            string sentiment = _sentimentAnalyzer.AnalyzeSentiment(userInput);

            if (keyword != null)
            {
                if (userInput.ToLower().Contains("curious") || userInput.ToLower().Contains("interested"))
                {
                    _memory.RememberInterest(keyword, userInput);
                    _ui.AddBotMessage($"I'll remember you're interested in {keyword}.");
                    Thread.Sleep(300);
                }

                var rememberedTopics = _memory.GetRememberedTopics();
                if (rememberedTopics.Any())
                {
                    _ui.AddBotMessage($"As someone interested in {string.Join(" and ", rememberedTopics)},");
                    Thread.Sleep(300);
                }

                if (sentiment != "neutral")
                {
                    _ui.AddBotMessage(_sentimentAnalyzer.GetResponseForSentiment(sentiment, keyword));
                    Thread.Sleep(300);
                }

                _keywordHandlers[keyword].Invoke();
            }
            else
            {
                _ui.AddBotMessage("🤖 I'm not sure I understand. Try asking about:");
                _ui.AddBotTip("- Passwords\n- Scams\n- Privacy\n- Phishing\n- 2FA\n- Malware\n- WiFi");
            }

            UserInput.Clear();
        }

        // === Response Methods ===

        private void ShowPasswordTips()
        {
            _ui.AddBotMessage("Strong passwords use at least 12 characters and a mix of symbols.");
            _ui.AddBotTip("Avoid dictionary words. Use passphrases and password managers.");
        }

        private void ShowScamInfo()
        {
            _ui.AddBotMessage("Common scams include tech support, romance, and investment frauds.");
            _ui.AddBotTip("Never send money to strangers. Always verify identities.");
        }

        private void ShowPrivacyInfo()
        {
            _ui.AddBotMessage("Limit what you share online, and review privacy settings.");
            _ui.AddBotTip("Use VPNs and secure browsers like Brave or Firefox.");
        }

        private void ShowPhishingInfo()
        {
            _ui.AddBotMessage("Phishing attempts use fake emails or websites to steal data.");
            _ui.AddBotTip("Check the sender’s address and hover over links before clicking.");
        }

        private void ShowTwoFactorAuthInfo()
        {
            _ui.AddBotMessage("2FA improves security using SMS codes or authenticator apps.");
            _ui.AddBotTip("Enable it on your email, bank, and social media accounts.");
        }

        private void ShowMalwareInfo()
        {
            _ui.AddBotMessage("Malware includes viruses and spyware that harm your device.");
            _ui.AddBotTip("Avoid unknown downloads and keep antivirus software updated.");
        }

        private void ShowHackingPreventionInfo()
        {
            _ui.AddBotMessage("Protect against hacking by using strong passwords and alerts.");
            _ui.AddBotTip("Enable 2FA and monitor for unusual account activity.");
        }

        private void ShowSocialMediaSafetyInfo()
        {
            _ui.AddBotMessage("Social media safety starts with privacy settings.");
            _ui.AddBotTip("Avoid oversharing and disable location tracking.");
        }

        private void ShowPublicWifiSafetyInfo()
        {
            _ui.AddBotMessage("Public WiFi can be dangerous due to fake hotspots and eavesdropping.");
            _ui.AddBotTip("Use a VPN and avoid logging into sensitive accounts.");
        }
    }
}
