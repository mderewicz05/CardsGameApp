using System.Collections.ObjectModel;
using CardGamesApp.Models;
using CardGamesApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardGamesApp.ViewModels;

public partial class WarGameViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly PlayerService _playerService;
    private readonly Deck _deck = new();

    [ObservableProperty]
    private string komunikat = "Kliknij przycisk, aby rozegrać rundę.";

    [ObservableProperty]
    private string kartaPierwszegoGracza = "-";

    [ObservableProperty]
    private string kartaDrugiegoGracza = "-";

    [ObservableProperty]
    private string zwyciezca = "-";

    public ObservableCollection<Player> Players => _playerService.Players;

    public WarGameViewModel(MainWindowViewModel mainWindowViewModel, PlayerService playerService)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _playerService = playerService;
    }

    [RelayCommand]
    private void RozegrajRunde()
    {
        if (Players.Count < 2)
        {
            Komunikat = "Do gry Wojna potrzeba minimum 2 graczy.";
            return;
        }

        Card pierwszaKarta = _deck.DobierzKarte();
        Card drugaKarta = _deck.DobierzKarte();

        KartaPierwszegoGracza = $"{Players[0].Login}: {pierwszaKarta}";
        KartaDrugiegoGracza = $"{Players[1].Login}: {drugaKarta}";

        if (pierwszaKarta.Wartosc > drugaKarta.Wartosc)
        {
            Zwyciezca = $"Wygrywa: {Players[0].Login}";
        }
        else if (drugaKarta.Wartosc > pierwszaKarta.Wartosc)
        {
            Zwyciezca = $"Wygrywa: {Players[1].Login}";
        }
        else
        {
            Zwyciezca = "Remis!";
        }

        Komunikat = "Runda zakończona.";
        GameHistoryService.AddEntry(new GameHistoryEntry
        {
            GameName = "Wojna",
            Players = $"{Players[0].Login} vs {Players[1].Login}",
            Winner = Zwyciezca.Replace("Wygrywa: ", "")
        });
    }

    [RelayCommand]
    private void PowrotDoMenu()
    {
        _mainWindowViewModel.ShowMainMenu();
    }
}