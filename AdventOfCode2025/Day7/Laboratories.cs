using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025.Day7
{
    public class Laboratories
    {
        private string[] _fileContents { get; set; }

        private MapState[,] map {  get; set; }

        public int NumberOfSplits { get; set; }

        public Laboratories(string file)
        {
            _fileContents = File.ReadAllLines(file);

            if (_fileContents == null)
            {
                throw new ArgumentException("");
            }
            int numberOfLines = _fileContents.Length;
            map = new MapState[_fileContents[0].Length + 1, numberOfLines + 1];
            for (int i = 0; i < _fileContents.Length; i++)
            {
                string line = _fileContents[i];
                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];
                    map[i,j] = GetMapStateFromChar(c);
                }
            }

            NumberOfSplits = 0;

            HashSet<int> nextLineNumbers = new HashSet<int>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                MapState[] currentLine = Enumerable.Range(0, map.GetLength(0))
                        .Select(x => map[i, x])
                        .ToArray();
                MapState[]? nextLine = null;

                if (i != map.GetLength(0) - 1)
                {
                    nextLine = Enumerable.Range(0, map.GetLength(0))
                        .Select(x => map[i + 1, x])
                        .ToArray();
                }

                if (nextLine == null)
                {
                    break;
                }

                HashSet<int> copyOfNextLineNumbers = nextLineNumbers;
                nextLineNumbers = new HashSet<int>();
                if (i == 0)
                {
                    int indexOfStart = currentLine.IndexOf(MapState.Start);
                    MapState charBelow = nextLine[indexOfStart];
                    if (charBelow == MapState.Splitter)
                    {
                        nextLineNumbers.Add(indexOfStart - 1);
                        nextLineNumbers.Add(indexOfStart + 1);
                        NumberOfSplits++;
                    }
                    else if (charBelow == MapState.Off)
                    {
                        nextLineNumbers.Add(indexOfStart);
                    }
                }
                else
                {
                    foreach (int index in copyOfNextLineNumbers)
                    {
                        MapState charBelow = nextLine[index];

                        if (charBelow == MapState.Splitter)
                        {
                            nextLineNumbers.Add(index - 1);
                            nextLineNumbers.Add(index + 1);
                            NumberOfSplits++;
                        }
                        else if (charBelow == MapState.Off)
                        {
                            nextLineNumbers.Add(index);
                        }
                    }
                }
            }
        }

        private MapState[] GetMapStateFromLine(string line)
        {
            MapState[] lineState = new MapState[line.Length];

            for (int i = 0; i < line.Length; i ++)
            {
                char c = line[i];
                lineState[c] = GetMapStateFromChar(c);
            }
            return lineState;
        }

        private MapState GetMapStateFromChar(char c)
        {
            switch (c)
            {
                case '.':
                    return MapState.Off;
                case 'S':
                    return MapState.Start;
                case '^':
                    return MapState.Splitter;
                case '|':
                    return MapState.On;
                default:
                    throw new ArgumentException($"Invalid map state: {c}");
            }
        }
    }

    public enum MapState
    {
        Off,
        On,
        Start,
        Splitter,
    }
}
