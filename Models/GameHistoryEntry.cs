using System;

namespace CardGamesApp.Models
{
    public class GameHistoryEntry
    {
        public string GameName { get; set; } = string.Empty; 
        public string Players { get; set; } = string.Empty; 
        public string Winner { get; set; } = string.Empty; 
        public DateTime Date { get; set; }
    }
}