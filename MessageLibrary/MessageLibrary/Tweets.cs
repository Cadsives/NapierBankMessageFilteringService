using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace MessageLibrary
{
    public class Tweets : Message
    {
        // Stores the twitter address
        private string theTwitterAddress = "";

        public Tweets(String message)
        {
            // Sets the text of message to the message in the constructor
            theMessage = message;
            // Uses the strategy pattern to set the behaviour of the Tweet
            setAbbreviate(new Abbreviate());
        }

        // This method used to save the Tweet
        // Abstract forced by the Message Class
        public override void saveFile()
        {
            // Stores the ID of the message
            string theID = "T" + RandomDigits(9);

            // Stores the path to the Json File Folder 
            string path = Environment.CurrentDirectory + @"\Json File Folder\" + theID + ".json";

            // Creates a new instance of the Java Script Serializer
            JavaScriptSerializer ser = new JavaScriptSerializer();

            // Creates a new instance of TheMessage class and saves it as a Tweet message
            TheMessage myMessage = new TheMessage() { messageID = theID, messageType = "Tweet", sendType = twitterAddress, subject = "N/A", message = theMessage };
            // Serialize the message
            string json = ser.Serialize(myMessage);

            // Saves the filename as the ID of the message and uses a do while loop to ensure the ID is not taking twice
            do
            {
                // Writes text to json
                File.WriteAllText(path, json);
            }
            // Repeats the loop if the ID is already taken
            while (!File.Exists(path));
        }

        // Getter and Setter used to get and set the twitter address
        public string twitterAddress
        {
            get { return this.theTwitterAddress; }
            set { this.theTwitterAddress = value; }
        }
    }
}

