using System.Collections.Generic;
using System.Linq;
using WorkFootballRater.Results;

namespace WorkFootballRater.Calculators
{
    public class GoalDifferencePlusCalculator
    {
        private readonly ResultsFile _results;
        private readonly GoalDifferences _unadjustedGoalDifferences;

        public GoalDifferencePlusCalculator(ResultsFile results, GoalDifferences unadjustedGoalDifferences)
        {
            _results = results;
            _unadjustedGoalDifferences = unadjustedGoalDifferences;
        }

        public GoalDifferencePlusResults Calculate()
        {
            var dictionaryOfResults = new Dictionary<string, List<decimal>>();

            foreach (var game in _results.Games)
            {
                var shirtsTotalGoalDifference = game.ShirtsPlayers.Select(x => _unadjustedGoalDifferences.For(x.Initials)).Sum();
                var shirtsAverageGoalDifference = shirtsTotalGoalDifference / game.ShirtsPlayers.Count;
                var bibsTotalGoalDifference = game.BibsPlayers.Select(x => _unadjustedGoalDifferences.For(x.Initials)).Sum();
                var bibsAverageGoalDifference = bibsTotalGoalDifference / game.BibsPlayers.Count;

                var shirtsGameGoalDifference = game.Score.Shirts - game.Score.Bibs;

                var playerImbalanceAdjustment = 0m;
                if (game.ShirtsPlayers.Count != game.BibsPlayers.Count)
                    playerImbalanceAdjustment = (decimal)(game.ShirtsPlayers.Count - game.BibsPlayers.Count) / 2;

                foreach (var shirtsPlayer in game.ShirtsPlayers)
                {
                    var shirtsAverageGoalDifferenceWithAveragePlayer = (shirtsTotalGoalDifference - _unadjustedGoalDifferences.For(shirtsPlayer.Initials)) / game.ShirtsPlayers.Count;
                    var gameGoalDifferencePlus = shirtsGameGoalDifference - (shirtsAverageGoalDifferenceWithAveragePlayer - bibsAverageGoalDifference);
                    gameGoalDifferencePlus -= playerImbalanceAdjustment;

                    if(!dictionaryOfResults.ContainsKey(shirtsPlayer.Initials))
                        dictionaryOfResults[shirtsPlayer.Initials] = new List<decimal>();
                    dictionaryOfResults[shirtsPlayer.Initials].Add(gameGoalDifferencePlus);
                }

                var bibsGameGoalDifference = -shirtsGameGoalDifference;

                foreach (var bibsPlayer in game.BibsPlayers)
                {
                    var bibsAverageGoalDifferenceWithAveragePlayer = (bibsTotalGoalDifference - _unadjustedGoalDifferences.For(bibsPlayer.Initials)) / game.BibsPlayers.Count;
                    var gameGoalDifferencePlus = bibsGameGoalDifference - (bibsAverageGoalDifferenceWithAveragePlayer - shirtsAverageGoalDifference);
                    gameGoalDifferencePlus += playerImbalanceAdjustment;

                    if (!dictionaryOfResults.ContainsKey(bibsPlayer.Initials))
                        dictionaryOfResults[bibsPlayer.Initials] = new List<decimal>();
                    dictionaryOfResults[bibsPlayer.Initials].Add(gameGoalDifferencePlus);
                }
            }

            var goalDifferencePlusResults = new GoalDifferencePlusResults();
            foreach (var player in dictionaryOfResults)
            {
                var averageGoalDifferencePlus = player.Value.Sum() / player.Value.Count;
                goalDifferencePlusResults.AddPlayer(player.Key, averageGoalDifferencePlus);
            }

            return goalDifferencePlusResults;
        }
    }
}