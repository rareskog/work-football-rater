namespace WorkFootballRater.Calculators
{
    public class GoalDifferencePlus
    {
        public string Player { get; set; }
        public decimal Value { get; set; }

        public override string ToString()
        {
            return $"{Player}: {Value:F3}";
        }
    }
}