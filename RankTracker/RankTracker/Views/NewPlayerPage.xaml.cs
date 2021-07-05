using RankTracker.Models;
using RankTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RankTracker.Views
{
    public partial class NewPlayerPage : ContentPage
    {
        public Game Game { get; set; }

        public NewPlayerPage()
        {
            Game = Static.AppInfoStatic.currentGame;
            InitializeComponent();
            BindingContext = new NewPlayerViewModel(Game);
        }
    }
}