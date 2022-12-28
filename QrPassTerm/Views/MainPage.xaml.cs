using QrPassTerm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QrPassTerm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm;
        public MainPage()
        {
            InitializeComponent();
            vm = (MainPageViewModel)BindingContext;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.OnAppering();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.OnDessapiring();
        }
    }
}