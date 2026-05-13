using System.Collections.Generic;
using System.Collections.ObjectModel;
using CardGamesApp.Models;
using CardGamesApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace CardGamesApp.ViewModels;

public partial class OczkoViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly PlayerService _playerService;

    private Deck _talia = new();

    private readonly List<Card> _kartyGracza = new();
    private readonly List<Card> _kartyKrupiera = new();

    [ObservableProperty]
    private string kartyGraczaTekst = "-";

    [ObservableProperty]
    private string kartyKrupieraTekst = "-";

    [ObservableProperty]
    private string sumaGraczaTekst = "Suma gracza: 0";

    [ObservableProperty]
    private string sumaKrupieraTekst = "Suma krupiera: 0";

    [ObservableProperty]
    private string komunikat = "Kliknij Start, aby rozpocząć grę.";

    [ObservableProperty]
    private bool czyGraAktywna;

    public ObservableCollection<Player> Players => _playerService.Players;

    public OczkoViewModel(MainWindowViewModel mainWindowViewModel, PlayerService playerService)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _playerService = playerService;
    }

    [RelayCommand]
    private void Start()
    {
        if (Players.Count < 1)
        {
            Komunikat = "Do gry Oczko potrzeba minimum 1 gracza.";
            return;
        }

        _talia = new Deck();
        _kartyGracza.Clear();
        _kartyKrupiera.Clear();

        _kartyGracza.Add(_talia.DobierzKarte());
        _kartyGracza.Add(_talia.DobierzKarte());

        _kartyKrupiera.Add(_talia.DobierzKarte());
        _kartyKrupiera.Add(_talia.DobierzKarte());

        CzyGraAktywna = true;
        Komunikat = "Gra rozpoczęta. Dobierz kartę albo zakończ turę.";
        AktualizujTeksty();
    }

    [RelayCommand]
    private void Dobierz()
    {
        if (!CzyGraAktywna)
        {
            Komunikat = "Najpierw rozpocznij grę.";
            return;
        }

        _kartyGracza.Add(_talia.DobierzKarte());
        AktualizujTeksty();

        if (ObliczSume(_kartyGracza) > 21)
        {
            CzyGraAktywna = false;
            Komunikat = "Przekroczono 21. Przegrywasz.";
        }
    }

    [RelayCommand]
    private void Zostan()
    {
        if (!CzyGraAktywna)
        {
            Komunikat = "Najpierw rozpocznij grę.";
            return;
        }

        while (ObliczSume(_kartyKrupiera) < 17)
        {
            _kartyKrupiera.Add(_talia.DobierzKarte());
        }

        CzyGraAktywna = false;
        AktualizujTeksty();
        UstalZwyciezce();
    }

    private int ObliczSume(List<Card> karty)
    {
        int suma = 0;

        foreach (var karta in karty)
        {
            if (karta.Figura == "A")
            {
                suma += 11;
            }
            else if (karta.Figura is "J" or "Q" or "K")
            {
                suma += 10;
            }
            else
            {
                suma += karta.Wartosc;
            }
        }

        int liczbaAsow = karty.FindAll(karta => karta.Figura == "A").Count;

        while (suma > 21 && liczbaAsow > 0)
        {
            suma -= 10;
            liczbaAsow--;
        }

        return suma;
    }

    private void UstalZwyciezce()
    {
        int sumaGracza = ObliczSume(_kartyGracza);
        int sumaKrupiera = ObliczSume(_kartyKrupiera);

        string nazwaGracza = Players.Count > 0 ? Players[0].Login : "Gracz";

        if (sumaGracza > 21)
        {
            Komunikat = "Przegrywasz. Przekroczono 21.";
        }
        else if (sumaKrupiera > 21)
        {
            Komunikat = $"Wygrywa {nazwaGracza}. Krupier przekroczył 21.";
        }
        else if (sumaGracza > sumaKrupiera)
        {
            Komunikat = $"Wygrywa {nazwaGracza}.";
        }
        else if (sumaKrupiera > sumaGracza)
        {
            Komunikat = "Wygrywa krupier.";
        }
        else
        {
            Komunikat = "Remis.";
        }
        GameHistoryService.AddEntry(new GameHistoryEntry
        {
            GameName = "Oczko",
            Players = Players.Count > 0 ? Players[0].Login : "Gracz",
            Winner = Komunikat,
            Date = DateTime.Now
        });
    }

    private void AktualizujTeksty()
    {
        KartyGraczaTekst = string.Join(", ", _kartyGracza);
        KartyKrupieraTekst = string.Join(", ", _kartyKrupiera);
        SumaGraczaTekst = $"Suma gracza: {ObliczSume(_kartyGracza)}";
        SumaKrupieraTekst = $"Suma krupiera: {ObliczSume(_kartyKrupiera)}";
    }

    [RelayCommand]
    private void PowrotDoMenu()
    {
        _mainWindowViewModel.ShowMainMenu();
    }
}