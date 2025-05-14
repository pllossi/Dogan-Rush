using CommunityToolkit.Mvvm.Input;
using Dogan_Rush.Models;
namespace Dogan_Rush.View;

public partial class GamePage : ContentPage
{
    private GameManager _gameManager;

    public GamePage()
    {
        _gameManager = new GameManager();
        InitializeComponent();
    }

    public GamePage(GameManager gameManager)
    {
        _gameManager = gameManager;
        InitializeComponent();
    }

    [RelayCommand]
    public async void TakeGuess(bool guess)
    {
        _gameManager.Guess(guess);

        await Navigation.PushAsync(new GamePage(_gameManager));
    }

    private void btn_Visa(object sender, EventArgs e)
    {
        //apre la finestra del documento visa
    }

    private void btn_Id(object sender, EventArgs e)
    {
        //apre la finestra del documento Id
    }

    private void btn_Entry(object sender, EventArgs e)
    {
        //apre la finestra del documento del visto d'entrata
    }

    private void btnIndietro_MainPage(object sender, EventArgs e)
    {
        //comunicare con la finestra iniziale per tornare indietro
    }

}