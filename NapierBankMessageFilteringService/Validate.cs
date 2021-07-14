using MessageLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBankMessageFilteringService
{
    public class Validate
    {
        // Creates a link for Enter Message
        private EnterMessage myPage;

        // Creates an array list to store the incidents
        private static ArrayList theIncidents;

        // Stores the message text
        public static string messageText = "";

        // Stores the subject text
        public static string subjectText = "";

        // Stores the alert message text
        private string alertMessage = "";

        // Stores the credentials text
        private string credentials = "";

        // Stores the message type
        private static int messageType = 0;

        // Stores the chracter number
        private static int charNum = 0;

        // Regular expressions for checking the sort code pattern and SIR pattern
        private Regex sortCodePattern = new Regex(@"\b\d\d-\d\d-\d\d\b");
        private Regex subjectSIRPattern = new Regex(@"SIR (3[01]|[12][0-9]|0?[1-9])\/(1[0-2]|0?[1-9])\/(?:[0-9]{2})?[0-9]{2}");

        // Constructor passing linking Entermessage
        public Validate(EnterMessage myPage)
        {
            // Makes the mypage variable equal the variable being passed in the constructor
            this.myPage = myPage;
            fillTheIncidentsArray();
        }

        // This method validates the user's entry into the message header
        public void validateMessageCredentials()
        {
            // Stores the credentials variable as the result of the textbox
            credentials = this.myPage.txtMessageCredentials.Text;

            // Checks if the message header is a valid phone number
            if (IsPhoneNumber(credentials)) // Checks if the message type is in phone number format
            {
                // Sets the message type to 1
                messageType = 1;
            }
            // Checks if the message header is a valid email address
            else if (IsValidEmail(credentials)) // Checks if the message type is in email address format
            {
                // Sets the email related elements to visible when a valid email is typed in the message credential box
                myPage.lblSubject.Visibility = Visibility.Visible;
                myPage.txtSubject.Visibility = Visibility.Visible;

                // Sets the message type to 2
                messageType = 2;
            }
            // Checks if the message header is a valid twitter address
            else if (IsValidTwitterAddress(credentials)) // Checks if the message type is in twitter address format
            {
                // Sets the message type to 3
                messageType = 3;
            }
            // Checks if the message type is not valid or if the entry has been removed
            else
            {
                // Sets the email related elements to not be visible until an email address is typed into the messagebox credentials
                myPage.lblSubject.Visibility = Visibility.Hidden;
                myPage.txtSubject.Visibility = Visibility.Hidden;

                // Clears all the variables
                messageText = String.Empty;
                subjectText = String.Empty;
                alertMessage = String.Empty;
                credentials = String.Empty;

                messageType = 0;
                charNum = 0;
            }
        }

        // This method validates the message type, the message size and sends the message to the relevant class
        public void sendMessage()
        {
            // Checks if the message is a valid message type
            if (!IsValidMessageType())
            {
                // Adds to the alertMessage variable
                alertMessage += "Please enter a valid SMS number, Email or Tweet address into the message credential box!\n";
                // Displays the error in the message box
                messageBoxAlert(alertMessage);
            }
            else if (IsValidMessageType())
            {
                // Uses switch case to determine message type
                switch (messageType)
                {
                    case 1:
                        // Checks if the message exceeds 140 characters
                        if (messageCharNum > 140)
                        {
                            // Adds to the alertMessage variable
                            alertMessage += "SMS messages can only be 140 characters long!\n";
                        }
                        // Checks if no message has been typed in
                        else if (messageCharNum == 0)
                        {
                            // Adds to the alertMessage variable
                            alertMessage += "Please enter a message!\n";
                        }
                        // Sends the SMS message
                        else
                        {
                            // Creates a new instance of SMS and passes the messagetext
                            SMS myMessage = new SMS(messageText);

                            // Sets the phoneNumber getter and setter to the text contained in txtMessageCredentials
                            myMessage.phoneNumber = myPage.txtMessageCredentials.Text;

                            // Uses the saveFile method to save the message
                            myMessage.saveFile();

                            // Adds to the alertMessage variable
                            alertMessage = "SMS Saved!";

                            // Clears all variables and getters and setters
                            messageText = String.Empty;
                            subjectText = String.Empty;
                            alertMessage = String.Empty;
                            credentials = String.Empty;

                            messageType = 0;
                            charNum = 0;
                        }
                        break;
                    case 2:
                        // Checks if the subject box is empty
                        if (subjectText.Length == 0)
                        {
                            // Adds to the alertMessage variable
                            alertMessage += "The subject box is empty!\n";
                        }
                        // Checks if the message exceeds 1028 characters
                        else if (messageCharNum > 1028)
                        {
                            // Adds to the alertMessage variable
                            alertMessage += "Emails can only be 1028 characters long!\n";
                        }
                        // Checks if no message has been typed in
                        else if (messageCharNum == 0)
                        {
                            // Adds to the alertMessage variable
                            alertMessage += "Please enter a message!\n";
                        }
                        // Sends the Email or SIR
                        else 
                        {
                            // Checks if the subject box is a valid SIR
                            if (!checkIFSir(subjectText))
                            {
                                // Creates a new instance of Email and passes the messagetext
                                Email myMessage = new Email(messageText);

                                // Sets the emailAddress getter and setter to the text contained in txtMessageCredentials 
                                myMessage.emailAddress = myPage.txtMessageCredentials.Text;

                                // Sets the messageSubject getter and setter to the text contatined in txtMessageCredentials
                                myMessage.messageSubject = myPage.txtSubject.Text;

                                // Sets the sirMessage getter and setter to false
                                myMessage.sirMessage = false;

                                // Uses the saveFile method to save the message
                                myMessage.saveFile();

                                // Adds to the alertMessage variable
                                alertMessage = "Email Saved!";

                                // Clears all variables and getters and setters
                                messageText = String.Empty;
                                subjectText = String.Empty;
                                alertMessage = String.Empty;
                                credentials = String.Empty;

                                messageType = 0;
                                charNum = 0;
                            }
                            else if (checkIFSir(subjectText))
                            {
                                // Gets the first line of theMessageText
                                String theSortCodeText = theMessageText.Split('\r')[0];

                                // Uses Match to check if the first line is a valid sort code
                                Match match = sortCodePattern.Match(theSortCodeText);

                                // Checks if the sort code is valid
                                if (match.Success)
                                {
                                    // Gets the line where the incident is stored
                                    theMessageText = theMessageText.Substring(theMessageText.IndexOf('\n') + 1);

                                    // Stores incidentText
                                    String incidentText = theMessageText.Split('\r')[0];

                                    // Checks if the incidentText is stored in the theIncidents array list
                                    if (theIncidents.Contains(incidentText))
                                    {
                                        // Creates a new instance of Email and passes the theSortCode text messagetext
                                        Email myMessage = new Email(theSortCodeText + "\n" + messageText);

                                        // Sets the emailAddress getter and setter to the text contained in txtMessageCredentials 
                                        myMessage.emailAddress = myPage.txtMessageCredentials.Text;

                                        // Sets the messageSubject getter and setter to the text contatined in txtMessageCredentials
                                        myMessage.messageSubject = myPage.txtSubject.Text;
                                        
                                        // Sets the sirMessage getter and setter to false
                                        myMessage.sirMessage = true;

                                        // Uses the saveFile method to save the message
                                        myMessage.saveFile();

                                        // Adds to the alertMessage variable
                                        alertMessage = "SIR Saved!";

                                        messageText = String.Empty;
                                        subjectText = String.Empty;
                                        alertMessage = String.Empty;
                                        credentials = String.Empty;

                                        messageType = 0;
                                        charNum = 0;
                                    }
                                    else
                                    {
                                        // Adds to the alertMessage variable
                                        alertMessage += "SIR not recognised. Must be one contained in the list!";
                                    }
                                }
                                else
                                {
                                    // Adds to the alertMessage variable
                                    alertMessage += "Sort Code format not recognised. Must be in format of XX-XX-XX!";
                                }
                            }
                        }
                        break;
                    case 3:
                        // Checks if the twitter address exceeds 15 characters
                        if (myPage.txtMessageCredentials.Text.Length > 15)
                        {
                            // Adds to the alertMessage variable
                            alertMessage += "Twitter address' must not exceed 15 characters!\n";
                        }
                        // Checks if the message exceeds 140 characters
                        if (messageCharNum > 140)
                        {
                            // Adds to the alertMessage variable
                            alertMessage += "Tweets can only be 140 characters long!\n";
                        }
                        // Checks if no message has been typed in
                        else if (messageCharNum == 0)
                        {
                            // Adds to the alertMessage variable
                            alertMessage += "Please enter a message!\n";
                        }
                        // Sends the Tweet
                        else
                        {
                            // Creates a new instance of Tweets and passes messagetext
                            Tweets myMessage = new Tweets(messageText);

                            // Sets the twiiterAddress getter and setter to the text contained in txtMessageCredentials 
                            myMessage.twitterAddress = myPage.txtMessageCredentials.Text;

                            // Uses the saveFile method to save the message
                            myMessage.saveFile();

                            // Adds to the alertMessage variable
                            alertMessage = "Tweet Saved!";

                            // Clears all variables and getters and setters
                            messageText = String.Empty;
                            subjectText = String.Empty;
                            alertMessage = String.Empty;
                            credentials = String.Empty;

                            messageType = 0;
                            charNum = 0;
                        }
                        break;
                }
            }
            // Checks if the alert message has any text in it
            if (alertMessage.Length > 0)
            {
                // Display the messagebox with all relevant error messages
                messageBoxAlert(alertMessage);
            }
        }

        // This method clears all variables and clears all text boxes
        public void clearBoxes()
        {
            messageText = String.Empty;
            subjectText = String.Empty;
            alertMessage = String.Empty;
            credentials = String.Empty;

            messageType = 0;
            charNum = 0;

            myPage.txtMessageCredentials.Clear();
            myPage.txtSubject.Clear();
            myPage.txtMessageContainer.Clear();
        }

        // This method fills theIncidentArray with the contents of the Incidents text file
        public static void fillTheIncidentsArray()
        {
            // Initializes the array
            theIncidents = new ArrayList();

            // Stores theLine string
            string theLine = "";

            // Creates an instance of Stream Reader
            StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\Incidents.txt");
            {
                while (reader.Peek() != -1)
                {
                    theLine = reader.ReadLine();
                    theIncidents.Add(theLine);
                }
            }
        }

        // This method checks if the phone number is in a valid format
        private static bool IsPhoneNumber(string number)
        {
            // Uses try catach to return the result
            try
            {
                return Regex.Match(number, @"^[-+]?[0-9]*\.?[0-9]+$").Success;
            }
            catch
            {
                return false;
            }
        }

        // This method checks if the email address is in a valid format
        private bool IsValidEmail(string email)
        {
            // Uses try catach to return the result
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // This method checks if the twitter address is in a valid format
        private bool IsValidTwitterAddress(string twitterid)
        {
            // Uses try catach to return the result
            try
            {
                return Regex.Match(twitterid, "^(@)").Success;
            }
            catch
            {
                return false;
            }
        }

        // This method checks if the message is a valid message type
        private bool IsValidMessageType()
        {
            if (messageType == 1 || messageType == 2 || messageType == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // This method checks if the message subject is in valid SIR format
        public bool checkIFSir(string subject)
        {
            // Check if the subject is in valid SIR format
            Match match = subjectSIRPattern.Match(subject);
            if (match.Success)
            {
                return true;
            }
            return false;
        } 

        // Getter and setter to set and get the message character number
        public int messageCharNum
        {
            get { return charNum; }
            set { charNum = value; }
        }

        // Getter and setter to set and get the message subject
        public string theSubjectText
        {
            get { return subjectText; }
            set { subjectText = value; }
        }

        // Getter and setter to set and get the message text
        public string theMessageText
        {
            get { return messageText; }
            set { messageText = value; }
        }

        // Displays the alert message message boxes
        private void messageBoxAlert(string message)
        {
            MessageBox.Show(message);
            alertMessage = "";
        }
    }
}
