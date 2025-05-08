using CommunityToolkit.Mvvm.ComponentModel;
using Dogan_Rush.Models;

namespace Dogan_Rush.View_Models
{
    public partial class IDCardViewModel : ObservableObject
    {
        public VISACard VISACard
        {
            get => default;
            set
            {
            }
        }

        public IDCardViewModel(VISACard visacard) 
        {
            VISACard = visacard;
        }

    }
}