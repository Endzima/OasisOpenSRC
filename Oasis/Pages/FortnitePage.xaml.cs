using Oasis.Class;
using Oasis.Class.LaunchLogic;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Security;
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

namespace Pages
{
    /// <summary>
    /// Interaction logic for FortnitePage.xaml
    /// </summary>
    public partial class FortnitePage : Page
    {
        private string fortnitePath = string.Empty;


        public FortnitePage()
        {
            InitializeComponent();
            this.Loaded += FNPage;
        }

        private async void FNPage(object sender, RoutedEventArgs e)
        {
            await LoadFNBackground();
            DoesCuhHaveFort();
        }

        //---------------------------------- Game logic below

        // First time writing launch logic, actually went pretty fucking well!

        private async void LaunchGame(string fortnitePath)
        {
            string username = StringSharing.Username;
            string password = StringSharing.AuthKey;

            try
            {
                WebClient RedirectDownload = new WebClient();
                RedirectDownload.DownloadFile("http://74.112.77.238:3551/launcher/skibidi/redirect", System.IO.Path.Combine(fortnitePath, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "Redirect.dll"));

                await Task.Delay(2000);

                await Task.Run(() =>
                {
                    StartGame.Launch(fortnitePath, "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -fromfl=eac -nobe -fltoken=3db3ba5dcbd2e16703f3978d -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQwN2IyZjQwZWJhYWQ4NTlhZDQiLCJnZW5lcmF0ZWQiOjE2Mzg3MTcyNzgsImNhbGRlcmFHdWlkIjoiMzgxMGI4NjMtMmE2NS00NDU3LTliNTgtNGRhYjNiNDgyYTg2IiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.VAWQB67RTxhiWOxx7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ", username, password);
                    FakeACTemp.Start(fortnitePath, "FortniteClient-Win64-Shipping_EAC.exe", "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -fromfl=eac -nobe -fltoken=3db3ba5dcbd2e16703f3978d -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQwN2IyZjQwZWJhYWQ4NTlhZDQiLCJnZW5lcmF0ZWQiOjE2Mzg3MTcyNzgsImNhbGRlcmFHdWlkIjoiMzgxMGI4NjMtMmE2NS00NDU3LTliNTgtNGRhYjNiNDgyYTg2IiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.VAWQB67RTxhiWOxx7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ", "r");
                    FakeACTemp.Start(fortnitePath, "FortniteLauncher.exe", "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -fromfl=eac -nobe -fltoken=3db3ba5dcbd2e16703f3978d -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQwN2IyZjQwZWJhYWQ4NTlhZDQiLCJnZW5lcmF0ZWQiOjE2Mzg3MTcyNzgsImNhbGRlcmFHdWlkIjoiMzgxMGI4NjMtMmE2NS00NDU3LTliNTgtNGRhYjNiNDgyYTg2IiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.VAWQB67RTxhiWOxx7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ", "dsf");
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occured when trying to launch v15.50. {ex.Message}");
            }
        }

        // locate the gamepath from registry, it's aight code
        private void DoesCuhHaveFort()
        {
            try
            {
                string registryKey = @"oasis";
                string gamePathKey = "GamePath";

                using (var key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(registryKey))
                {
                    if (key != null)
                    {
                        object gamePath = key.GetValue(gamePathKey);

                        if (gamePath != null)
                        {
                            fortnitePath = gamePath.ToString();

                            ImportButton.Visibility = Visibility.Collapsed;
                            ImportButton.IsHitTestVisible = false;
                        }
                        else
                        {
                            ImportButton.IsHitTestVisible = true;
                            ImportButton.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        ImportButton.IsHitTestVisible = true;
                        ImportButton.Visibility = Visibility.Visible;
                    }
                }

                Console.WriteLine($"Fortnite Path: {fortnitePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading registry: {ex.Message}");
            }
        }
        // saves the path you imported
        private void SaveGamePathToRegistry(string gamePath)
        {
            try
            {
                string registryKey = @"oasis";
                string gamePathKey = "GamePath";

                using (var key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(registryKey))
                {
                    if (key != null)
                    {
                        key.SetValue(gamePathKey, gamePath);
                        MessageBox.Show($"Game path saved to registry: {gamePath}");
                    }
                    else
                    {
                        MessageBox.Show("Error: Unable to create or open registry key.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving game path to registry: {ex.Message}");
            }
        }

        //-------------------------------------------------- Button logic below

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            using (var folderDialog = new CommonOpenFileDialog())
            {
                folderDialog.IsFolderPicker = true; 
                folderDialog.Title = "Select Fortnite Game Folder"; 

                if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string selectedPath = folderDialog.FileName;

                    string fortniteGamePath = System.IO.Path.Combine(selectedPath, "FortniteGame");
                    string enginePath = System.IO.Path.Combine(selectedPath, "Engine");

                    if (Directory.Exists(fortniteGamePath) && Directory.Exists(enginePath))
                    {
                        SaveGamePathToRegistry(selectedPath);
                        MessageBox.Show($"Game path saved to registry: {selectedPath}");
                        ImportButton.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("The selected folder does not contain the necessary 'FortniteGame' and 'Engine' directories. Please choose the correct folder.",
                                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fortnitePath))
            {
                MessageBox.Show("Fortnite path is not set! Please check your installation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            LaunchGame(fortnitePath);
        }

        private void YouTube_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.youtube.com/@OasisOGFN") { UseShellExecute = true });
        }

        //----------------------------- Contentpages

        private async Task LoadFNBackground()
        {
            string apiUrl = "http://74.112.77.238:3551/launcher/api/content/pages";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(apiUrl);
                    var appContent = JsonConvert.DeserializeObject<AppConfig>(json);

                    var fortnitePage = appContent?.games?.FirstOrDefault(g => g.id == "fortnitepage");

                    if (fortnitePage != null)
                    {
                        var images = fortnitePage.images;

                        var backgroundImage = images.FirstOrDefault(img => img.id == "backgroundImageFNP");
                        var secondImage = images.FirstOrDefault(img => img.id == "newsImageFNP1");
                        var thirdImage = images.FirstOrDefault(img => img.id == "newsImageFNP2");

                        if (backgroundImage != null) LoadImage(backgroundImage.image, FortniteBG);
                        if (secondImage != null) LoadImage(secondImage.image, FortniteNews1);
                        if (thirdImage != null) LoadImage(thirdImage.image, FortniteNews2);

                        if (secondImage != null && !string.IsNullOrEmpty(secondImage.header))
                            HeaderText1.Text = secondImage.header;

                        if (thirdImage != null && !string.IsNullOrEmpty(thirdImage.header))
                            HeaderText2.Text = thirdImage.header;

                        if (secondImage != null && !string.IsNullOrEmpty(secondImage.description))
                            DescText1.Text = secondImage.description;

                        if (thirdImage != null && !string.IsNullOrEmpty(thirdImage.description))
                            DescText2.Text = thirdImage.description;
                    }
                    else
                    {
                        Console.WriteLine("fortnitepage not found in JSON.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load content: {ex.Message}");
                }
            }
        }


        private void LoadImage(string imageUrl, Image imageControl)
        {
            try
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(imageUrl, UriKind.Absolute);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                imageControl.Dispatcher.Invoke(() =>
                {
                    imageControl.Source = bitmapImage;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading image: " + ex.Message);
            }
        }

    }
}
