using RankTracker.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace RankTracker.Views
{
    public partial class GameDetailPage : ContentPage
    {
        public GameDetailPage()
        {
            InitializeComponent();
            BindingContext = new GameDetailViewModel();
        }
    }
}