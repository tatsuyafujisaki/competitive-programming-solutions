using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gcj
{
    internal static class Program
    {
        private static long _minDiff;
        private static string _answer;

        private static void Main()
        {
            const string inputFile = @"B-large-practice.in";
            const string outputFile = @"output.txt";

            var lines = File.ReadLines(inputFile).ToList();
            var tn = long.Parse(lines[0]);

            lines = lines.Skip(1).ToList();

            var result = new List<string>();

            for (var ti = 0; ti < tn; ti++)
            {
                var xs = lines[ti].Split().ToList();
                var c = new StringBuilder(xs[0]);
                var j = new StringBuilder(xs[1]);

                var n = c.Length;

                _minDiff = long.MaxValue;
                _answer = "";

                for (var i = 0; i < n; i++)
                {
                    if (c[i] != '?' && j[i] != '?')
                    {
                        if (c[i] < j[i])
                        {
                            UpdateMinDiffAndAnswer(c.ToString().Replace('?', '9'), j.ToString().Replace('?', '0'));
                        }
                        else
                        {
                            UpdateMinDiffAndAnswer(c.ToString().Replace('?', '0'), j.ToString().Replace('?', '9'));
                        }
                    }
                    else if (c[i] == '?' && j[i] == '?')
                    {
                        UpdateMinDiffAndAnswer(new StringBuilder(c.ToString()) { [i] = '0' }.ToString().Replace('?', '9'),
                                               new StringBuilder(j.ToString()) { [i] = '1' }.ToString().Replace('?', '0'));

                        UpdateMinDiffAndAnswer(new StringBuilder(c.ToString()) { [i] = '1' }.ToString().Replace('?', '0'),
                                               new StringBuilder(j.ToString()) { [i] = '0' }.ToString().Replace('?', '9'));

                        c[i] = '0';
                        j[i] = '0';
                    }
                    else if (c[i] == '?' && j[i] != '?')
                    {
                        if (j[i] != '9')
                        {
                            UpdateMinDiffAndAnswer(new StringBuilder(c.ToString()) { [i] = ToChar(ToInt(j[i]) + 1) }.ToString().Replace('?', '0'),
                                                   new StringBuilder(j.ToString()).ToString().Replace('?', '9'));
                        }

                        if (j[i] != '0')
                        {
                            UpdateMinDiffAndAnswer(new StringBuilder(c.ToString()) { [i] = ToChar(ToInt(j[i]) - 1) }.ToString().Replace('?', '9'),
                                                   new StringBuilder(j.ToString()).ToString().Replace('?', '0'));
                        }

                        c[i] = j[i];
                    }
                    else
                    {
                        if (c[i] != '9')
                        {
                            UpdateMinDiffAndAnswer(new StringBuilder(c.ToString()).ToString().Replace('?', '9'),
                                                   new StringBuilder(j.ToString()) { [i] = ToChar(ToInt(c[i]) + 1) }.ToString().Replace('?', '0'));
                        }

                        if (c[i] != '0')
                        {
                            UpdateMinDiffAndAnswer(new StringBuilder(c.ToString()).ToString().Replace('?', '0'),
                                                   new StringBuilder(j.ToString()) { [i] = ToChar(ToInt(c[i]) - 1) }.ToString().Replace('?', '9'));
                        }

                        j[i] = c[i];
                    }
                }

                UpdateMinDiffAndAnswer(c.ToString(), j.ToString());

                result.Add($"Case #{ti + 1}: {_answer}");
            }

            File.WriteAllLines(outputFile, result);
        }

        private static void UpdateMinDiffAndAnswer(string s1, string s2)
        {
            var diff = Math.Abs(long.Parse(s1) - long.Parse(s2));

            var candidate = s1 + ' ' + s2;

            if (diff < _minDiff || diff == _minDiff && string.Compare(candidate, _answer, StringComparison.Ordinal) < 0)
            {
                _minDiff = diff;
                _answer = candidate;
            }
        }

        private static int ToInt(int c)
        {
            return c - '0';
        }

        private static char ToChar(int x)
        {
            return x.ToString()[0];
        }
    }
}