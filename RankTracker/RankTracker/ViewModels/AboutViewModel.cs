using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Credits";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/KujawskiSzymon/RankTracker"));
        }

        public ICommand OpenWebCommand { get; }
    }
}