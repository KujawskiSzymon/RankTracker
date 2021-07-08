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
            Routing.RegisterRoute(nameof(PlayerDetailPage), typeof(PlayerDetailPage));
            Routing.RegisterRoute(nameof(SelectPlayersPage), typeof(SelectPlayersPage));
            Routing.RegisterRoute(nameof(CreateMatchView), typeof(CreateMatchView));
            Routing.RegisterRoute(nameof(GamesPage), typeof(GamesPage));
        }

    }
}
