using System;
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
        private bool _showSuitGuess; 

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

        public bool ShowSuitGuess
        {
            get => _showSuitGuess;
            set { _showSuitGuess = value; OnPropertyChanged(); }
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
            if (nextCard == null) { Message = "Koniec talii!"; return; }

            if ((guessedHigher && nextCard.Wartosc > _currentCard.Wartosc) ||
                (!guessedHigher && nextCard.Wartosc < _currentCard.Wartosc))
            {
                Score++;
                Message = $"Dobrze, To byla {nextCard.Figura} {nextCard.Kolor}.";
                ShowSuitGuess = false;
            }
            else if (nextCard.Wartosc == _currentCard.Wartosc)
            {
                Message = $"Remis, To byla {nextCard.Figura}.";
            }
            else
            {
                Message = $"Pudlo, To byla {nextCard.Figura} {nextCard.Kolor}. Jaki kolor miala nietrafiona karta?";
                ShowSuitGuess = true;

                GameHistoryService.AddEntry(new GameHistoryEntry
                {
                    GameName = "Higher or Lower",
                    Players = _playerService.Players.Count > 0 ? _playerService.Players[0].Login : "Gracz",
                    Winner = $"Wynik: {Score}",
                    Date = DateTime.Now
                });
            }
            CurrentCard = nextCard;
        }

        [RelayCommand]
        public void GuessSuit(string suit)
        {
            if (CurrentCard.Kolor.ToLower().Contains(suit.ToLower()))
            {
                Message = $"Trafiles kolor ({suit})! Bonus +2 punkty";
                Score += 2;
            }
            else
            {
                Message = $"Niestety, to nie by³ {suit}. Zaczynamy od zera.";
                Score = 0;
            }
            ShowSuitGuess = false;
        }

        [RelayCommand]
        private void PowrotDoMenu()
        {
            _mainWindowViewModel?.ShowMainMenu();
        }
    }
}