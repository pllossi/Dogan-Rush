using Dogan_Rush.ViewModels;
using Dogan_Rush.Infrastracture;
using Dogan_Rush.Models;
using Dogan_Rush.Views;

namespace Dogan_Rush.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }


        private void btnInfo_Clicked(object sender, EventArgs e)
        {
            // Mostra istruzioni
        }

        private async void OnStartGameClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }


    }
}
