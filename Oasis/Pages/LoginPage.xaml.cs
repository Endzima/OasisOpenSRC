using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Web.WebView2.Core;

namespace Oasis.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private string _randomString;

        public string RandomString
        {
            get { return _randomString; }
            set
            {
                _randomString = value;
            }
        }

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void InitializeWebView2()
        {
            webView.IsHitTestVisible = true;
            webView.Visibility = Visibility.Visible;

            await webView.EnsureCoreWebView2Async();

            if (webView.CoreWebView2 != null)
            {
                webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
                webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
            }

            string url = $"https://discord.com/oauth2/authorize?client_id=1346455919269122080&response_type=code&redirect_uri=http%3A%2F%2F74.112.77.238%3A3551%2Fstella%2Foauth%2Fcallback&scope=identify+email+guilds";
            webView.Source = new Uri(url);
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            InitializeWebView2();
        }
    }
}
