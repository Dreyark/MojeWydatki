using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeWydatki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCategoryPopup : PopupPage
    {
        public AddCategoryPopup()
        {
            InitializeComponent();
        }

        private async void Background_tapped(object sender, EventArgs e)
        {
            await CloseAllPopup();
        }

        async private void AddCategory_Clicked(object sender, EventArgs e)
        {

            await CloseAllPopup();
        }

        private async Task CloseAllPopup()
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        void Entry_CategoryChanged(object sender, TextChangedEventArgs e)
        {
            var newText = e.NewTextValue;
            if (newText == "")
            {

                AddCategoryButton.IsVisible = false;

            }
            else
            {
                AddCategoryButton.IsVisible = true;
            }
        }
    }
}