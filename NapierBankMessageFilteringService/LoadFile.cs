using MessageLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Windows;

namespace NapierBankMessageFilteringService
{
    public class LoadFile
    {
        // Creates a link for Filter Message
        private FilterMessage myPage;

        // Stores the path to the Json File Folder 
        private string path = Environment.CurrentDirectory + @"\Json File Folder\";

        // Stores the file index
        private static int index = 0;

        // Constructor passing linking Filtermessage
        public LoadFile(FilterMessage myPage)
        {
            // Makes the mypage variable equal the variable being passed in the constructor
            this.myPage = myPage;
        }

        // This method loads all the json messages found in the Json File Folder
        public void loadAllMessages()
        {
            // Creates a new instance of TheMessage Class
            TheMessage loadedMessage = new TheMessage();

            // Enumerates the json files and stores them in the files variable
            var files = Directory.EnumerateFiles(path, "*.json");

            // Loops through all the files 
            foreach (string currentFile in files)
            {
                // Creates a new instance of StreamReader and passed the current file to be read
                StreamReader sr = new StreamReader(currentFile);

                // Stores the json string
                var jsonString = sr.ReadToEnd();

                // Creates a new instance of the JavaScriptSerializer
                JavaScriptSerializer ser = new JavaScriptSerializer();

                // Makes TheMessage object equal the deserialized file
                loadedMessage = ser.Deserialize<TheMessage>(jsonString);

                // Sets the value of txtMessageWall to be the result of returnMessage
                this.myPage.txtMessageWall.Text += loadedMessage.returnMessage();

                // If any hashtags, mentionsm urls or SIR's are found. 
                // Then it adds them to relventant lists
                foreach (var s in loadedMessage.theHashtags)
                {
                    this.myPage.lstHashtag.Items.Add(new MyItem { Hashtags = s });
                }

                foreach (var s in loadedMessage.theMentions)
                {
                    this.myPage.lstMentions.Items.Add(new MyItem { Mentions = s });
                }

                foreach (var s in loadedMessage.theURLS)
                {
                    this.myPage.lstURL.Items.Add(new MyItem { Urls = s });
                }

                foreach (var s in loadedMessage.theSIR)
                {
                    this.myPage.lstSIR.Items.Add(new MyItem { Incidents = s });
                }
            }
        }

        public void loadNextMessage()
        {
            // Creates a new instance of TheMessage Class
            TheMessage loadedMessage = new TheMessage();

            // Gets all files in the directory
            var files = Directory.GetFiles(path, "*.json");

            // Uses try catch to see if the file exists
            try
            {
                // Sets the result of files as the file index
                var theFile = files[fileIndex];

                // Creates a new instance of StreamReader and passed the theFile to be read
                StreamReader sr = new StreamReader(theFile);

                // Stores the json string
                var jsonString = sr.ReadToEnd();

                // Creates a new instance of the JavaScriptSerializer
                JavaScriptSerializer ser = new JavaScriptSerializer();

                // Makes TheMessage object equal the deserialized file
                loadedMessage = ser.Deserialize<TheMessage>(jsonString);

                // Sets the value of txtMessageWall to be the result of returnMessage
                this.myPage.txtMessageWall.Text += loadedMessage.returnMessage();

                // If any hashtags, mentionsm urls or SIR's are found. 
                // Then it adds them to relventant lists
                foreach (var s in loadedMessage.theHashtags)
                {
                    this.myPage.lstHashtag.Items.Add(new MyItem { Hashtags = s });
                }

                foreach (var s in loadedMessage.theMentions)
                {
                    this.myPage.lstMentions.Items.Add(new MyItem { Mentions = s });
                }

                foreach (var s in loadedMessage.theURLS)
                {
                    this.myPage.lstURL.Items.Add(new MyItem { Urls = s });
                }

                // Increments the fileindex by 1
                fileIndex++;
            }
            catch(IndexOutOfRangeException)
            {
                // Shows message box if there're no more files to load
                MessageBox.Show("There're no more files to load!");
            }
        }

        public void loadPrevMessage()
        {
            // Creates a new instance of TheMessage Class
            TheMessage loadedMessage = new TheMessage();

            // Gets all files in the directory
            var files = Directory.GetFiles(path, "*.json");

            // Decrements the fileindex by 1
            fileIndex--;

            // Uses try catch to see if the file exists
            try
            {
                // Sets the result of files as the file index
                var theFile = files[fileIndex];

                // Creates a new instance of StreamReader and passed the theFile to be read
                StreamReader sr = new StreamReader(theFile);

                // Stores the json string
                var jsonString = sr.ReadToEnd();

                // Creates a new instance of the JavaScriptSerializer
                JavaScriptSerializer ser = new JavaScriptSerializer();

                // Makes TheMessage object equal the deserialized file
                loadedMessage = ser.Deserialize<TheMessage>(jsonString);

                // Sets the value of txtMessageWall to be the result of returnMessage
                this.myPage.txtMessageWall.Text += loadedMessage.returnMessage();
            }
            catch (IndexOutOfRangeException)
            {
                // Shows message box if there're no more files to load
                MessageBox.Show("There're no more files to load!");
            }
        }

        // Getter and Setter used to get and set the file index
        public int fileIndex
        {
            get { return index; }
            set { index = value; }
        }
    }
}
