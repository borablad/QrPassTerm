﻿using QrPassTerm.Services.rest_and_interface;
using QrPassTerm.Views;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using QrPassTerm.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QrPassTerm
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<RestMockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            OnResume();

        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }
        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
