﻿using LocationTest.Services;
using System;
using Xamarin.Forms;

namespace LocationTest.Pages
{
    public class LoginPage : ContentPage
    {
        private readonly StackLayout PageLayout;

        public LoginPage()
        {
            // Create the Button and attach Clicked handler.
            Image logo = new Image
            {
                Source = "logo_clear.png",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                Margin = new Thickness(20),
                Scale = 0.2
            };

            Label title = new Label
            {
                Text = "Welcome to Breeze!",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 22,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black
            };

            Label subtitle = new Label
            {
                Text = "Please sign in to be able to use this application.",
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 18,
                Margin = new Thickness(0, 5, 0, 0)
            };

            Button button = new Button
            {
                Text = "Log in / Sign Up",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20, 0, 0),
                Padding = new Thickness(20, 5)
            };
            button.Clicked += this.OnButtonClicked;

            Image background = new Image
            {
                Aspect = Aspect.AspectFill,
                Source = "background.jpg"
            };

            this.PageLayout = new StackLayout
            {
                Children =
                {
                    title,
                    subtitle,
                    button
                },
                BackgroundColor = Color.FromRgba(255, 255, 255, 0.7),
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(30),
                Margin = new Thickness(15)
            };

            Grid grid = new Grid();
            grid.Children.Add(background);
            grid.Children.Add(logo);
            grid.Children.Add(this.PageLayout);

            this.Content = grid;
        }

        private async void OnButtonClicked(object sender, EventArgs args)
        {
            this.PageLayout.Children.Clear();
            this.PageLayout.Children.Add(new ActivityIndicator
            {
                IsEnabled = true,
                IsRunning = true,
                IsVisible = true,
                Color = Color.FromHex("#58aef8"),
                Margin = new Thickness(0, 54)
            });

            IAuthenticationService authenticationService = DependencyService.Get<IAuthenticationService>();
            LoginResult authenticationResult = await authenticationService.Authenticate();

            if (authenticationResult.Error)
            {
                Application.Current.MainPage = new LoginPage();
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new MainPage(authenticationResult));
            }
        }
    }
}
