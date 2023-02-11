
using QrPassTerm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace QrPassTerm.Widgets
{


    public partial class ResultEntered : Popup
    {
        private bool dis;
        ResultEnterViewModel vm;
        public ResultEntered()
        {
            InitializeComponent();
            vm = (ResultEnterViewModel)BindingContext;
            MessagingCenter.Subscribe<ResultEnterViewModel>(this, "ClosePopup", (sender) =>
            {
                if (!dis)
                {
                    dis = !dis;
                    Dismiss(this);
                }
            });

            _ = CloseP();
        }


        public async Task CloseP()
        {
            await Task.Delay(4000);
            if (!dis)
            {
                dis = !dis;
                Dismiss(this);
            }
        }

    }
}