using CommunityToolkit.Mvvm.Input;
using Dogan_Rush.Models;
namespace Dogan_Rush.View;

public partial class SchermataGioco : ContentPage
{
    private GameManager _gameManager;

    public SchermataGioco()
    {
        _gameManager = new GameManager();
        InitializeComponent();
    }

    public SchermataGioco(GameManager gameManager)
    {
        _gameManager = gameManager;
        InitializeComponent();
    }

    [RelayCommand]
    public async void TakeGuess(bool guess)
    {
        _gameManager.Guess(guess);

        await Navigation.PushAsync(new SchermataGioco(_gameManager));
    }

}