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
            const string inputFile = @"C-large-practice.in";
            const string outputFile = @"Output.txt";

            Solve(inputFile, outputFile);

            Process.Start(outputFile);

            Clipboard.SetText(Path.GetFullPath(outputFile));
        }

        static void Solve(string inputPath, string outputPath)
        {
            var lines = File.ReadLines(inputPath).ToList();
            var t = long.Parse(lines[0]);

            lines = lines.Skip(1).ToList();

            using (var sw = new StreamWriter(outputPath))
            {
                for (var ti = 0; ti < t; ti++)
                {
                    var (n, k) = GetTwo(lines[ti].Split().Select(long.Parse).ToList());

                    var hs = new HashSet<long> { n };
                    var c = new Dictionary<long, long> { { n, 1 } };
                    long p = 0;

                    while (true)
                    {
                        var x = hs.Max();

                        long x0;
                        long x1;

                        if (x % 2 == 0)
                        {
                            x0 = x / 2;
                            x1 = (x - 2) / 2;
                        }
                        else
                        {
                            x0 = (x - 1) / 2;
                            x1 = (x - 1) / 2;
                        }

                        p += c[x];

                        if (k <= p)
                        {
                            sw.WriteLine($"Case #{ti + 1}: {x0} {x1}");
                            break;
                        }

                        hs.Remove(x);
                        hs.Add(x0);
                        hs.Add(x1);

                        Add(c, x0, c[x]);
                        Add(c, x1, c[x]);
                    }
                }
            }
        }

        static (T a, T b) GetTwo<T>(IReadOnlyList<T> xs)
        {
            return (xs[0], xs[1]);
        }

        static void Add(IDictionary<long, long> d, long key, long valueToAdd)
        {
            if (d.ContainsKey(key))
            {
                d[key] += valueToAdd;
            }
            else
            {
                d.Add(key, valueToAdd);
            }
        }
    }
}