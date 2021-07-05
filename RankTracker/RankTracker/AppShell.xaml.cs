using RankTracker.ViewModels;
using RankTracker.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RankTracker
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GameDetailPage), typeof(GameDetailPage));
            Routing.RegisterRoute(nameof(NewGamePage), typeof(NewGamePage));
            Routing.RegisterRoute(nameof(NewPlayerPage), typeof(NewPlayerPage));
        }

    }
}
