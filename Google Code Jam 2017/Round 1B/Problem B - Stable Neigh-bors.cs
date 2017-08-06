using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Gcj
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string inputFile = @"B-large-practice.in";
            const string outputFile = @"Output.txt";

            Solve(inputFile, outputFile);

            Process.Start(outputFile);

            Clipboard.SetText(Path.GetFullPath(outputFile));
        }

        static void Solve(string inputPath, string outputPath)
        {
            var lines = File.ReadLines(inputPath).ToList();
            var t = int.Parse(lines[0]);
            lines = lines.Skip(1).ToList();

            (int, int, int, int, int, int, int) GetSeven(IReadOnlyList<int> xs) => (xs[0], xs[1], xs[2], xs[3], xs[4], xs[5], xs[6]);

            using (var sw = new StreamWriter(outputPath))
            {
                for (var ti = 0; ti < t; ti++)
                {
                    var (n, r, o, y, g, b, v) = GetSeven(lines[0].Split().Select(int.Parse).ToList());
                    lines = lines.Skip(1).ToList();

                    string result;

                    if (r == 0 && 0 < o && y == 0 && g == 0 && v == 0)
                    {
                        result = o == b ? Repeat("OB", o) : "IMPOSSIBLE";
                    }
                    else if (o == 0 && y == 0 && 0 < g && b == 0 && v == 0)
                    {
                        result = g == r ? Repeat("GR", g) : "IMPOSSIBLE";
                    }
                    else if (r == 0 && o == 0 && g == 0 && b == 0 && 0 < v)
                    {
                        result = v == y ? Repeat("VY", v) : "IMPOSSIBLE";
                    }
                    else if (0 < o && b <= o
                             || 0 < g && r <= g
                             || 0 < v && y <= v)
                    {
                        result = "IMPOSSIBLE";
                    }
                    else
                    {
                        b -= o;
                        r -= g;
                        y -= v;

                        n = r + y + b;

                        if (Math.Floor(n / 2.0) < new[] { r, y, b }.Max())
                        {
                            result = "IMPOSSIBLE";
                        }
                        else
                        {
                            var xs = new List<(char, int)> { ('R', r), ('Y', y), ('B', b) }.OrderBy(x => x.Item2).ToList();

                            var ss = Enumerable.Repeat(xs[0].Item1, xs[0].Item2)
                                    .Concat(Enumerable.Repeat(xs[1].Item1, xs[1].Item2))
                                    .Concat(Enumerable.Repeat(xs[2].Item1, xs[2].Item2))
                                    .Select(c => c.ToString()).ToList();

                            if (0 < o)
                            {
                                for (var i = 0; i < n; i++)
                                {
                                    if (ss[i] == "B")
                                    {
                                        ss[i] = Repeat("BO", o) + "B";
                                        break;
                                    }
                                }
                            }

                            if (0 < g)
                            {
                                for (var i = 0; i < n; i++)
                                {
                                    if (ss[i] == "R")
                                    {
                                        ss[i] = Repeat("RG", g) + "R";
                                        break;
                                    }
                                }
                            }

                            if (0 < v)
                            {
                                for (var i = 0; i < n; i++)
                                {
                                    if (ss[i] == "Y")
                                    {
                                        ss[i] = Repeat("YV", v) + "Y";
                                        break;
                                    }
                                }
                            }

                            var stack = new Stack<string>(ss);

                            for (var i = 0; i < n; i += 2)
                            {
                                ss[i] = stack.Pop();
                            }

                            for (var i = 1; i < n; i += 2)
                            {
                                ss[i] = stack.Pop();
                            }

                            result = string.Concat(ss);
                        }
                    }

                    sw.WriteLine($"Case #{ti + 1}: {result}");
                }
            }
        }

        static string Repeat(string s, int n) => string.Concat(Enumerable.Repeat(s, n));
    }
}