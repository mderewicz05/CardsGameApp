using System;
using System.Linq;
using System.Collections.ObjectModel;
using CardGamesApp.Models;
using CardGamesApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardGamesApp.ViewModels;

public partial class MemoryGameViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly PlayerService _playerService;
    private readonly Random _random = new();

    private MemoryCard? _pierwszaWybranaKarta;
    private MemoryCard? _drugaWybranaKarta;

    [ObservableProperty]
    private string komunikat = "Odkryj dwie karty i znajdź pary.";

    [ObservableProperty]
    private int liczbaRuchow;

    [ObservableProperty]
    private int znalezionePary;

    public ObservableCollection<MemoryCard> Karty { get; } = new();

    public MemoryGameViewModel(MainWindowViewModel mainWindowViewModel, PlayerService playerService)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _playerService = playerService;

        RozpocznijNowaGre();
    }

    private void RozpocznijNowaGre()
    {
        Karty.Clear();
        LiczbaRuchow = 0;
        ZnalezionePary = 0;
        Komunikat = "Odkryj dwie karty i znajdź pary.";

        string[] symbole =
        {
            "A♠", "K♥", "Q♦", "J♣", "10♠", "9♥"
        };

        int id = 1;

        foreach (var symbol in symbole)
        {
            Karty.Add(new MemoryCard(id++, symbol));
            Karty.Add(new MemoryCard(id++, symbol));
        }

        var potasowane = Karty.OrderBy(_ => _random.Next()).ToList();

        Karty.Clear();

        foreach (var karta in potasowane)
        {
            Karty.Add(karta);
        }
    }

    [RelayCommand]
    private void WybierzKarte(MemoryCard karta)
    {
        if (karta.CzyOdkryta || karta.CzyZnaleziona)
        {
            return;
        }

        if (_pierwszaWybranaKarta != null && _drugaWybranaKarta != null)
        {
            ZakryjNiepasujaceKarty();
        }

        karta.CzyOdkryta = true;
        OnPropertyChanged(nameof(Karty));

        if (_pierwszaWybranaKarta == null)
        {
            _pierwszaWybranaKarta = karta;
            Komunikat = "Wybierz drugą kartę.";
            return;
        }

        _drugaWybranaKarta = karta;
        LiczbaRuchow++;

        if (_pierwszaWybranaKarta.Symbol == _drugaWybranaKarta.Symbol)
        {
            _pierwszaWybranaKarta.CzyZnaleziona = true;
            _drugaWybranaKarta.CzyZnaleziona = true;

            ZnalezionePary++;
            Komunikat = "Znaleziono parę!";

            _pierwszaWybranaKarta = null;
            _drugaWybranaKarta = null;

            if (ZnalezionePary == 6)
            {
                Komunikat = $"Koniec gry! Liczba ruchów: {LiczbaRuchow}.";
            }
        }
        else
        {
            Komunikat = "To nie jest para. Kliknij kolejną kartę, aby zakryć poprzednie.";
        }

        OnPropertyChanged(nameof(Karty));
    }

    private void ZakryjNiepasujaceKarty()
    {
        if (_pierwszaWybranaKarta != null && !_pierwszaWybranaKarta.CzyZnaleziona)
        {
            _pierwszaWybranaKarta.CzyOdkryta = false;
        }

        if (_drugaWybranaKarta != null && !_drugaWybranaKarta.CzyZnaleziona)
        {
            _drugaWybranaKarta.CzyOdkryta = false;
        }

        _pierwszaWybranaKarta = null;
        _drugaWybranaKarta = null;

        OnPropertyChanged(nameof(Karty));
    }

    [RelayCommand]
    private void NowaGra()
    {
        RozpocznijNowaGre();
    }

    [RelayCommand]
    private void PowrotDoMenu()
    {
        _mainWindowViewModel.ShowMainMenu();
    }
}