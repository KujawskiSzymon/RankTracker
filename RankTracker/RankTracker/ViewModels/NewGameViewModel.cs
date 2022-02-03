using RankTracker.Models;
using RankTracker.Services;
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
                Name = Name
            };
            GameDataStore database = await GameDataStore.Instance;
            await database.SaveGameAsync(newItem);

            //await GamesStore.AddGameAsync(newItem); delete after sql

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
