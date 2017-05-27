using System;
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
            const string inputFile = @"A-large-practice.in";
            const string outputFile = @"output.txt";

            Solve(inputFile, outputFile);

            Process.Start(outputFile);

            Clipboard.SetText(Path.GetFullPath(outputFile));
        }

        static void Solve(string inputFile, string outputFile)
        {
            var lines = File.ReadLines(inputFile).ToList();
            var t = long.Parse(lines[0]);

            lines = lines.Skip(1).ToList();

            using (var sw = new StreamWriter(outputFile))
            {
                for (var ti = 0; ti < t; ti++)
                {
                    var xs = lines[ti].Split();

                    var pancakes = xs[0].ToCharArray().Select(c => c == '+').ToList();
                    var flipperSize = long.Parse(xs[1]);
                    var flippingCount = 0;

                    while (pancakes.Any())
                    {
                        if (pancakes[0])
                        {
                            pancakes.RemoveAt(0);
                            continue;
                        }

                        if (pancakes.Count < flipperSize)
                        {
                            break;
                        }

                        for (var i = 0; i < flipperSize; i++)
                        {
                            pancakes[i] = !pancakes[i];
                        }

                        flippingCount++;

                        pancakes.RemoveAt(0);
                    }

                    sw.WriteLine(pancakes.Any()
                        ? $"Case #{ti + 1}: IMPOSSIBLE"
                        : $"Case #{ti + 1}: {flippingCount}");
                }
            }
        }
    }
}