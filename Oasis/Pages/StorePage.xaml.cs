using Newtonsoft.Json;
using Oasis.Class;
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

namespace Pages
{
    /// <summary>
    /// Interaction logic for StorePage.xaml
    /// </summary>
    public partial class StorePage : Page
    {
        public StorePage()
        {
            InitializeComponent();
            PullSkibidiShop();
        }

        private async Task PullSkibidiShop()
        {
            string apiUrl = "https://oasis-api.zippywippy.online/fortnite/api/storefront/v2/catalog";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(apiUrl);
                    var appContent = JsonConvert.DeserializeObject<AppConfig>(json);

                    var fortnitePage = appContent?.games?.FirstOrDefault(g => g.id == "fortnitepage");

                    if (fortnitePage != null)
                    {

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
    }
}
