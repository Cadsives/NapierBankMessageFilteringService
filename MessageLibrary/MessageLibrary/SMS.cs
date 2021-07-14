using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;

namespace MessageLibrary
{
    public class SMS: Message
    {
        // Stores the phone number
        private string theNumber = "";

        // Constructor for SMS passing string message
        public SMS(String message)
        {
            // Sets the text of message to the message in the constructor
            theMessage = message;
            // Uses the strategy pattern to set the behaviour of the SMS
            setAbbreviate(new Abbreviate());
        }

        // This method used to save the SMS
        // Abstract forced by the Message Class
        public override void saveFile()
        {
            // Stores the ID of the message
            string theID = "S" + RandomDigits(9);

            // Stores the path to the Json File Folder 
            string path = Environment.CurrentDirectory + @"\Json File Folder\" + theID + ".json";

            // Creates a new instance of the Java Script Serializer
            JavaScriptSerializer ser = new JavaScriptSerializer();

            // Creates a new instance of TheMessage class and saves it as a SMS message
            TheMessage myMessage = new TheMessage() { messageID = theID, messageType = "SMS", sendType = phoneNumber, subject = "N/A", message = theMessage };
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

        // Getter and Setter used to get and set the phone number
        public string phoneNumber
        {
            get { return this.theNumber; }
            set { this.theNumber = value; }
        }
    }
}
