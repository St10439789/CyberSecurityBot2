using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityBot2
{
   public  class Chatbot
   
        {
            // Sound, logo, and typing effect managers
            private SoundManager _soundManager;
            private LogoDisplay _logoDisplay;
            private ConsoleTypeEffect consoleType = new ConsoleTypeEffect();

            // Memory for user data (e.g., name, interest)
            private Dictionary<string, string> memory = new Dictionary<string, string>();

            // Random generator for varied responses
            private Random random = new Random();

            // Tracks the last topic discussed for "more" responses
            private string currentTopic = "";

            public Chatbot()
            {
                _soundManager = new SoundManager();
                _logoDisplay = new LogoDisplay();
            }

            public void Start()
            {
                _soundManager.PlayGreeting();
                _logoDisplay.DisplayHeader();
                _logoDisplay.DisplayAsciiLogo();
                AskUserName();
                BasicResponseSystem();
            }

            // Ask the user for their name and store it
            private void AskUserName()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                consoleType.typyingeffect("\nPlease enter your name: ");
                Console.ResetColor();

                string userName = Console.ReadLine();
                memory["name"] = userName;

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                consoleType.typyingeffect($"\nHello, {userName}! Welcome to the Cybersecurity Awareness Bot.");
                Console.ResetColor();
            }

            // Main chatbot interaction loop
            public void BasicResponseSystem()
            {
                string userName = memory.ContainsKey("name") ? memory["name"] : "User";

                Console.ForegroundColor = ConsoleColor.Cyan;
                consoleType.typyingeffect($"\nHello {userName}, I’m here to help you stay safe online. Ask me something about cybersecurity!");
                Console.ResetColor();

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(userName + ": ");
                    Console.ResetColor();

                    string input = Console.ReadLine().ToLower();
                    Console.WriteLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        ShowMessage("I didn't quite understand that. Could you rephrase?", ConsoleColor.Red);
                        continue;
                    }

                    if (input.Contains("exit") || input.Contains("goodbye"))
                    {
                        ShowMessage("Goodbye! Stay safe online!", ConsoleColor.Green);
                        break;
                    }

                    // Respond empathetically if sentiment detected
                    if (DetectSentiment(input, out string sentimentResponse))
                    {
                        ShowMessage(sentimentResponse, ConsoleColor.Yellow);
                    }

                    // Store user interest
                    if (input.Contains("interested in "))
                    {
                        string interest = input.Substring(input.IndexOf("interested in ") + 13);
                        memory["interest"] = interest;
                        ShowMessage($"Great! I'll remember that you're interested in {interest}.", ConsoleColor.Cyan);
                        continue;
                    }

                    // Handle "more" follow-up
                    if (input.Contains("more") && !string.IsNullOrEmpty(currentTopic))
                    {
                        ShowMessage(GetMoreDetails(currentTopic), ConsoleColor.Blue);
                        continue;
                    }

                    // Process topic-related response
                    string response = GetResponse(input);

                    if (!string.IsNullOrEmpty(response))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("CyberSecurityBot: ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(response);
                        Console.ResetColor();

                        if (!string.IsNullOrEmpty(currentTopic) && memory.ContainsKey("interest"))
                        {
                            ShowMessage($"As someone interested in {memory["interest"]}, this topic is especially relevant for you.", ConsoleColor.DarkCyan);
                        }
                    }
                    else
                    {
                        ShowMessage("Sorry, I didn’t quite understand that. Can you try rephrasing?", ConsoleColor.Red);
                    }

                    Console.WriteLine();
                }
            }

            // Returns a response based on keywords or randomly selected tip
            private string GetResponse(string input)
            {
                var keywordResponses = new Dictionary<string[], object>
            {
                { new[] { "password", "password safety" }, "Use strong, unique passwords. Avoid using personal info." },
                { new[] { "scam" }, "Be cautious of unexpected messages asking for personal info." },
                { new[] { "privacy" }, "Adjust your social media and browser privacy settings to control what you share." },
                { new[] { "vpn" }, "A VPN creates a secure connection and protects your data online." },
                { new[] { "phishing", "phishing tip" }, new List<string>
                    {
                        "Be cautious of emails asking for personal information.",
                        "Check URLs carefully before clicking.",
                        "Look for spelling mistakes or suspicious sender addresses."
                    }
                },
                { new[] { "malware" }, "Malware is harmful software designed to damage or steal information." },
                { new[] { "ransomware" }, "Ransomware locks your files and demands a payment to unlock them." },
                { new[] { "social engineering" }, "It tricks users into giving up confidential information via deception." },
                { new[] { "sabotage" }, "Sabotage involves intentionally damaging or disabling systems." },
                { new[] { "safe browsing" }, "Use HTTPS sites, and avoid entering data on unknown websites." }
            };

                foreach (var pair in keywordResponses)
                {
                    foreach (var keyword in pair.Key)
                    {
                        if (input.Contains(keyword))
                        {
                            currentTopic = keyword;

                            if (pair.Value is string singleResponse)
                                return singleResponse;

                            if (pair.Value is List<string> responseList)
                                return responseList[random.Next(responseList.Count)];
                        }
                    }
                }

                return null;
            }

            // Detect simple sentiment in user input
            private bool DetectSentiment(string input, out string response)
            {
                var sentiments = new Dictionary<string, string>
            {
                { "worried", "It's completely understandable to feel that way. Let me help you feel more confident online." },
                { "curious", "Curiosity is great! Let’s explore cybersecurity together shall we?." },
                 {"scared","it is okay to feel scared but i am always here anytime when you need assistance" },
                { "frustrated", "Cybersecurity can be tricky, but I’m here to help!" }
            };

                foreach (var sentiment in sentiments)
                {
                    if (input.Contains(sentiment.Key))
                    {
                        response = sentiment.Value;
                        return true;
                    }
                }

                response = null;
                return false;
            }

            // Provide additional detail on a previously discussed topic
            private string GetMoreDetails(string topic)
            {
                var moreDetails = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", "Use a password manager and enable two-factor authentication. Change your passwords regularly." },
                { "scam", "Check the sender’s identity. Scammers often use urgency and fake names to pressure you." },
                { "privacy", "Use browser extensions that block tracking. Review app permissions regularly." },
                { "vpn", "Choose a trustworthy provider and keep it enabled on public networks. Avoid free VPNs if possible." },
                { "phishing", "Hover over links to preview URLs. Don't reply to suspicious messages. Report them when possible." },
                { "malware", "Avoid pirated software, and keep all your apps and systems updated." },
                { "ransomware", "Back up your files regularly and disconnect infected devices immediately." },
                { "social engineering", "Verify requests in person or via trusted methods. Don't share sensitive data over email or chat." },
                { "sabotage", "Limit user permissions in critical systems and monitor for suspicious internal activity." },
                { "safe browsing", "Use a secure browser and avoid clicking unknown popups or links." }
            };

                return moreDetails.ContainsKey(topic)
                    ? moreDetails[topic]
                    : "I don't have more details on that topic yet.";
            }

            // Helper method for printing styled text
            private void ShowMessage(string message, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                consoleType.typyingeffect(message);
                Console.ResetColor();
            }
        }
    }




