using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025.Day6
{
    public class TrashCompactorPt2
    {
        public long[][] numbers;

        public Operations[] operations;

        public TrashCompactorPt2(string input)
        {
            string[] lines = File.ReadAllLines(input);
            int height = lines.Length;
            int width = lines[0].Length;
            numbers = new long[lines.Length - 1][];
            int columns = -1;

            List<long> totals = new List<long>();
            List<long> allNumbers = new List<long>();
            bool columnIsAllBlanks = true;
            for (int i = width - 1; i >= 0; i--)
            {
                List<char> currentNumberCharacters = new List<char>();
                Operations? currentOperation = null;
                for (global::System.Int32 j = 0; j < height; j++)
                {
                    char currentCharacter = lines[j][i];

                    if (char.IsDigit(currentCharacter))
                    {
                        currentNumberCharacters.Add(currentCharacter);
                    }
                    else if (currentCharacter == '*')
                    {
                        currentOperation = Operations.Multiplication;
                    }
                    else if (currentCharacter == '+')
                    {
                        currentOperation = Operations.Addition;
                    }
                }

                if (currentNumberCharacters.Count == 0)
                {
                    allNumbers = new List<long>();
                    continue;
                }

                long number = long.Parse(string.Join("", currentNumberCharacters.ToArray()));
                allNumbers.Add(number);

                if (currentOperation != null)
                {
                    if (currentOperation == Operations.Addition)
                    {
                        long total = 0;
                        for (global::System.Int32 j = 0; j < allNumbers.Count; j++)
                        {
                            long currentNumber = allNumbers[j];
                            total += currentNumber;
                        }
                        totals.Add(total);
                    }
                    else if (currentOperation == Operations.Multiplication)
                    {
                        long total = 1;
                        for (global::System.Int32 j = 0; j < allNumbers.Count; j++)
                        {
                            long currentNumber = allNumbers[j];
                            total *= currentNumber;
                        }
                        totals.Add(total);
                    }
                    currentOperation = null;
                }
            }
            long sum = totals.Sum(t => t);
            Console.WriteLine(sum);
        }
    }
}
