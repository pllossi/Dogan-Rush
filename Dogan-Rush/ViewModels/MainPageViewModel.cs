using CommunityToolkit.Mvvm.Input;
using Dogan_Rush.View;

namespace Dogan_Rush.ViewModels
{
    public partial class MainPageViewModel
    {
        [RelayCommand]
        public async Task StartGame()
        {
            // Navigate to the next page
            await Application.Current.MainPage.Navigation.PushAsync(new GamePage());
        }
    }
}