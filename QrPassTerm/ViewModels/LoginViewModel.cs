using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QrPassTerm.Models;
using QrPassTerm.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QrPassTerm.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {

        [ObservableProperty]
        private string uName, pass;
        [ObservableProperty]
        private bool systemTheme, lightTheme, darkTheme, isExpanded;
        [ObservableProperty]
        private string userThem;

        public string SchemeM { get { return Sheme; } set { Sheme = value; } }
        public string PortM { get { return Port; } set { Port = value; } }
        public string HostM { get { return HostUrl; } set { HostUrl = value; } }

        public int userTheme
        {
            get => Preferences.Get("CastTheme", 0);
            set
            {
                Preferences.Set("CastTheme", value);
                OnPropertyChanged(nameof(userTheme));
            }
        }
        public LoginViewModel()
        {

        }
        internal async void onAppering()
        {
            if (userTheme == 2) DarkTheme = true;
            else if (userTheme == 1) LightTheme = true;
            else SystemTheme = true;
            UName=UserName;
            Pass = Password;
         /*   if (string.IsNullOrWhiteSpace(UserName) && string.IsNullOrWhiteSpace(Password)) return;
            UserName =  UName ;
             Password=Pass ;
            try
            {
                IsBusy = true;
                var response = await DataStore.LoginAsync(new UserDto { UserName = UserName, Password = Password });
                Preferences.Set("access_token", response);
                Preferences.Set("auth_scheme", $"Bearer");
                IsBusy = false;
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            catch
            {

            }*/

        }
        [RelayCommand]
        private async void Login()
        {
           /* await Shell.Current.GoToAsync($"{nameof(MainPage)}");
            return;*/
            IsBusy = true;
            try
            {
                if (string.IsNullOrWhiteSpace(UName) || string.IsNullOrWhiteSpace(Pass))
                {
                    ShowWarning("Ошибка", "Заполните поля");
                    IsBusy = false;
                    return;
                }
                // await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                var numb = UName.Replace("+", "").Replace(" ", "");
             
                var response = await DataStore.LoginAsync(new UserDto { UserName = numb, Password = Pass });
              
                SaveUserDadta();
                Preferences.Set("access_token", response);
                Preferences.Set("auth_scheme", $"Bearer");
               
            }
            catch (Exception ex)
            {
                // await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

                IsBusy = false;
                 ShowWarning("Ошибка", ex.Message); return;


              

              
            }
            IsBusy = false;
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
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

        private void SaveUserDadta()
        {
            UserName = UName;
            Password = Pass;
        }
    }
}
