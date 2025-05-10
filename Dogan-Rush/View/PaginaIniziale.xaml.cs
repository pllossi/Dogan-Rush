using CommunityToolkit.Mvvm.Input;

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
        public async Task IniziaGioco()
        {
            await Navigation.PushAsync(new SchermataGioco());
        }

        private void btnIstruzioni_Clicked(object sender, EventArgs e)
        {
            //verrà mostrate le istruzioni
        }
    }
}