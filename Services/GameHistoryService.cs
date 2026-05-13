using System;
using System.Collections.Generic;
using CardGamesApp.Services;
using CardGamesApp.Models;

namespace CardGamesApp.Services
{
    public class GameHistoryService
    {
        public static List<GameHistoryEntry> History { get; set; } = new();

        public static void AddEntry(GameHistoryEntry entry)
        {
            History.Add(entry);
        }

        public static List<GameHistoryEntry> GetHistory() => History;
}
}