using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QrPassTerm.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QrPassTerm.ViewModels
{
    public partial class MainPageViewModel:BaseViewModel
    {
        [ObservableProperty]
        private int qrValue;

        private GenerateQr generateQr;
        private bool check;
        [ObservableProperty]
        private bool inPage = true;

        [ObservableProperty]
        private bool systemTheme, lightTheme, darkTheme , isExpanded;
        [ObservableProperty]
        private string userThem;


        public int userTheme
        {
            get => Preferences.Get("CastTheme", 0);
            set
            {
                Preferences.Set("CastTheme", value);
                OnPropertyChanged(nameof(userTheme));
            }
        }

        public MainPageViewModel()
        {

        }
        internal async void OnAppering()
        {
            if (userTheme == 2) DarkTheme = true;
            else if (userTheme == 1) LightTheme = true;
            else SystemTheme = true;

            try {
                generateQr = await DataStore.GetQr();
                QrValue = generateQr.Code;
            } catch { ShowWarning("error", "need inet"); return; }
            getQr();

        }

        [RelayCommand] // Смена темы
        public void ThemeSelectionChanged(string parm)
        {
            int preferens = int.Parse(parm);
            userTheme = preferens;
            switch (preferens)
            {
                case 2:
                    {

                        Xamarin.Forms.Application.Current.UserAppTheme = OSAppTheme.Dark;
                        UserThem = "Тёмная";
                        SystemTheme = false;
                        LightTheme = false;

                  


                        break;
                    }
                case 1:
                    {
                        Xamarin.Forms.Application.Current.UserAppTheme = OSAppTheme.Light;
                        UserThem = "Светлая";
                        SystemTheme = false;
                        DarkTheme = false;
                       

                        break;
                    }

                default:
                    {
                        Xamarin.Forms.Application.Current.UserAppTheme = OSAppTheme.Unspecified;
                        UserThem = "Системная";
                        LightTheme = false;
                        DarkTheme = false;
                       
                        break;
                    }
            }
        }

        private async Task getQr()
        {
            if (check) return;
            check = true;
            int count = 0;
            while (InPage)
            {
                if (count >= 25)
                {
                    count = 0;
                    try
                    {
                        generateQr = await DataStore.GetQr();
                        QrValue = generateQr.Code;
                    }
                    catch { ShowWarning("error", "need inet"); return; }
                }
                count++;
                await Task.Delay(1000);
            }
        }


        internal void OnDessapiring()
        {
            InPage=false;
        }
    }
}
