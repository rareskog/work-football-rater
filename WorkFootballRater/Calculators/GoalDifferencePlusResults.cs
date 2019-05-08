using System.Collections.Generic;

namespace WorkFootballRater.Calculators
{
    public class GoalDifferencePlusResults
    {
        public GoalDifferencePlusResults()
        {
            Players = new List<GoalDifferencePlus>();
        }

        public List<GoalDifferencePlus> Players { get; set; }
        
        public void AddPlayer(string initials, decimal averageGoalDifference)
        {
            Players.Add(new GoalDifferencePlus { Player = initials, Value = averageGoalDifference });
        }
    }
}