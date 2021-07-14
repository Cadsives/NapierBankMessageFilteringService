using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NapierBankMessageFilteringService
{
    /// <summary>
    /// Interaction logic for CreateMessage.xaml
    /// </summary>
    public partial class CreateMessage : Page
    {
        public CreateMessage()
        {
            InitializeComponent();

            // Populates the messageFrame with the EnterMessage Page
            EnterMessage myMessage = new EnterMessage();

            messageFrame.Content = myMessage;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // Closes the application
            System.Environment.Exit(1);
        }
    }
}
