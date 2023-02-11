using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QrPassTerm.ViewModels
{

    public partial class ResultEnterViewModel : BaseViewModel
    {
        #region Fields
        [ObservableProperty]
        private bool message;
        #endregion

        public ResultEnterViewModel()
        {

        }

        [RelayCommand]
        private async Task CloseTapped()
        {
            MessagingCenter.Send(this, "ClosePopup");
            await Task.CompletedTask;
        }
    }

}