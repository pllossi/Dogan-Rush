using CommunityToolkit.Mvvm.Input;
using Dogan_Rush.Models;
using Dogan_Rush.View_Models;
using Dogan_Rush.View;

namespace Dogan_Rush.View
{
    public partial class MainPage : ContentPage
    {
        private int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        [RelayCommand]
        public async Task StartGame()
        {
            await Navigation.PushAsync(new GamePage());

            GamePage gamePage = new GamePage();
            ///non funziona il metodo show
            ///gamePage.Sho();
            this.IsVisible = false;

        }

        private void btnInfo_Clicked(object sender, EventArgs e)
        {
            //verrà mostrate le istruzioni
        }
    }
}