using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Oasis.Pages
{
    /// <summary>
    /// Interaction logic for RetryOrClose.xaml
    /// </summary>
    public partial class RetryOrClose : Page
    {
        public RetryOrClose()
        {
            InitializeComponent();
        }

        private void RetryThen()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.LoadThatShitUp();
            }
            else
            {
                throw new InvalidOperationException("MainWindow is not available or of the expected type.");
            }
        }

        private void Retry_Click(object sender, RoutedEventArgs e)
        {
            RetryThen();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void E7IsBent_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://discord.com/channels/1335246747928367174/1335246800885383269") { UseShellExecute = true });
        }
    }
}
