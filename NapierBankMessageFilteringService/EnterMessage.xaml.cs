using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageLibrary;

namespace NapierBankMessageFilteringService
{
    /// <summary>
    /// Interaction logic for EnterMessage.xaml
    /// </summary>
    public partial class EnterMessage : Page
    { 
        public EnterMessage()
        {
            InitializeComponent();

            // Sets the email related elements to hidden until a valid email address is typed into the txtMessageCredentials
            lblSubject.Visibility = Visibility.Hidden;
            txtSubject.Visibility = Visibility.Hidden;
        }

        // This event handler executes the sendMessage method in the Validate class
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            // Creates an instances of Validate class
            Validate validate = new Validate(this);
            // Uses method sendMessage to save messages
            validate.sendMessage();
        }

        // This event handler executes the clearBoxes method in the Validate class
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Creates an instances of Validate class
            Validate validate = new Validate(this);
            // Uses method clearBoxes to clear all text boxes, lists and variables
            validate.clearBoxes();
        }

        // This event handler executes the validateMessageCredentials method in the Validate class
        private void txtMessageCredentials_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Creates an instances of Validate class
            Validate validate = new Validate(this);
            // Uses method validateMessageCredentials to validate what type of message has been typed into the textbox
            validate.validateMessageCredentials();
        }

        // This event handler executes the theSubjectText getter and setter in the Validate class
        private void txtSubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Creates an instances of Validate class
            Validate validate = new Validate(this);
            // Sets the theSubjectText getter and setter to be the text typed into the textbox
            validate.theSubjectText = txtSubject.Text;
        }

        // This event handler executes the theMessageText and the messageCharNum getter and setter in the Validate class
        private void txtMessageContainer_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Creates an instances of Validate class
            Validate validate = new Validate(this);
            // Uses sets the theMessageText getter and setter to be the text typed into the textbox
            validate.theMessageText = txtMessageContainer.Text;
            // Sets the messageCharNum getter and setter to be the length of the text typed into the textbox
            validate.messageCharNum = txtMessageContainer.Text.Length;
        }
    }
}
