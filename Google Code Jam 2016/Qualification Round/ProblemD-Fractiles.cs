using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gcj
{
    internal static class Program
    {
        private static void Main()
        {
            const string inputFile = @"D-large-practice.in";
            const string outputFile = @"output.txt";

            var lines = File.ReadLines(inputFile).ToList();
            var t = long.Parse(lines[0]);

            lines = lines.Skip(1).ToList();

            var tiles = new List<long>();
            var result = new List<string>();

            for (var i = 0; i < t; i++)
            {
                var xs = lines[i].Split().Select(long.Parse).ToList();
                var k = xs[0];
                var c = xs[1];
                var s = xs[2];

                if (s * c < k)
                {
                    result.Add($"Case #{i + 1}: IMPOSSIBLE");
                    continue;
                }

                tiles.Clear();

                for (long j = 1; j < k + 1; j += c)
                {
                    long p = 1;

                    for (long m = 0; m < c; m++)
                    {
                        p = (p - 1) * k + Math.Min(j + m, k);
                    }
                    tiles.Add(p);
                }

                result.Add($"Case #{i + 1}: {string.Join(" ", tiles)}");
            }

            File.WriteAllLines(outputFile, result);
        }
    }
}