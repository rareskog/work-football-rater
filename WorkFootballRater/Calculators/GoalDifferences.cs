using System.Collections.Generic;
using System.Linq;

namespace WorkFootballRater.Calculators
{
    public class GoalDifferences
    {
        public GoalDifferences()
        {
            Players = new List<GoalDifference>();
        }

        private List<GoalDifference> Players { get; set; }

        public void AddPlayer(string initials, decimal averageGoalDifference)
        {
            Players.Add(new GoalDifference { Player = initials, AverageGoalDifference = averageGoalDifference });
        }

        public decimal For(string player)
        {
            return Players.Single(x => x.Player == player).AverageGoalDifference;
        }
    }
}