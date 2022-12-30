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
        public LoginViewModel()
        {

        }
        internal async void onAppering()
        {

            if (string.IsNullOrWhiteSpace(UserName) && string.IsNullOrWhiteSpace(Password)) return;
            UName = UserName;
            Pass = Password;
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

            }

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
        private void SaveUserDadta()
        {
            UserName = UName;
            Password = Pass;
        }
    }
}
