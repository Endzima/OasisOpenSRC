using Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
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

namespace Oasis.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Home();
            CheckStatus();

            string discordId = StringSharing.DiscordId;
            string username = StringSharing.Username;
            string authKey = StringSharing.AuthKey;
            string role = StringSharing.Role;
            string Avatar = StringSharing.Hash;

            //MessageBox.Show($"DiscordId: {discordId}, Username: {username}, Role: {role}, AuthKey: {authKey}");

            ShowUserInfoSkibidiToilette(username, discordId, Avatar);
        }

        private void GrabPFP(string discordId, string Avatar)
        {
            string avatarUrl = $"https://cdn.discordapp.com/avatars/{discordId}/{Avatar}";
            AvatarImageBrush.ImageSource = new BitmapImage(new Uri(avatarUrl));
        }

        private void ShowUserInfoSkibidiToilette(string username, string discordId, string Avatar)
        {
            GrabPFP(discordId, Avatar);
            if (!string.IsNullOrEmpty(username) && username.Length > 14)
            {
                username = username.Substring(0, 14) + "...";
            }
            UsernameText.Text = $"{username}";
        }

        public void SetUserBadge(string role)
        {
            var (roleText, imagePath, colorHex) = RoleManager.GetBadgeImagePath(role);

            if (!string.IsNullOrEmpty(roleText))
            {
                UserBadgeImage.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                RoleText.Text = roleText;
                RoleText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex));
            }
            else
            {
                //RoleText.Text = "Unknown Role";
            }
        }

        private void HomeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimateHomeButton(true);
        }

        private void HomeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimateHomeButton(false);
        }

        private void FNBButton_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimateFNButton(true);
        }

        private void FNBButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimateFNButton(false);
        }

        private void StoreButton_MouseEnter(object sender, MouseEventArgs e)
        {
            AnimateStoreButton(true);
        }

        private void StoreButton_MouseLeave(object sender, MouseEventArgs e)
        {
            AnimateStoreButton(false);
        }

        private void AnimateHomeButton(bool isHomeHovering)
        {
            double textMoveTo = isHomeHovering ? 15 : 0;
            double iconMoveFrom = isHomeHovering ? -15 : 0; 
            double iconMoveTo = isHomeHovering ? 0 : -15; 
            double iconOpacityTo = isHomeHovering ? 1 : 0;  

            DoubleAnimation textMoveAnimation = new DoubleAnimation
            {
                To = textMoveTo,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut }
            };
            HomeTextTransform.BeginAnimation(TranslateTransform.XProperty, textMoveAnimation);

            DoubleAnimation iconMoveAnimation = new DoubleAnimation
            {
                From = iconMoveFrom,
                To = iconMoveTo,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut }
            };
            HomeIconTransform.BeginAnimation(TranslateTransform.XProperty, iconMoveAnimation);

            DoubleAnimation iconOpacityAnimation = new DoubleAnimation
            {
                To = iconOpacityTo,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            HomeButtonIcon.BeginAnimation(UIElement.OpacityProperty, iconOpacityAnimation);
        }

        private void AnimateFNButton(bool isFNHovering)
        {
            double fnTextMoveTo = isFNHovering ? 15 : 0;
            double fnIconMoveFrom = isFNHovering ? -15 : 0;
            double fnIconMoveTo = isFNHovering ? 0 : -15;
            double fnIconOpacityTo = isFNHovering ? 1 : 0;

            DoubleAnimation fnTextMoveAnimation = new DoubleAnimation
            {
                To = fnTextMoveTo,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut }
            };
            FNPTextTransform.BeginAnimation(TranslateTransform.XProperty, fnTextMoveAnimation);

            DoubleAnimation fnIconMoveAnimation = new DoubleAnimation
            {
                From = fnIconMoveFrom,
                To = fnIconMoveTo,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut }
            };
            FNPIconTransform.BeginAnimation(TranslateTransform.XProperty, fnIconMoveAnimation);

            DoubleAnimation fnIconOpacityAnimation = new DoubleAnimation
            {
                To = fnIconOpacityTo,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            FNPButtonIcon.BeginAnimation(UIElement.OpacityProperty, fnIconOpacityAnimation);
        }

        private void AnimateStoreButton(bool isStoreHovering)
        {
            double storeTextMoveTo = isStoreHovering ? 15 : 0;
            double storeIconMoveFrom = isStoreHovering ? -15 : 0;
            double storeIconMoveTo = isStoreHovering ? 0 : -15;
            double storeIconOpacityTo = isStoreHovering ? 1 : 0;

            DoubleAnimation storeTextMoveAnimation = new DoubleAnimation
            {
                To = storeTextMoveTo,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut }
            };
            StoreTextTransform.BeginAnimation(TranslateTransform.XProperty, storeTextMoveAnimation);

            DoubleAnimation storeIconMoveAnimation = new DoubleAnimation
            {
                From = storeIconMoveFrom,
                To = storeIconMoveTo,
                Duration = TimeSpan.FromSeconds(0.2),
                EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut }
            };
            StoreIconTransform.BeginAnimation(TranslateTransform.XProperty, storeIconMoveAnimation);

            DoubleAnimation storeIconOpacityAnimation = new DoubleAnimation
            {
                To = storeIconOpacityTo,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            StoreButtonIcon.BeginAnimation(UIElement.OpacityProperty, storeIconOpacityAnimation);
        }

        private async void CheckStatus()
        {
            string url = "http://127.0.0.1:80/oasis/status/backend/launcher";
            await CheckEndpointStatusAsync(url);
        }

        private async Task CheckEndpointStatusAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        StatusText.Text = "Online";
                        StatusText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6dd48a"));
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        StatusText.Text = "Offline";
                        StatusText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ec1c24"));
                    }
                    else
                    {
                        StatusText.Text = "Offline";
                        StatusText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ec1c24"));
                    }
                }
                catch (Exception)
                {
                    StatusText.Text = "Error";
                }
            }
        }

        private void Home()
        {
            MainFrame.Navigate(new Home());
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new Home());
        }

        public void NavigateToPage(Page newPage)
        {
            DoubleAnimation fadeOut = new DoubleAnimation
            {
                From = 1,
                To = 0.5, 
                Duration = TimeSpan.FromSeconds(0.3)
            };

            //await Task.Delay(100);

            DoubleAnimation fadeIn = new DoubleAnimation
            {
                From = 0.5, 
                To = 1,    
                Duration = TimeSpan.FromSeconds(0.3)
            };

            fadeOut.Completed += (s, e) =>
            {
                MainFrame.Navigate(newPage);

                MainFrame.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            };

            MainFrame.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

        private void FNPage_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new FortnitePage());
        }

        private void Store_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new StorePage());
        }
    }
}
