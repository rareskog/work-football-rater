using System.Collections.Generic;

namespace WorkFootballRater.Results
{
    public class WeeklyGame
    {
        public int Week { get; set; }
        public Score Score { get; set; }
        public List<Player> BibsPlayers { get; set; }
        public List<Player> ShirtsPlayers { get; set; }
    }
}