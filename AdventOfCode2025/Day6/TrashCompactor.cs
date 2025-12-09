using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025.Day6
{
    public class TrashCompactor
    {
        public long[][] numbers;

        public Operations[] operations;

        public TrashCompactor(string input)
        {
            string[] lines = File.ReadAllLines(input);
            numbers = new long[lines.Length - 1][];
            int columns = -1;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] items = lines[i].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (columns == -1)
                {
                    columns = items.Length;
                }

                for (global::System.Int32 j = 0; j < items.Length; j++)
                {
                    string item = items[j];

                    if (string.IsNullOrEmpty(item))
                    {
                        continue;
                    }

                    if (i == lines.Length - 1)
                    {
                        if (operations == null)
                        {
                            operations = new Operations[items.Length];
                        }

                        if (item == "*")
                        {
                            operations[j] = Operations.Multiplication;
                        }
                        else
                        {
                            operations[j] = Operations.Addition;
                        }
                    }
                    else
                    {
                        if (numbers[i] == null)
                        {
                            numbers[i] = new long[items.Length];
                        }

                        long num = long.Parse(item);
                        numbers[i][j] = num;
                    }
                }
            }

            long?[] totals = new long?[columns];

            for (int i = 0; i < numbers.Length; i++)
            {
                for (global::System.Int32 j = 0; j < numbers[i].Length; j++)
                {
                    Operations op = operations[j];
                    if (totals[j] == null)
                    {
                        // Start with the correct seed values
                        if (op == Operations.Multiplication)
                        {
                            totals[j] = 1;
                        }
                        else if (op == Operations.Addition)
                        {
                            totals[j] = 0;
                        }
                    }
                    if (op == Operations.Addition)
                    {
                        totals[j] += numbers[i][j];
                    }
                    else if (op == Operations.Multiplication)
                    {
                        totals[j] *= numbers[i][j];
                    }
                }
            }

            Console.WriteLine($"Sum: {totals.Sum(t => t)}");
        }
    }

    public enum Operations
    {
        Addition,
        Multiplication,
    }

}
