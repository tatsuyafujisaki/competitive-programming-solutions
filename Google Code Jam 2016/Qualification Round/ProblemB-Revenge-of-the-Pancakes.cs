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
            var t = int.Parse(xs[0]);
            var s = xs.GetRange(1, t).ToList();

            var result = new string[t];

            for (var ti = 0; ti < t; ti++)
            {
                var groupedHeight = 1 + CountSubstring(s[ti], "-\\+") + CountSubstring(s[ti], "\\+-");
                result[ti] = $"Case #{ti + 1}: {(s[ti].EndsWith("-") ? groupedHeight : groupedHeight - 1)}";
            }

            File.WriteAllLines(outputFile, result);
        }

        private static int CountSubstring(string s, string substring)
        {
            return Regex.Matches(s, substring, RegexOptions.IgnoreCase).Count;
        }
    }
}