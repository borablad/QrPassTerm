using QrPassTerm.Services.rest_and_interface;
using QrPassTerm.Views;
using System;
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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
