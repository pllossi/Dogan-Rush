using CommunityToolkit.Mvvm.Input;

using Dogan_Rush.ViewModels;

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