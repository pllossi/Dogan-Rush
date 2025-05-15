using Dogan_Rush.ViewModels;
using Dogan_Rush.Infrastracture;
using Dogan_Rush.Models;
using CommunityToolkit.Mvvm.Input;

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

        [RelayCommand]
        public async Task OnStartGameClicked()
        {
            await Navigation.PushAsync(new GamePage());
        }


    }
}
