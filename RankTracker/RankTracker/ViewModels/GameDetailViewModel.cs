using RankTracker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    [QueryProperty(nameof(GameId), nameof(GameId))]
    public class GameDetailViewModel : BaseViewModel
    {
        private string gameId;
        private string name;
        private List<Player> players;
        public string Id { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public List<Player>Players
        {
            get => players;
            set => SetProperty(ref players, value);
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

        public async void LoadGameId(string itemId)
        {
            try
            {
                var item = await DataStore.GetGameAsync(itemId);
                Id = item.Id;
                Name = item.Name;
                Players = item.Players;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
