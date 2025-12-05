using AdventOfCode2025.Day5;

namespace AdventOfCode2025;

class Program
{
    static void Main(string[] args)
    {
        Cafeteria cafe = new Cafeteria(@"D:\repos\advent-of-code-2025\AdventOfCode2025\Day5\ingredients.txt");
        Console.WriteLine(cafe.FreshIngredients.Count);
        Console.WriteLine(cafe.TotalFreshIngredients);
        Cafeteria cafe2 = new Cafeteria(@"D:\repos\advent-of-code-2025\AdventOfCode2025\Day5\ingredients2.txt");
        Console.WriteLine(cafe2.FreshIngredients.Count);
        Console.WriteLine(cafe2.TotalFreshIngredients);
    }
}
