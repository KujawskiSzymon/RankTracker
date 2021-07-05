using RankTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RankTracker.ViewModels
{
    public class NewGameViewModel : BaseViewModel
    {
        private string name;
        

        public NewGameViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name);
               
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
            
        }

      

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Game newItem = new Game()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Players = new List<Player>()
            };

            await GamesStore.AddGameAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
