using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace MessageLibrary
{
    public class Email : Message
    {
        // Stores the isSIR value
        private bool isSIR = false;
        // Stores the Email address
        private string theEmail = "";
        // Stores the subject text
        private string theSubject = "";

        // Constructor for Email passing string message
        public Email(String message)
        {
            // Sets the text of the message to the message in the constructor
            theMessage = message;
            // Uses the strategy pattern to set the behaviour of the Email
            setUrlQua(new Quarantine());
        }

        // This method used to save the Emails and SIR messages
        // Abstract forced by the Message Class
        public override void saveFile()
        {
            // Stores the ID of the message
            string theID = "E" + RandomDigits(9);

            // Stores the path to the Json File Folder 
            string path = Environment.CurrentDirectory + @"\Json File Folder\" + theID + ".json";

            // Creates a new instance of the Java Script Serializer
            JavaScriptSerializer ser = new JavaScriptSerializer();

            // Checks if the Email is an Email or SIR
            if (sirMessage == false)
            {
                // If false, creates a new instance of TheMessage class and saves the email as a standard email
                TheMessage myMessage = new TheMessage() { messageID = theID, messageType = "Email", sendType = emailAddress, subject = messageSubject, message = theMessage };
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
            else if (sirMessage == true)
            {
                // If true, creates a new instance of TheMessage class and saves it a SIR
                TheMessage myMessage = new TheMessage() { messageID = theID, messageType = "SIR", sendType = emailAddress, subject = messageSubject, message = theMessage };
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
        }

        // Getter and Setter used to get and set the email address
        public string emailAddress
        {
            get{ return this.theEmail; }
            set { this.theEmail = value; }
        }

        // Getter and Setter used to get and set the message subject
        public string messageSubject
        {
            get { return this.theSubject; }
            set { this.theSubject = value; }
        }

        // Getter and Setter used to get and set the sir message boolean
        public bool sirMessage
        {
            get { return this.isSIR; }
            set { this.isSIR = value; }
        }
    }
}
