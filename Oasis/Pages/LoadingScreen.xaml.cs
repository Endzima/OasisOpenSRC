using System;
using System.Collections.Generic;
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

namespace Oasis.Pages
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Page
    {
        public LoadingScreen()
        {
            InitializeComponent();
            LoadCunt();
        }

        private async void LoadCunt()
        {
            await Task.Delay(2000);
            OpenMain();
        }

        private async void OpenMain()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.NavMain2();
            }
            else
            {
                throw new InvalidOperationException("MainWindow is not available or of the expected type.");
            }
            await Task.Delay(1300);
        }
    }
}
