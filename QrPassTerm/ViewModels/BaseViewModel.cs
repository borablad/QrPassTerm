﻿using QrPassTerm.Models;
using QrPassTerm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using CommunityToolkit.Mvvm.ComponentModel;
using Xamarin.CommunityToolkit.Extensions;
using QrPassTerm.Widgets;
using Xamarin.Essentials;
using QrPassTerm.Services.rest_and_interface;
namespace QrPassTerm.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
       // public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        [ObservableProperty]
        bool isBusy = false;
        [ObservableProperty]
        string title = string.Empty;

        public string UserName { get => Preferences.Get(nameof(UserName), ""); set => Preferences.Set(nameof(UserName), value); }
        public string Password { get => Preferences.Get(nameof(Password), ""); set => Preferences.Set(nameof(Password), value); }

        public RestI DataStore => DependencyService.Get<RestI>();



        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public async void ShowWarning(string title, string message)
        {
            try
            {
                var popup = new WarningView();
                var popupvm = (WarningViewModel)popup.BindingContext;
                popupvm.Title = title;
                popupvm.Message = message;
                var page = await App.Current.MainPage.ShowPopupAsync(popup);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
