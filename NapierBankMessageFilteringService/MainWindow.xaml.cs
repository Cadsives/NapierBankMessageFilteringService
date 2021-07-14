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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Invokes the create directory method
            create_directory();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Sets the pageFrame as the Main Interface
            MainInterface myInterface = new MainInterface();

            pageFrame.Content = myInterface;
        }

        private void btnCreateMessage_Click(object sender, RoutedEventArgs e)
        {
            // Fills the pageFrame with the Create Message Page
            CreateMessage myMessage = new CreateMessage();

            pageFrame.Content = myMessage;
        }

        private void btnFilterMessage_Click(object sender, RoutedEventArgs e)
        {
            // Fills the pageFrame with Filter Message Page
            FilterMessage fMessage = new FilterMessage();

            pageFrame.Content = fMessage;
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            // Displays a message box that show the instructions on how to use this application
            MessageBox.Show("Instructions:\n1: Click Create Message to create a message.\n2: Click Filter Message to filter messages", "Help");
        }

        private void create_directory()
        {
            // Creates the Json File Folder if it doesn't exist
            if (System.IO.Directory.Exists(Environment.CurrentDirectory + @"\Json File Folder"))
            {
                return;
            }
            else
            {
                System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(@"Json File Folder");
            }
        }
    }
}
