using System;
using System.Collections.Generic;
using System.Net.Http;
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
    /// Interaction logic for CheckUpdates.xaml
    /// </summary>
    public partial class CheckUpdates : Page
    {
        string url = "http://74.112.77.238:3551/stella/status/backend/launcher";

        public CheckUpdates()
        {
            InitializeComponent();
            LoadCunt();
        }

        private async void LoadCunt()
        {
            await Task.Delay(3000);
            await CheckEndpointStatusAsync();
            //OpenMain();
        }

        private async void OpenMain()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.LoadLogin();
            }
            else
            {
                throw new InvalidOperationException("MainWindow is not available or of the expected type.");
            }
            await Task.Delay(1300);
        }

        private async void OpenFail()
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.LoadFail();
            }
            else
            {
                throw new InvalidOperationException("MainWindow is not available or of the expected type.");
            }
            await Task.Delay(1300);
        }

        private async Task CheckEndpointStatusAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(3.5);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        StatusText.Text = "You're up to date!";
                        await Task.Delay(500);
                        OpenMain();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        StatusText.Text = "Failed.";
                        await Task.Delay(1000);
                        StatusText.Text = "Retrying...";
                        TryAgain();
                        await Task.Delay(1000);
                        StatusText.Text = "Failed.";
                        await Task.Delay(500);
                        OpenFail();
                    }
                    else
                    {
                        StatusText.Text = "Failed to connect to Oasis services, please try again later.";
                        await Task.Delay(1000);
                        OpenFail();
                    }
                }
                catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
                {
                    OpenFail();
                }
                catch (HttpRequestException)
                {
                    await Task.Delay(1000);
                    OpenFail();
                }
                catch (Exception)
                {
                    OpenFail();
                }
            }
        }

        private async void TryAgain()
        {
           await CheckEndpointStatusAsync();
        }
    }
}
