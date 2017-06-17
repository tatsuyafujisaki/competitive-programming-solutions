using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Gcj
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string inputFile = @"D-large-practice.in";
            const string outputFile = @"Output.txt";

            Solve(inputFile, outputFile);

            Process.Start(outputFile);

            Clipboard.SetText(Path.GetFullPath(outputFile));
        }

        static void Solve(string inputPath, string outputPath)
        {
            const char bishopSign = '+';
            const char rookSign = 'x';
            const char queenSign = 'o';
            const char blankSign = '*';

            var lines = File.ReadLines(inputPath).ToList();
            var t = long.Parse(lines[0]);
            lines = lines.Skip(1).ToList();

            (int, int) GetTwo(IReadOnlyList<int> xs) => (xs[0], xs[1]);

            using (var sw = new StreamWriter(outputPath))
            {
                for (var ti = 0; ti < t; ti++)
                {
                    var (n, m) = GetTwo(lines[0].Split().Select(int.Parse).ToList());

                    var rookMatrix = new bool[n, n];
                    var bishopMatrix = new bool[n, n];

                    var xs = lines.GetRange(1, m).Select(s => s.Split()).ToList();

                    foreach (var x in xs)
                    {
                        var rowIndex = int.Parse(x[1]) - 1;
                        var columnIndex = int.Parse(x[2]) - 1;

                        switch (char.Parse(x[0]))
                        {
                            case bishopSign:
                                bishopMatrix[rowIndex, columnIndex] = true;
                                break;
                            case rookSign:
                                rookMatrix[rowIndex, columnIndex] = true;
                                break;
                            case queenSign:
                                bishopMatrix[rowIndex, columnIndex] = true;
                                rookMatrix[rowIndex, columnIndex] = true;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    var newBishopMatrix = Bishop.GetBishopsWithExtra(Matrix.ToCharMatrix(bishopMatrix, bishopSign, blankSign));
                    var newRookMatrix = Rook.GetRooksWithExtra(Matrix.ToCharMatrix(rookMatrix, rookSign, blankSign));

                    var extraModels = new HashSet<(char, int, int)>();

                    for (var rowIndex = 0; rowIndex < n; rowIndex++)
                    {
                        for (var columnIndex = 0; columnIndex < n; columnIndex++)
                        {
                            if (bishopMatrix[rowIndex, columnIndex] != newBishopMatrix[rowIndex, columnIndex])
                            {
                                extraModels.Add(newRookMatrix[rowIndex, columnIndex] ? (queenSign, rowIndex, columnIndex) : (bishopSign, rowIndex, columnIndex));
                            }

                            if (rookMatrix[rowIndex, columnIndex] != newRookMatrix[rowIndex, columnIndex])
                            {
                                extraModels.Add(newBishopMatrix[rowIndex, columnIndex] ? (queenSign, rowIndex, columnIndex) : (rookSign, rowIndex, columnIndex));
                            }
                        }
                    }

                    sw.WriteLine($"Case #{ti + 1}: {GetStylePoints(newBishopMatrix) + GetStylePoints(newRookMatrix)} {extraModels.Count}");

                    foreach (var (modelSign, rowIndex, columnIndex) in extraModels)
                    {
                        sw.WriteLine($"{modelSign} {rowIndex + 1} {columnIndex + 1}");
                    }

                    lines = lines.Skip(1 + m).ToList();
                }
            }
        }

        static int GetStylePoints(bool[,] matrix)
        {
            var n = matrix.GetLength(0);

            var stylePoints = 0;

            for (var rowIndex = 0; rowIndex < n; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < n; columnIndex++)
                {
                    if (matrix[rowIndex, columnIndex])
                    {
                        stylePoints++;
                    }
                }
            }

            return stylePoints;
        }
    }
}