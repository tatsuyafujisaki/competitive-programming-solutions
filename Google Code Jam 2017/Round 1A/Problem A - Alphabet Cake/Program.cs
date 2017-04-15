using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Gcj
{
    static class Program
    {
        static void Main()
        {
            const string inPath = @"A-large-practice.in";
            const string outPath = @"Output.txt";

            Solve(inPath, outPath);

            Process.Start(outPath);
        }

        static void Solve(string inPath, string outPath)
        {
            var lines = File.ReadLines(inPath).ToList();
            var t = long.Parse(lines[0]);
            lines = lines.Skip(1).ToList();

            using (var sw = new StreamWriter(outPath))
            {
                for (var ti = 0; ti < t; ti++)
                {
                    sw.WriteLine($"Case #{ti + 1}:");

                    var r = int.Parse(lines[0].Split()[0]);

                    Fill(new CharGrid(lines.Skip(1).Take(r).ToList())).Write(sw);

                    lines = lines.Skip(1 + r).ToList();
                }
            }
        }

        static CharGrid Fill(CharGrid cg)
        {
            var questionCount = cg.Count('?');

            if (questionCount == 0)
            {
                return cg;
            }

            if (questionCount == cg.Length - 1)
            {
                cg.FillWithNonBlank();
                return cg;
            }

            if (cg.MoreThanTwoColumnsHaveNonBlank())
            {
                var t1 = cg.GetIndexOfLeftmostNonBlank();

                cg.SetSubgrid(Fill(cg.CreateCharSubgrid(0, 0, cg.RowLength, t1.Item2 + 1)));
                cg.SetSubgrid(Fill(cg.CreateCharSubgrid(0, t1.Item2 + 1)), 0, t1.Item2 + 1);
            }
            else
            {
                var t1 = cg.GetIndexOfLeftmostNonBlank();

                cg.SetSubgrid(Fill(cg.CreateCharSubgrid(0, 0, t1.Item1 + 1, cg.ColumnLength)));
                cg.SetSubgrid(Fill(cg.CreateCharSubgrid(t1.Item1 + 1, 0)), t1.Item1 + 1);
            }

            return cg;
        }
    }
}