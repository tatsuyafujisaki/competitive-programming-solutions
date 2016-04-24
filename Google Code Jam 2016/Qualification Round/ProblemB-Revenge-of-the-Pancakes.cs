using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gcj
{
    internal static class Program
    {
        private static void Main()
        {
            const string inputFile = @"B-large-practice.in";
            const string outputFile = @"output.txt";

            var xs = File.ReadLines(inputFile).ToList();
            var n = int.Parse(xs[0]);
            var s = xs.GetRange(1, n).ToList();

            var result = new List<string>();

            for (var i = 0; i < n; i++)
            {
                var groupedHeight = 1 + CountSubstring(s[i], "-\\+") + CountSubstring(s[i], "\\+-");
                result.Add($"Case #{i + 1}: {(s[i].EndsWith("-") ? groupedHeight : groupedHeight - 1)}");
            }

            File.WriteAllLines(outputFile, result);
        }

        private static int CountSubstring(string s, string substring)
        {
            return Regex.Matches(s, substring, RegexOptions.IgnoreCase).Count;
        }
    }
}