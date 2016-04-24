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
            var jj = int.Parse(xs[1]);

            var result = new List<string> { "Case #1:" };

            for (var i = 0; i < n - 10; i++)
            {
                for (var j = 0; j < n - 10 - i; j++)
                {
                    for (var k = 0; k < n - 10 - i - j; k++)
                    {
                        var l = n - 10 - i - j - k;

                        var jamcoin = $"11{new string('0', i)}11{new string('0', j)}11{new string('0', k)}11{new string('0', l)}11";
                        const string divisors = "3 2 5 2 7 2 3 2 11";
                        result.Add($"{jamcoin} {divisors}");

                        if (--jj == 0)
                        {
                            File.WriteAllLines(outputFile, result);
                        }
                    }
                }
            }
        }
    }
}