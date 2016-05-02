using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gcj
{
    internal static class Program
    {
        private static readonly string[] Zero = { "Z", "E", "R", "O" };
        private static readonly string[] One = { "O", "N", "E" };
        private static readonly string[] Two = { "T", "W", "O" };
        private static readonly string[] Three = { "T", "H", "R", "E", "E" };
        private static readonly string[] Four = { "F", "O", "U", "R" };
        private static readonly string[] Five = { "F", "I", "V", "E" };
        private static readonly string[] Six = { "S", "I", "X" };
        private static readonly string[] Seven = { "S", "E", "V", "E", "N" };
        private static readonly string[] Eight = { "E", "I", "G", "H", "T" };
        private static readonly string[] Nine = { "N", "I", "N", "E" };

        private static void Main()
        {
            const string inputFile = @"A-large-practice.in";
            const string outputFile = @"output.txt";

            var lines = File.ReadLines(inputFile).ToList();
            var n = int.Parse(lines[0]);

            lines = lines.Skip(1).ToList();

            var result = new List<string>();

            for (var i = 0; i < n; i++)
            {
                var numbers = new List<int>();

                var s = lines[i];

                while (s != "")
                {
                    var s0 = s;

                    if (s.Contains("Z"))
                    {
                        s = RemoveSubstrings(s, Zero);
                        numbers.Add(0);
                    }

                    if (s.Contains("W"))
                    {
                        s = RemoveSubstrings(s, Two);
                        numbers.Add(2);
                    }

                    if (s.Contains("U"))
                    {
                        s = RemoveSubstrings(s, Four);
                        numbers.Add(4);
                    }

                    if (s.Contains("X"))
                    {
                        s = RemoveSubstrings(s, Six);
                        numbers.Add(6);
                    }

                    if (s.Contains("G"))
                    {
                        s = RemoveSubstrings(s, Eight);
                        numbers.Add(8);
                    }

                    if (s0 == s)
                    {
                        break;
                    }
                }

                while (s != "")
                {
                    var s0 = s;

                    if (s.Contains("O"))
                    {
                        s = RemoveSubstrings(s, One);
                        numbers.Add(1);
                    }

                    if (s.Contains("H"))
                    {
                        s = RemoveSubstrings(s, Three);
                        numbers.Add(3);
                    }

                    if (s.Contains("F"))
                    {
                        s = RemoveSubstrings(s, Five);
                        numbers.Add(5);
                    }

                    if (s.Contains("S"))
                    {
                        s = RemoveSubstrings(s, Seven);
                        numbers.Add(7);
                    }

                    if (s0 == s)
                    {
                        break;
                    }
                }

                while (s != "")
                {
                    s = RemoveSubstrings(s, Nine);
                    numbers.Add(9);
                }

                numbers.Sort();

                result.Add($"Case #{i + 1}: {string.Join("", numbers)}");
            }

            File.WriteAllLines(outputFile, result);
        }

        private static string RemoveSubstrings(string s0, IEnumerable<string> substrings)
        {
            return substrings.Aggregate(s0, (s, substring) => s.Remove(s.IndexOf(substring, StringComparison.OrdinalIgnoreCase), 1));
        }
    }
}