using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025.Day7
{
    public class LaboratoriesPt2
    {
        private string[] _fileContents { get; set; }

        private MapStateV2[,] map { get; set; }

        public long NumberOfTimelines { get; set; }

        public LaboratoriesPt2(string file, bool printStatus)
        {
            _fileContents = File.ReadAllLines(file);

            if (_fileContents == null)
            {
                throw new ArgumentException("");
            }
            int numberOfLines = _fileContents.Length;
            map = new MapStateV2[numberOfLines, _fileContents[0].Length];
            for (int i = 0; i < _fileContents.Length; i++)
            {
                string line = _fileContents[i];
                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];
                    MapState state = GetMapStateFromChar(c);
                    MapStateV2 cellState = new MapStateV2();
                    cellState.State = state;
                    cellState.CurrentSum = 0;
                    map[i, j] = cellState;
                }
            }

            NumberOfTimelines = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                MapStateV2[] currentLine = Enumerable.Range(0, map.GetLength(1))
                        .Select(x => map[i, x])
                        .ToArray();
                MapStateV2[]? nextLine = null;

                if (i != map.GetLength(0) - 1)
                {
                    nextLine = Enumerable.Range(0, map.GetLength(1))
                        .Select(x => map[i + 1, x])
                        .ToArray();
                }

                if (nextLine == null)
                {
                    break;
                }

                for (global::System.Int32 index = 0; index < currentLine.Length; index++)
                {
                    MapStateV2 currentCell = currentLine[index];

                    if (currentCell.State == MapState.Start)
                    {
                        currentCell.CurrentSum = 1;
                    }

                    if (currentCell.State == MapState.Off || currentCell.State == MapState.Splitter)
                    {
                        continue;
                    }

                    MapStateV2 charBelow = nextLine[index];

                    if (charBelow.State == MapState.Splitter)
                    {
                        MapStateV2 charBelowLeft = nextLine[index - 1];
                        charBelowLeft.State = MapState.On;
                        charBelowLeft.CurrentSum += currentCell.CurrentSum;

                        MapStateV2 charBelowRight = nextLine[index + 1];
                        charBelowRight.State = MapState.On;
                        charBelowRight.CurrentSum += currentCell.CurrentSum;
                        charBelow.CurrentSum = 0;
                    }
                    else if (charBelow.State == MapState.Off)
                    {
                        charBelow.State = MapState.On;
                        charBelow.CurrentSum += currentCell.CurrentSum;
                    }
                    else if (charBelow.State == MapState.On)
                    {
                        charBelow.State = MapState.On;
                        charBelow.CurrentSum += currentCell.CurrentSum;
                    }
                }

                if (printStatus)
                {
                    PrintState(map);
                }
            }

            MapStateV2[] lastLine = Enumerable.Range(0, map.GetLength(1))
                    .Select(x => map[map.GetLength(1) - 1, x])
                    .ToArray();
            NumberOfTimelines = lastLine.Sum(cell => cell.CurrentSum);
        }

        private void PrintState(MapStateV2[,] map)
        {
            int width = map.GetLength(1);
            int height = map.GetLength(0);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    MapStateV2 state = map[i, j];

                    Console.Write(GetOutputFromMapState(state));
                }
                Console.Write("\n");
            }
        }

        private string GetOutputFromMapState(MapStateV2 map)
        {
            if (map.State == MapState.On)
            {
                return map.CurrentSum.ToString();
            }
            else if (map.State == MapState.Off)
            {
                return ".";
            }
            else if (map.State == MapState.Splitter)
            {
                return "^";
            }
            else if (map.State == MapState.Start)
            {
                return "S";
            }
            else
            {
                return "?";
            }
        }

        private MapState[] GetMapStateFromLine(string line)
        {
            MapState[] lineState = new MapState[line.Length];

            for (int i = 0; i < line.Length; i++)
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

    public class MapStateV2
    {
        public long CurrentSum { get; set; }
        public MapState State { get; set; }
    }

}
