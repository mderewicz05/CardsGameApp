using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CardGamesApp.Models;
using CardGamesApp.Services;
using CommunityToolkit.Mvvm.Input;

namespace CardGamesApp.ViewModels
{
    public partial class HigherOrLowerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly PlayerService _playerService;

        private Deck _deck;
        private Card _currentCard;
        private string _message = "";
        private int _score;

        // W³aœciwoœci
        public Card CurrentCard
        {
            get => _currentCard;
            set { _currentCard = value; OnPropertyChanged(); }
        }

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public int Score
        {
            get => _score;
            set { _score = value; OnPropertyChanged(); }
        }
        public HigherOrLowerViewModel(MainWindowViewModel mainWindowViewModel, PlayerService playerService)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _playerService = playerService;

            _deck = new Deck();
            _currentCard = _deck.DobierzKarte();
            _message = "Zgadnij: kolejna karta bedzie wyzsza czy nizsza?";
        }

        [RelayCommand]
        public void GuessHigher() => MakeGuess(true);

        [RelayCommand]
        public void GuessLower() => MakeGuess(false);

        private void MakeGuess(bool guessedHigher)
        {
            Card nextCard = _deck.DobierzKarte();

            if (nextCard == null)
            {
                Message = "Koniec talii!";
                return;
            }

            if ((guessedHigher && nextCard.Wartosc > _currentCard.Wartosc) ||
                (!guessedHigher && nextCard.Wartosc < _currentCard.Wartosc))
            {
                Score++;
                Message = $"Dobrze! To by³a {nextCard}.";
            }
            else if (nextCard.Wartosc == _currentCard.Wartosc)
            {
                Message = $"Remis! To by³a {nextCard}. Punktow bez zmian.";
            }
            else
            {
                Message = $"Pudlo! To by³a {nextCard}. Twój wynik to {Score}.";
                GameHistoryService.AddEntry(new GameHistoryEntry
                {
                    GameName = "Higher or Lower",
                    Players = "Gracz 1",
                    Winner = $"Wynik: {Score}",
                    Date = DateTime.Now
                });
                Score = 0;
            }

            CurrentCard = nextCard;
        }

        [RelayCommand]
        private void PowrotDoMenu()
        {
            if (_mainWindowViewModel != null)
            {
                _mainWindowViewModel.ShowMainMenu();
            }
        }
    }
}