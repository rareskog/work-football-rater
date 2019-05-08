using System;

namespace WorkFootballRater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter path to JSON results file: ");
            var path = Console.ReadLine();
            var results = new StatsBuilder(path).Build();
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
