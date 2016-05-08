﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gcj
{
    internal static class Program
    {
        private static void Main()
        {
            const string inputFile = @"A-large-practice.in";
            const string outputFile = @"output.txt";

            var lines = File.ReadLines(inputFile).ToList();
            var t = int.Parse(lines[0]);

            lines = lines.Skip(1).ToList();

            var result = new List<string>(t);

            for (var ti = 0; ti < t; ti++)
            {
                var ps = lines[1].Split().Select(long.Parse).ToList();

                var evacuees = new List<string>();

                while (3 < ps.Sum())
                {
                    evacuees.Add(Pop(ps) + Pop(ps));
                }

                if (ps.Sum() == 3)
                {
                    evacuees.Add(Pop(ps));
                }

                evacuees.Add(Pop(ps) + Pop(ps));

                result.Add($"Case #{ti + 1}: {string.Join(" ", evacuees)}");

                lines = lines.Skip(2).ToList();
            }

            File.WriteAllLines(outputFile, result);
        }

        private static string Pop(IList<long> xs)
        {
            var i = IndexOfMax(xs);
            xs[i]--;
            return ((char)('A' + i)).ToString();
        }

        private static int IndexOfMax(IList<long> xs)
        {
            var max = xs[0];
            var indexOfMax = 0;

            var n = xs.Count;
            for (var i = 1; i < n; i++)
            {
                if (max < xs[i])
                {
                    max = xs[i];
                    indexOfMax = i;
                }
            }

            return indexOfMax;
        }
    }
}