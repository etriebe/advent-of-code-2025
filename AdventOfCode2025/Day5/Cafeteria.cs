using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025.Day5
{
    public class Cafeteria
    {
        private string _ingredientList {  get; set; }

        private string _rawText { get; set; }

        public List<long> FreshIngredients { get; set; }

        public List<long> Ingredients { get; set; }

        public List<IngredientRange> IngredientRanges { get; set; }
        
        public long TotalFreshIngredients { get; set; }

        public Cafeteria(string ingredientList)
        {
            _ingredientList = ingredientList;

            if (!File.Exists(_ingredientList))
            {
                throw new ArgumentException("Unable to find ingredient list");
            }

            _rawText = File.ReadAllText(_ingredientList);

            this.IngredientRanges = new List<IngredientRange>();
            this.FreshIngredients = new List<long>();
            this.Ingredients = new List<long>();

            bool processingRanges = true;
            foreach (string line in _rawText.Split('\n'))
            {
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine))
                {
                    processingRanges = false;
                    continue;
                }

                if (processingRanges)
                {
                    string[] rangeSplit = trimmedLine.Split("-".ToCharArray());

                    if (rangeSplit.Length != 2)
                    {
                        throw new ArgumentException($"Invalid range! {trimmedLine}");
                    }
                    long lowerBound = long.Parse(rangeSplit[0]);
                    long upperBound = long.Parse(rangeSplit[1]);
                    IngredientRange range = new IngredientRange(lowerBound, upperBound);
                    this.IngredientRanges.Add(range);
                }
                else
                {
                    this.Ingredients.Add(long.Parse(trimmedLine));
                }
            }

            IngredientRanges = IngredientRanges.OrderBy(i => i.LowerBound).ToList();

            foreach (long ingredientID in this.Ingredients)
            {
                bool isFresh = false;
                foreach (IngredientRange range in IngredientRanges)
                {
                    if (ingredientID < range.LowerBound)
                    {
                        break;
                    }

                    if (ingredientID >= range.LowerBound && ingredientID <= range.UpperBound)
                    {
                        Console.WriteLine($"Fresh Ingredient ID: {ingredientID}");
                        this.FreshIngredients.Add(ingredientID);
                        isFresh = true;
                        break;
                    }
                }

                if (!isFresh)
                {
                    Console.WriteLine($"Spoiled Ingredient ID: {ingredientID}");
                }
            }

            this.TotalFreshIngredients = 0;

            long previousLowerBound = -1;
            long previousUpperBound = -1;
            for (int i = 0; i < this.IngredientRanges.Count; i++)
            {
                IngredientRange currentRange = this.IngredientRanges[i];

                if (previousLowerBound == -1)
                {
                    previousLowerBound = currentRange.LowerBound;
                }

                if (previousUpperBound == -1)
                {
                    previousUpperBound = currentRange.UpperBound;
                }

                long currentLowerBound = currentRange.LowerBound;
                long currentUpperBound = currentRange.UpperBound;

                if (currentLowerBound >= previousLowerBound && currentLowerBound <= previousUpperBound)
                {
                    previousUpperBound = Math.Max(previousUpperBound, currentUpperBound);
                }
                else if (currentUpperBound > previousUpperBound)
                {
                    Console.WriteLine($"Marking range as fresh: {previousLowerBound} - {previousUpperBound}");
                    this.TotalFreshIngredients += previousUpperBound - previousLowerBound + 1;
                    previousLowerBound = currentLowerBound;
                    previousUpperBound = currentUpperBound;
                }
                else
                {
                    Console.WriteLine("Not sure we'd get here...");
                }
            }

            // Add one last time beacuse we didn't sum for the last iteration
            Console.WriteLine($"Marking range as fresh: {previousLowerBound} - {previousUpperBound}");
            this.TotalFreshIngredients += previousUpperBound - previousLowerBound + 1;
        }
    }
}
