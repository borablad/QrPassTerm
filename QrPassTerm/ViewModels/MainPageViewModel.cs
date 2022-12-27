using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QrPassTerm.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public MainPageViewModel()
        {

        }
        internal async void OnAppering()
        {
            try {
                generateQr = await DataStore.GetQr();
                QrValue = generateQr.Code;
            } catch { ShowWarning("error", "need inet"); return; }
            getQr();

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
    }
}
