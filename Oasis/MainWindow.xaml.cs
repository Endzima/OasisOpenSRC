using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Oasis.Class;

namespace Oasis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string DiscordId { get; private set; }
        public string Username { get; private set; }
        public string AuthKey { get; private set; }
        public string Role { get; private set; }
        public string Hash { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            RegisterCustomUriScheme();
            //LoadThatShitUp();
        }



        public static void RegisterCustomUriScheme()
        {
            try
            {
                // Get the application's EXE path (replace Oasis.dll with Oasis.exe)
                string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string exePath = appPath.Replace("Stella.dll", "Stella.exe");
                string command = $"\"{exePath}\" \"%1\"";  // Command to be executed for the custom URI scheme

                Console.WriteLine($"App Path (Fixed): {exePath}");

                // Check if the 'oasis' key exists in HKEY_CLASSES_ROOT and create it if not
                using (RegistryKey oasisKey = Registry.ClassesRoot.CreateSubKey(@"stella"))
                {
                    if (oasisKey != null)
                    {
                        // Set the default value of the 'oasis' key
                        oasisKey.SetValue("", "URL:stella Protocol");
                        oasisKey.SetValue("URL Protocol", "");  // Set URL Protocol for the custom scheme

                        // Create the 'shell\open\command' subkey if it doesn't exist
                        using (RegistryKey commandKey = oasisKey.CreateSubKey(@"shell\open\command"))
                        {
                            if (commandKey != null)
                            {
                                // Set the command to execute when the custom URI scheme is called
                                commandKey.SetValue("", command);
                                Console.WriteLine("Command for oasis protocol set successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Error: Unable to create command subkey.");
                            }
                        }

                        // Success message
                       // Console.WriteLine("Custom URI scheme registered successfully.");
                       // MessageBox.Show("Custom URI scheme registered for oasis://", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                       // Console.WriteLine("Error: Unable to create oasis key.");
                        //MessageBox.Show("Error: Unable to create oasis key.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log and show error message if something goes wrong
                //Console.WriteLine($"Error: {ex.Message}");
                //MessageBox.Show($"Error registering URI scheme: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ProcessUriScheme(string uri)
        {
            try
            {
                Logger.Log($"Processing Callback, {uri}");

                string cleanedUri = uri.Replace(" ", "").Replace("stella:", "").Trim('/');

                var parts = cleanedUri.Split('/');
                Logger.Log($"Parts: {string.Join(", ", parts)}");

                if (parts.Length != 5)
                {
                    Logger.Log("Callback doesn't contain the 5 required fields.");
                    return;
                }

                DiscordId = parts[0].Trim();
                Username = parts[1].Trim();
                AuthKey = parts[2].Trim();
                Hash = parts[2].Trim();

                StringSharing.DiscordId = DiscordId;
                StringSharing.Username = Username;
                StringSharing.AuthKey = AuthKey;
                StringSharing.Hash = Hash;

                Logger.Log($"Received Callback in MainWindow.");
                SniffHimOut();
            }
            catch (Exception ex)
            {
                Logger.Log($"Error processing callback. {ex.Message}");
            }
        }

        private async void SniffHimOut()
        {
            if (StringSharing.Username == "zipperonfmod" || StringSharing.DiscordId == "644764459453382686")
            {
                // got you now you sneaky little nob
                PageNavigation.Navigate(new Oasis.Pages.LoadToKeepBroWaiting());
                await Task.Delay(2000);
                FlashAnimZips();
            }
            else
            {
                NavMain();
            }
        }

        private async void FlashAnim()
        {
            IAmThrowingAFlashbang2();
            IAmThrowingAFlashbang();
            await Task.Delay(1500);
            FlashBang.Opacity = 1;
            await Task.Delay(2000);
            DoubleAnimation fadeInDescTxt = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1.2))
            };
            FlashBang.BeginAnimation(UIElement.OpacityProperty, fadeInDescTxt);

            await Task.Delay(1000);
        }

        private async void FlashAnimZips()
        {
            IAmThrowingAFlashbang2Zips();
            IAmThrowingAFlashbangZips();
            await Task.Delay(1500);
            FlashBang.Opacity = 1;
            await Task.Delay(2000);
            DoubleAnimation fadeInDescTxt = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1.2))
            };
            FlashBang.BeginAnimation(UIElement.OpacityProperty, fadeInDescTxt);

            await Task.Delay(1000);
        }

        private void IAmThrowingAFlashbang()
        {
            var audioUri = new Uri("pack://application:,,,/src/Sounds/flashbang.mp3", UriKind.RelativeOrAbsolute);
            var resourceStream = Application.GetResourceStream(audioUri)?.Stream;

            if (resourceStream != null)
            {
                string tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "flashbang.mp3");

                using (var fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }

                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Open(new Uri(tempFile));
                mediaPlayer.Play();
            }
            else
            {
                MessageBox.Show("Failed to load audio resource.");
            }
        }

        private async void IAmThrowingAFlashbang2()
        {
            var audioUri = new Uri("pack://application:,,,/src/Sounds/videoplayback.mp3", UriKind.RelativeOrAbsolute);
            var resourceStream = Application.GetResourceStream(audioUri)?.Stream;

            if (resourceStream != null)
            {
                string tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "videoplayback.mp3");

                using (var fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }

                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Volume = 1;
                mediaPlayer.Open(new Uri(tempFile));
                mediaPlayer.Play();
                FlashBang.IsHitTestVisible = false;
                await Task.Delay(3000);
                NavigateMain();
            }
            else
            {
                MessageBox.Show("Failed to load audio resource.");
            }
        }

        private void IAmThrowingAFlashbangZips()
        {
            var audioUri = new Uri("pack://application:,,,/src/Sounds/flashbang.mp3", UriKind.RelativeOrAbsolute);
            var resourceStream = Application.GetResourceStream(audioUri)?.Stream;

            if (resourceStream != null)
            {
                string tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "flashbang.mp3");

                using (var fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }

                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Open(new Uri(tempFile));
                mediaPlayer.Play();
            }
            else
            {
                MessageBox.Show("Failed to load audio resource.");
            }
        }

        private async void IAmThrowingAFlashbang2Zips()
        {
            var audioUri = new Uri("pack://application:,,,/src/Sounds/videoplayback.mp3", UriKind.RelativeOrAbsolute);
            var resourceStream = Application.GetResourceStream(audioUri)?.Stream;

            if (resourceStream != null)
            {
                string tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "videoplayback.mp3");

                using (var fileStream = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }

                MediaPlayer mediaPlayer = new MediaPlayer();
                mediaPlayer.Volume = 1;
                mediaPlayer.Open(new Uri(tempFile));
                mediaPlayer.Play();
                //FlashBang.IsHitTestVisible = false;
                await Task.Delay(3000);
                //NavigateMain();
            }
            else
            {
                MessageBox.Show("Failed to load audio resource.");
            }

            var violinUri = new Uri("pack://application:,,,/src/Sounds/violin.mp3", UriKind.RelativeOrAbsolute);
            var resStream = Application.GetResourceStream(violinUri)?.Stream;

            if (resStream != null)
            {
                string vioFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "violin.mp3");

                using (var fileStream = new FileStream(vioFile, FileMode.Create, FileAccess.Write))
                {
                    resStream.CopyTo(fileStream);
                }

                MediaPlayer violinPlayer = new MediaPlayer();
                violinPlayer.Volume = 1;
                violinPlayer.Open(new Uri(vioFile));
                violinPlayer.Play();

                DoubleAnimation fadeInSevander = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = new Duration(TimeSpan.FromSeconds(1.5))
                };
                Sevander.BeginAnimation(UIElement.OpacityProperty, fadeInSevander);

                await Task.Delay(1000);

                DoubleAnimation fadeOutSevander = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = new Duration(TimeSpan.FromSeconds(1.2))
                };
                Sevander.BeginAnimation(UIElement.OpacityProperty, fadeOutSevander);

                //FlashBang.IsHitTestVisible = false;
                await Task.Delay(3000);
                NavigateMain();
            }
            else
            {
                MessageBox.Show("Failed to load audio resource.");
            }
        }

        //old ProcessUriScheme. uses CallbackDecrypt.cs and CallbackPayload.cs but it was being gay and wouldn't decrypt so I'm not bothered and I'll just revert to the first one.
        public void OldProcessUriScheme(string uri)
        {
            try
            {
                if (!uri.StartsWith("oasis://"))
                {
                    throw new Exception("Invalid URI format.");
                }

                string encryptedData = uri.Substring(8);
                MessageBox.Show($"Encrypted Data (before filter): {encryptedData}");

                encryptedData = FilterValidCharacters(encryptedData);

                if (string.IsNullOrEmpty(encryptedData))
                {
                    throw new Exception("Encrypted data contains no valid characters.");
                }

                MessageBox.Show($"Encrypted Data (after filter): {encryptedData}");

                byte[] encryptedBytes;
                if (IsBase64String(encryptedData))
                {
                    encryptedBytes = Convert.FromBase64String(encryptedData);
                }
                else
                {
                    encryptedBytes = Convert.FromHexString(encryptedData);
                }

                string decryptedData = CallbackDecryption.DecryptData(encryptedBytes);

                if (string.IsNullOrEmpty(decryptedData))
                {
                    throw new Exception("Decrypted data is empty or null.");
                }

                MessageBox.Show($"Decrypted Data: {decryptedData}");

                var parameters = decryptedData.Split('&');

                if (parameters.Length < 4)
                {
                    throw new Exception("Decrypted data does not contain enough parameters.");
                }

                DiscordId = parameters[0];
                Username = parameters[1];
                AuthKey = parameters[2];
                Role = parameters[3];

                MessageBox.Show($"{DiscordId} {Username} {Role} {AuthKey}", "Cheeky", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Ruh Roh", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private string FilterValidCharacters(string input)
        {
            var filteredString = new string(input.Where(c => (char.IsLetterOrDigit(c))).ToArray());
            return filteredString;
        }

        private bool IsBase64String(string input)
        {
            return (input.Length % 4 == 0) && input.All(c => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=".Contains(c));
        }


        public async void LoadThatShitUp()
        {
            await Task.Delay(500);
            PageNavigation.Navigate(new Pages.CheckUpdates());
        }

        public void LoadLogin()
        {
            PageNavigation.Navigate(new Pages.LoginPage());
        }

        public void LoadFail()
        {
            PageNavigation.Navigate(new Pages.RetryOrClose());
        }

        public void NavMain()
        {
            PageNavigation.Navigate(new Pages.LoadingScreen());
        }

        public void NavigateMain()
        {
            Task.Delay(2000);
            PageNavigation.Navigate(new Pages.LoadingScreen());
        }

        public void NavMain2()
        {
            BackgroundLogin.Visibility = Visibility.Collapsed;
            PageNavigation.Navigate(new Pages.MainPage());
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DragMoveDock(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }
    }
}