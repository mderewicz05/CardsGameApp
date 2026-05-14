using System.Collections.Generic;
using System.Collections.ObjectModel;
using CardGamesApp.Models;
using CardGamesApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardGamesApp.ViewModels;

public partial class OczkoViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly PlayerService _playerService;

    private Deck _talia = new();

    [ObservableProperty]
    private Card? wybranaKartaGracza;

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

    public ObservableCollection<Card> KartyGracza { get; } = new();

    public ObservableCollection<Card> KartyKrupiera { get; } = new();

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

        KartyGracza.Clear();
        KartyKrupiera.Clear();

        KartyGracza.Add(_talia.DobierzKarte());
        KartyGracza.Add(_talia.DobierzKarte());

        KartyKrupiera.Add(_talia.DobierzKarte());
        KartyKrupiera.Add(_talia.DobierzKarte());

        WybranaKartaGracza = null;
        CzyGraAktywna = true;

        Komunikat = "Gra rozpoczęta. Dobierz kartę, usuń jedną kartę albo zakończ turę.";
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

        KartyGracza.Add(_talia.DobierzKarte());
        AktualizujTeksty();

        if (ObliczSume(KartyGracza) > 21)
        {
            CzyGraAktywna = false;
            Komunikat = "Przekroczono 21. Przegrywasz.";
        }
    }

    [RelayCommand] 
    private void UsunWybranaKarte()
    {
        if (!CzyGraAktywna)
        {
            Komunikat = "Najpierw rozpocznij grę.";
            return;
        }

        if (WybranaKartaGracza is null)
        {
            Komunikat = "Najpierw wybierz kartę gracza z listy.";
            return;
        }

        if (KartyGracza.Count <= 1)
        {
            Komunikat = "Nie możesz usunąć ostatniej karty.";
            return;
        }

        KartyGracza.Remove(WybranaKartaGracza);
        WybranaKartaGracza = null;

        Komunikat = "Usunięto wybraną kartę.";
        AktualizujTeksty();
    }

    [RelayCommand]
    private void Zostan()
    {
        if (!CzyGraAktywna)
        {
            Komunikat = "Najpierw rozpocznij grę.";
            return;
        }

        while (ObliczSume(KartyKrupiera) < 17)
        {
            KartyKrupiera.Add(_talia.DobierzKarte());
        }

        CzyGraAktywna = false;
        AktualizujTeksty();
        UstalZwyciezce();
    }

    private int ObliczSume(IEnumerable<Card> karty)
    {
        int suma = 0;
        int liczbaAsow = 0;

        foreach (var karta in karty)
        {
            if (karta.Figura == "A")
            {
                suma += 11;
                liczbaAsow++;
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

        while (suma > 21 && liczbaAsow > 0)
        {
            suma -= 10;
            liczbaAsow--;
        }

        return suma;
    }

    private void UstalZwyciezce()
    {
        int sumaGracza = ObliczSume(KartyGracza);
        int sumaKrupiera = ObliczSume(KartyKrupiera);

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
    }

    private void AktualizujTeksty()
    {
        KartyKrupieraTekst = string.Join(", ", KartyKrupiera);
        SumaGraczaTekst = $"Suma gracza: {ObliczSume(KartyGracza)}";
        SumaKrupieraTekst = $"Suma krupiera: {ObliczSume(KartyKrupiera)}";
    }

    [RelayCommand]
    private void PowrotDoMenu()
    {
        _mainWindowViewModel.ShowMainMenu();
    }
}