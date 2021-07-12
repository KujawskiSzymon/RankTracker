using RankTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RankTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchDetailPage : ContentPage
    {
        public MatchDetailPage()
        {
            InitializeComponent();
            BindingContext = new MatchDetailViewModel();
        }
    }
}