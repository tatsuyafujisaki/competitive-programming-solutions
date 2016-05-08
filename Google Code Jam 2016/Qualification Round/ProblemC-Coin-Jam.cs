using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gcj
{
    internal static class Program
    {
        private static void Main()
        {
            const string inputFile = @"C-large-practice.in";
            const string outputFile = @"output.txt";

            var xs = File.ReadLines(inputFile).Skip(1).First().Split();
            var n = int.Parse(xs[0]);
            var j = int.Parse(xs[1]);

            var result = new List<string>(j + 1) { "Case #1:" };

            for (var i = 0; i < n - 10; i++)
            {
                for (var k = 0; k < n - 10 - i; k++)
                {
                    for (var l = 0; l < n - 10 - i - k; l++)
                    {
                        var m = n - 10 - i - k - l;

                        var jamcoin = $"11{new string('0', i)}11{new string('0', k)}11{new string('0', l)}11{new string('0', m)}11";
                        const string divisors = "3 2 5 2 7 2 3 2 11";
                        result.Add($"{jamcoin} {divisors}");

                        if (--j == 0)
                        {
                            File.WriteAllLines(outputFile, result);
                        }
                    }
                }
            }
        }
    }
}