using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025.Day5
{
    public class IngredientRange
    {
        public long LowerBound { get; set; }
        public long UpperBound { get; set; }

        public IngredientRange(long lowerBound, long upperBound)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException("Invalid range!");
            }

            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
        }

        public bool IngredientIsFresh(long ingredientID)
        {
            if (ingredientID < LowerBound)
            {
                return false;
            }

            if (ingredientID > UpperBound)
            {
                return false;
            }
            return true;
        }
    }
}
