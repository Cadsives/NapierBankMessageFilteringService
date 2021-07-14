using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace MessageLibrary
{
    public abstract class Message
    {
        // Stores the message
        private string message = "";

        // Stores the abbreviate behaviour
        protected Abbreviations _abbreviate;

        // Stores the URL behaviour
        protected Urls _urls;

        // Creates an abstract method saveFile to force all inherited classes to implement it
        public abstract void saveFile();

        // The message's blank constructor
        public Message() { }

        // Abbreviate Methods

        public void performAbbreviate()
        {
            this._abbreviate.abbreviate(theMessage);
        }

        public Abbreviations getAbbreviate()
        {
            return this._abbreviate;
        }

        public void setAbbreviate(Abbreviations _abbreviate)
        {
            this._abbreviate = _abbreviate;
        }

        // Url Methods
        public void performUrlQua()
        {
            this._urls.quarantine(theMessage);
        }

        public Urls getUrlQua()
        {
            return this._urls;
        }

        public void setUrlQua(Urls _urls)
        {
            this._urls = _urls;
        }

        // Getter and Setter used to get and set the message text
        protected string theMessage
        {
            get { return this.message; }
            set { this.message = value; }
        }

        // This method used to generate 9 random digits to be included in the message id
        protected string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(9).ToString());
            return s;
        }
    }

    public class TheMessage
    {
        // Variables used to serialize the json file
        public string messageID { get; set; }
        public string messageType { get; set; }
        public string sendType { get; set; }
        public string subject { get; set; }
        public string message { get; set; }

        // Sets up the relevant lists and are ignored by the java serializer
        [ScriptIgnore]
        public List<string> theHashtags = new List<string>(); // For storing i.e (#TheMovie)
        [ScriptIgnore]
        public List<string> theMentions = new List<string>(); // For storing i.e (@Ben)
        [ScriptIgnore]
        public List<string> theSIR = new List<string>(); // For stroing i.e (SIR 09/09/16 Theft)
        [ScriptIgnore]
        public List<string> theURLS = new List<string>(); // For storing i.e (www.bbcnews.co.uk)

        // This method used to return the contents of each json file
        public string returnMessage()
        {
            // Checks each type of the message to ensure they are categorised and returned correctly
            if (messageType.Equals("SMS"))
            {
                // Creates an instacnes of SMS message with the parameter string message 
                SMS mySMS = new SMS(message);
                
                // Performs the abbreviate method to expand textspeak text
                mySMS.performAbbreviate();
                
                // Returns the SMS in the correct format
                return String.Format("\nMessage ID: {0} \nSender: {1} \nBody: {2}\n", messageID, sendType, message = mySMS.getAbbreviate().abbreviate(message));
            }
            else if (messageType.Equals("Email"))
            {
                // Creates a new instance of Email message with the parameter string message 
                Email myEmail = new Email(message);
                
                // Performs the url quarantine method to qurantined all urls
                myEmail.performUrlQua();
                
                // Returns the Email in the correct format
                return String.Format("\nMessage ID: {0} \nSender: {1} \nSubject: {2} \nBody: {3}\n", messageID, sendType, subject, message = myEmail.getUrlQua().quarantine(message));
            }
            else if(messageType.Equals("SIR"))
            {
                // Creates an instance of the Email message with the parameter string message
                Email myEmail = new Email(message);

                // Sets the regular expression for checking the message for urls 
                var regx = new Regex("((http://|www\\.)([A-Z0-9.-:]{1,})\\.[0-9A-Z?;~&#=\\-_\\./]{2,})", RegexOptions.IgnoreCase);

                // Checks the message for urls (www.bbcnew.co.uk)
                var urls = regx.Matches(message);

                // Adds any matches found to the URLs list
                foreach (Match m in urls)
                {
                    theURLS.Add(m.Value);
                }

                // Stores splitMessage as message
                String splitMessage = message;

                // Gets the first line in message
                String theSIRText = splitMessage.Split('\r')[0];

                // Adds the sort code and incident to the SIR list
                theSIR.Add(theSIRText);

                // Performs the url quarantine method to qurantined all urls
                myEmail.performUrlQua();
                // Returns the Email in the correct format
                return String.Format("\nMessage ID: {0} \nSender: {1} \nSubject: {2} \nMessage:\n{3}\n", messageID, sendType, subject, message = myEmail.getUrlQua().quarantine(message));
            }
            else if (messageType.Equals("Tweet"))
            {
                // Creates an instance of the Tweet message with the parameter string message
                Tweets myTweet = new Tweets(message);

                // Sets the regular expression for checking the message for hashtags
                var hashtagPattern = new Regex(@"(?<=#)\w+");

                // Checks the message for hashtags (#TheMovie)
                var hashtags = hashtagPattern.Matches(message);

                // Adds any matches found to the Hashtags list
                foreach (Match m in hashtags)
                {
                    theHashtags.Add(m.Value);
                }

                // Sets the regular expression for checking the message for mentions
                var mentionPattern = new Regex(@"(?<=@)\w+");

                // Checks the message for mentions (@Ben)
                var mentions = mentionPattern.Matches(message);

                // Adds any matches found to the Mentions list
                foreach (Match m in mentions)
                {
                    theMentions.Add(m.Value);
                }

                // Performs the abbreviate method to expand textspeak text
                myTweet.performAbbreviate();
                // Returns the Tweet in the correct format
                return String.Format("\nMessage ID: {0} \nSender: {1} \nBody: {2}\n", messageID, sendType, message = myTweet.getAbbreviate().abbreviate(message));
            }
            // Just because something needs to be returned
            return null;
        }
    }
}
