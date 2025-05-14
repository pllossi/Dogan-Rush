using Dogan_Rush.View;
using Dogan_Rush.Views;
namespace Dogan_Rush
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // In App.xaml.cs or wherever you initialize your main page
            MainPage = new NavigationPage(new MainPage());

        }
    }
}