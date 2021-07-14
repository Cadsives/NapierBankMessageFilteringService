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
    /// Interaction logic for MainInterface.xaml
    /// </summary>
    public partial class MainInterface : Page
    {
        // This Page is generally used as the default page when the program loads
        public MainInterface()
        {
            InitializeComponent();

            // Stores the path of the Logo
            string path = Environment.CurrentDirectory + @"\Images\NapierBankLogo.jpg";

            // Sets the Image box to be the logo stored in path
            image.Source = new BitmapImage(new Uri(path));
        }
    }
}
