using RankTracker.Models;
using RankTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RankTracker.Views
{
    public partial class NewGamePage : ContentPage
    {
        public Game Game { get; set; }

        public NewGamePage()
        {
            InitializeComponent();
            BindingContext = new NewGameViewModel();
        }
    }
}