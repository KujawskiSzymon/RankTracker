using RankTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    public class CreateMatchViewModel : BaseViewModel
    {
        private ObservableCollection<PlayerMatchCreate> players;
        public Command CreateMatchCommand { get; }

        public CreateMatchViewModel()
        {
            players = new ObservableCollection<PlayerMatchCreate>();
            foreach(var p in Static.AppInfoStatic.currentPlayersInMatch)
            {
                PlayerMatchCreate player = new PlayerMatchCreate() { Name = p.name, points = 0 };
                players.Add(player);
            }
            CreateMatchCommand = new Command(OnCreateMatch);
        }

        public ObservableCollection<PlayerMatchCreate> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        private async void OnCreateMatch(object obj)
        {
            
        }


    }
}
