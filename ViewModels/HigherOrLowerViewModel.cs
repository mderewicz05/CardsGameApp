using System.ComponentModel;
using System.Runtime.CompilerServices;
using CardGamesApp.Models;

namespace CardGamesApp.ViewModels
{
    public class HigherOrLowerViewModel : INotifyPropertyChanged
    {
        private Deck _deck;
        private Card _currentCard;
        private string _message;
        private int _score;

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

        public HigherOrLowerViewModel()
        {
            _deck = new Deck();
            _currentCard = _deck.DobierzKarte();
            Message = "Zgadnij: kolejna karta bedzie wyzsza czy nizsza?";
        }

        // Metoda dla przycisku "Wy¿sza"
        public void GuessHigher() => MakeGuess(true);

        // Metoda dla przycisku "Ni¿sza"
        public void GuessLower() => MakeGuess(false);

        private void MakeGuess(bool guessedHigher)
        {
            Card nextCard = _deck.DobierzKarte();

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
                Message = $"Pudlo! To by³a {nextCard}. Zaczynamy od nowa.";
                // Tutaj mo¿esz wywo³aæ zapis do historii (Twój Etap 3)
                // SaveToHistory("Higher or Lower", Score);
                Score = 0;
            }

            CurrentCard = nextCard;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}