using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
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
using Oasis.Class;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
using Oasis.Pages;

namespace Pages
{

    public partial class Home : Page
    {
        private Oasis.Class.AppConfig AppContent { get; set; }

        public Home()
        {
            InitializeComponent();
            this.Loaded += Home_Loaded;
        }

        private async void Home_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadAppContent();

            var backgroundImages = AppContent?.games
                ?.FirstOrDefault(g => g.id == "fortnite")?.images;

            if (backgroundImages != null && backgroundImages.Count >= 3)
            {
                LoadImage(backgroundImages[0].image, NewsImageFN1);
                LoadImage(backgroundImages[1].image, NewsImageFN2);
                LoadImage(backgroundImages[2].image, NewsImageFN3);
            }
        }

        private void LoadImage(string imageUrl, ImageBrush imageBrush)
        {
            try
            {
                var bitmapImage = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                imageBrush.ImageSource = bitmapImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading image: " + ex.Message);
            }
        }

        private async Task LoadAppContent()
        {
            string apiUrl = "http://74.112.77.238:3551/launcher/api/content/pages";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(apiUrl);
                    var appContent = JsonConvert.DeserializeObject<Oasis.Class.AppConfig>(json);

                    var fortniteGame = appContent.games.FirstOrDefault(g => g.id == "fortnite");
                    if (fortniteGame != null)
                    {
                        var newsImages = fortniteGame.images
                            .Where(img => img.id.StartsWith("newsImageFN"))
                            .ToList();

                        if (newsImages.Count > 0) DescText.Text = newsImages[0].description;
                        if (newsImages.Count > 1) DescText2.Text = newsImages[1].description;
                        if (newsImages.Count > 2) DescText3.Text = newsImages[2].description;

                        if (newsImages.Count > 0) MainText.Text = newsImages[0].header;
                        if (newsImages.Count > 1) MainText2.Text = newsImages[1].header;
                        if (newsImages.Count > 2) MainText3.Text = newsImages[2].header;

                        if (newsImages.Count > 0) NewsImageFN1.ImageSource = new BitmapImage(new Uri(newsImages[0].image, UriKind.Absolute));
                        if (newsImages.Count > 1) NewsImageFN2.ImageSource = new BitmapImage(new Uri(newsImages[1].image, UriKind.Absolute));
                        if (newsImages.Count > 2) NewsImageFN3.ImageSource = new BitmapImage(new Uri(newsImages[2].image, UriKind.Absolute));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load content: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
