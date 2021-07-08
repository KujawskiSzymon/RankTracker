using RankTracker.Models;
using RankTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    [QueryProperty(nameof(GameId), nameof(GameId))]
    public class SelectPlayersViewModel : BaseViewModel
    {
        private string gameId;
        private ObservableCollection<PlayerView> players;
        public string Id { get; set; }
        public Command CreateMatchCommand { get; }

        public Command CheckPlayerCommand { get; }

        public SelectPlayersViewModel()
        {
            Players = new ObservableCollection<PlayerView>();
            CreateMatchCommand = new Command(OnCreateMatch,Validate);
            this.PropertyChanged +=
                    (_, __) => CreateMatchCommand.ChangeCanExecute();
            
        }
        public string GameId
        {
            get
            {
                return gameId;
            }
            set
            {
                gameId = value;
                LoadGameId(value);

            }
        }

        public ObservableCollection<PlayerView> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }

        public async void LoadGameId(string itemId)
        {
            try
            {
                Players.Clear();
                var item = await GamesStore.GetGameAsync(itemId);
                Id = item.Id;
                var p = item.Players;
                foreach (var player in p)
                {
                    PlayerView pv = new PlayerView() { id = player.Id, ischecked = false, name = player.Name };
                    Players.Add(pv);
                }
               
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        private async void OnCreateMatch()
        {
            Static.AppInfoStatic.currentPlayersInMatch = new List<PlayerView>();
            Static.AppInfoStatic.currentPlayersInMatch.Clear();
            foreach (var p in Players)
            {
                if(p.ischecked)
                Static.AppInfoStatic.currentPlayersInMatch.Add(p);
            }
            await Shell.Current.GoToAsync(nameof(CreateMatchView));
        }

        private bool Validate()
        {
            int value = 0;
            foreach (var p in Players)
            {
                if (p.ischecked)
                    value++;
                
            }
            if (value > 1)
                return true;
            else
                return false;

        }
        private async void OnCheck(object obj)
        {
            throw new NotImplementedException();
        }


    }
}
