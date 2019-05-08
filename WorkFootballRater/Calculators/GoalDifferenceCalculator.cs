using System;
using System.Collections.Generic;
using System.Linq;
using WorkFootballRater.Results;

namespace WorkFootballRater.Calculators
{
    public class GoalDifferenceCalculator
    {
        private readonly ResultsFile _results;

        public GoalDifferenceCalculator(ResultsFile results)
        {
            _results = results;
        }

        public GoalDifferences Calculate()
        {
            var allGameGoalDifferences = new Dictionary<string, List<int>>();
            foreach (var game in _results.Games)
            {
                var shirtsGoalDifference = game.Score.Shirts - game.Score.Bibs;
                foreach (var shirtsPlayer in game.ShirtsPlayers)
                {
                    if(!allGameGoalDifferences.ContainsKey(shirtsPlayer.Initials))
                        allGameGoalDifferences.Add(shirtsPlayer.Initials, new List<int>());
                    allGameGoalDifferences[shirtsPlayer.Initials].Add(shirtsGoalDifference);
                }

                var bibsGoalDifference = -shirtsGoalDifference;
                foreach (var bibsPlayer in game.BibsPlayers)
                {
                    if (!allGameGoalDifferences.ContainsKey(bibsPlayer.Initials))
                        allGameGoalDifferences.Add(bibsPlayer.Initials, new List<int>());
                    allGameGoalDifferences[bibsPlayer.Initials].Add(bibsGoalDifference);
                }
            }

            var goalDifferences = new GoalDifferences();
            foreach (var player in allGameGoalDifferences)
            {
                var averageGoalDifference = (decimal)player.Value.Sum() / player.Value.Count;
                goalDifferences.AddPlayer(player.Key, averageGoalDifference);
            }

            return goalDifferences;
        }
    }
}