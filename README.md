# CyberSecurityBot2

# 🛡️ Cybersecurity Awareness Chatbot

This C# console-based chatbot is designed to educate users about common cybersecurity topics and best practices. It provides helpful guidance using keyword recognition, random responses, sentiment-aware replies, memory recall, and follow-up conversations — all within a friendly and interactive environment.

Features

Keyword Recognition
The bot identifies key cybersecurity terms (e.g., `password`, `scam`, `phishing`, `privacy`) and provides useful, topic-specific tips.

 Random Responses
Some topics (like `phishing`) return a random helpful tip to keep the conversation dynamic and engaging.

 Natural Conversation Flow
The chatbot maintains a `currentTopic` to track the conversation. Users can ask for "more" information about a topic without repeating the question.

 🧠 Memory & Recall
- Stores the user's name to personalize greetings.
- Remembers interests (e.g., "I'm interested in privacy") and refers back to them in future responses.

Sentiment Detection
Detects simple emotional keywords like:
- worried
- curious
- frustrated

and adjusts tone to provide encouragement or support.

 Error Handling
Handles unknown inputs gracefully by prompting the user to rephrase instead of crashing or becoming unresponsive.

 Code Optimization
- Uses dictionaries, lists, and string matching to efficiently handle logic.
- Fully contained in a single file (`Chatbot.cs`) for simplicity.


Technologies Used

- Language: **C# (.NET Framework or .NET Core)**
- Platform: **Console Application**
- Classes:
  - `SoundManager` (for audio effects)
  - `LogoDisplay` (for ASCII headers)
  - `ConsoleTypeEffect` (typing animation effect)
  - `Chatbot` (main interaction logic)

 



