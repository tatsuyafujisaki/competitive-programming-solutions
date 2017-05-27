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
            var result = new string[t];

            for (var ti = 0; ti < t; ti++)
            {
                var xs = lines[ti].Split().Select(long.Parse).ToList();
                var k = xs[0];
                var c = xs[1];
                var s = xs[2];

                if (s * c < k)
                {
                    result[ti] = $"Case #{ti + 1}: IMPOSSIBLE";
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

                result[ti] = $"Case #{ti + 1}: {string.Join(" ", tiles)}";
            }

            File.WriteAllLines(outputFile, result);
        }
    }
}