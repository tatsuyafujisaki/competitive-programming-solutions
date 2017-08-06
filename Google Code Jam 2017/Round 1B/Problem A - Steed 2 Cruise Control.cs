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
            const string inputFile = @"A-large-practice.in";
            const string outputFile = @"Output.txt";

            Solve(inputFile, outputFile);

            Process.Start(outputFile);

            Clipboard.SetText(Path.GetFullPath(outputFile));
        }

        static void Solve(string inputPath, string outputPath)
        {
            var lines = File.ReadLines(inputPath).ToList();
            var t = int.Parse(lines[0]);
            lines = lines.Skip(1).ToList();

            (int, int) GetPair(IReadOnlyList<int> xs) => (xs[0], xs[1]);

            using (var sw = new StreamWriter(outputPath))
            {
                for (var ti = 0; ti < t; ti++)
                {
                    var (d, n) = GetPair(lines[0].Split().Select(int.Parse).ToList());

                    sw.WriteLine($"Case #{ti + 1}: {d / lines.GetRange(1, n).Select(s => s.Split().Select(int.Parse).ToList()).Select(xs => new { K = xs[0], S = xs[1] }).Max(h => (double)(d - h.K) / h.S):#.000000}");

                    lines = lines.Skip(1 + n).ToList();
                }
            }
        }
    }
}