using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QrPassTerm.Models;
using QrPassTerm.Views;
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


        public string SchemeM { get { return Sheme; } set { Sheme = value; } }  
        public string PortM { get { return Port; } set { Port = value; } }  
        public string HostM { get { return HostUrl; } set { HostUrl = value; } }
        private const int CountToGetQr = 25;


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
                OnPropertyChanged(nameof(QrValue));
            } catch { ShowWarning("error", "need inet");  }
            getQr();

        }

        // LogAut
        [RelayCommand]
        public void Logout()
        {
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        [RelayCommand]
        public void ShowResult(string param)
        {
            bool parm;
            if (param == "true")
                parm = true;
            else parm = false;
            ShowEnteredResult(parm);
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
                if (count >= CountToGetQr)
                {
                    count = 0;
                    try
                    {
                        generateQr = await DataStore.GetQr();
                        QrValue = generateQr.Code;
                    }
                    catch { ShowWarning("error", "need inet");  }
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
