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
            const string inputFile = @"B-large-practice.in";
            const string outputFile = @"Output.txt";

            Solve(inputFile, outputFile);

            Process.Start(outputFile);

            Clipboard.SetText(Path.GetFullPath(outputFile));
        }

        static void Solve(string inputPath, string outputPath)
        {
            var lines = File.ReadLines(inputPath).ToList();
            var t = long.Parse(lines[0]);

            lines = lines.Skip(1).ToList();

            using (var sw = new StreamWriter(outputPath))
            {
                for (var ti = 0; ti < t; ti++)
                {
                    var xs = lines[ti].ToCharArray().Select(c => c - '0').ToList();

                    while (true)
                    {
                        var unsortedIndex = GetUnsortedIndex(xs);

                        if (unsortedIndex == -1)
                        {
                            sw.WriteLine($"Case #{ti + 1}: {string.Join("", xs)}");
                            break;
                        }

                        xs[unsortedIndex - 1] -= 1;

                        for (var i = unsortedIndex; i < xs.Count; i++)
                        {
                            xs[i] = 9;
                        }

                        xs = string.Join("", xs).TrimStart('0').ToCharArray().Select(c => c - '0').ToList();
                    }
                }
            }
        }

        static int GetUnsortedIndex(IReadOnlyList<int> xs)
        {
            for (var i = 1; i < xs.Count; i++)
            {
                if (xs[i] < xs[i - 1])
                {
                    return i;
                }
            }

            return -1;
        }
    }
}