using AdventOfCode2025.Day5;
using AdventOfCode2025.Day7;

namespace AdventOfCode2025;

class Program
{
    static void Main(string[] args)
    {
        Laboratories lab = new Laboratories(@"D:\repos\advent-of-code-2025\AdventOfCode2025\Day7\BeamSplitter.txt");
        Console.WriteLine(lab.NumberOfSplits);
        Laboratories lab2 = new Laboratories(@"D:\repos\advent-of-code-2025\AdventOfCode2025\Day7\BeamSplitter2.txt");
        Console.WriteLine(lab2.NumberOfSplits);

        LaboratoriesPt2 lab3 = new LaboratoriesPt2(@"D:\repos\advent-of-code-2025\AdventOfCode2025\Day7\BeamSplitter.txt", false);
        Console.WriteLine(lab3.NumberOfTimelines);
        LaboratoriesPt2 lab4 = new LaboratoriesPt2(@"D:\repos\advent-of-code-2025\AdventOfCode2025\Day7\BeamSplitter2.txt", false);
        Console.WriteLine(lab4.NumberOfTimelines);

        /*
        Cafeteria cafe = new Cafeteria(@"D:\repos\advent-of-code-2025\AdventOfCode2025\Day5\ingredients.txt");
        Console.WriteLine(cafe.FreshIngredients.Count);
        Console.WriteLine(cafe.TotalFreshIngredients);
        Cafeteria cafe2 = new Cafeteria(@"D:\repos\advent-of-code-2025\AdventOfCode2025\Day5\ingredients2.txt");
        Console.WriteLine(cafe2.FreshIngredients.Count);
        Console.WriteLine(cafe2.TotalFreshIngredients);
        */
    }
}
