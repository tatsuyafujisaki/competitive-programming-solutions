using System.Collections.Generic;
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

            var xs = File.ReadLines(inputFile).ToList();
            var t = int.Parse(xs[0]);
            var ns = xs.GetRange(1, t).Select(long.Parse).ToList();

            var result = new List<string>();

            for (var ti = 0; ti < t; ti++)
            {
                if (ns[ti] == 0)
                {
                    result.Add($"Case #{ti + 1}: INSOMNIA");
                    continue;
                }

                var founds = new bool[10];

                long result2;
                for (var i = 1; ; i++)
                {
                    result2 = ns[ti] * i;
                    var digits = result2.ToString().ToCharArray().Select(c => (int)char.GetNumericValue(c));

                    foreach (var d in digits)
                    {
                        founds[d] = true;
                    }

                    if (founds.All(b => b))
                    {
                        break;
                    }
                }

                result.Add($"Case #{ti + 1}: {result2}");
            }

            File.WriteAllLines(outputFile, result);
        }
    }
}