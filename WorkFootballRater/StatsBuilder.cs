using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using WorkFootballRater.Calculators;
using WorkFootballRater.Results;

namespace WorkFootballRater
{
    public class StatsBuilder
    {
        private readonly string _resultsFilePath;

        public StatsBuilder(string resultsFilePath)
        {
            _resultsFilePath = resultsFilePath;
        }

        public List<string> Build()
        {
            var resultsString = File.ReadAllText(_resultsFilePath);
            var results = JsonConvert.DeserializeObject<ResultsFile>(resultsString);
            var unadjustedGoalDifferences = new GoalDifferenceCalculator(results).Calculate();
            var goalDifferencePlus = new GoalDifferencePlusCalculator(results, unadjustedGoalDifferences).Calculate();
            return goalDifferencePlus.Players.Select(x => x.ToString()).ToList();
        }
    }
}
