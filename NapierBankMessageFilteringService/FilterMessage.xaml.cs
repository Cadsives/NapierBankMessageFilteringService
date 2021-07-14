using MessageLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace NapierBankMessageFilteringService
{
    /// <summary>
    /// Interaction logic for FilterMessage.xaml
    /// </summary>
    public partial class FilterMessage : Page
    {
        public FilterMessage()
        {
            InitializeComponent();
        }

        // This event handler executes the loadAllMessages method in the loadFile class
        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            // Clears the message wall textbox
            txtMessageWall.Clear();
            // Creates an instance of LoadFile
            LoadFile loadFile = new LoadFile(this);
            // Loads all the json files
            loadFile.loadAllMessages();
        }

        // This event handler executes the loadNextMessage method in the loadFile class
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            // Clears the message wall textbox
            txtMessageWall.Clear();
            // Creates an instance of LoadFile
            LoadFile loadFile = new LoadFile(this);
            // Loads the next json file
            loadFile.loadNextMessage();
        }

        // This event handler executes the loadPrevMessage method in the loadFile class
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            // Clears the message wall textbox
            txtMessageWall.Clear();
            // Creates an instance of LoadFile
            LoadFile loadFile = new LoadFile(this);
            // Checks where the fileindex is
            if (loadFile.fileIndex == 0)
            {
                // Displays the relevant message box if the file index is at 0
                MessageBox.Show("This is the first message file");
            }
            else
            { 
                // Loads the previous message
                loadFile.loadPrevMessage();
            }
        }

        // This event handler clears the following textboxes
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Clears all textboxes and lists
            txtMessageWall.Clear();
            lstHashtag.Items.Clear();
            lstMentions.Items.Clear();
            lstSIR.Items.Clear();
            lstURL.Items.Clear();
        }
    }

    public class MyItem
    {
        // Variables used to set the data binding on the relevant list view boxes
        public string Hashtags { get; set; }
        public string Mentions { get; set; }
        public string Incidents { get; set; }
        public string Urls { get; set; }
    }
}
