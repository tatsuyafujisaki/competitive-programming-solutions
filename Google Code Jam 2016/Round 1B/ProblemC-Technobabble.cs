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

            var lines = File.ReadLines(inputFile).ToList();
            var t = long.Parse(lines[0]);

            lines = lines.Skip(1).ToList();

            var result = new List<string>();
            var firsts = new HashSet<string>();
            var seconds = new HashSet<string>();
            var edges = new Dictionary<string, HashSet<string>>();

            for (var ti = 0; ti < t; ti++)
            {
                firsts.Clear();
                seconds.Clear();
                edges.Clear();

                var n = int.Parse(lines[0]);

                for (var i = 0; i < n; i++)
                {
                    var xs = lines[i + 1].Split();

                    var first = xs[0];
                    var second = xs[1];

                    firsts.Add(first);
                    seconds.Add(second);

                    if (edges.ContainsKey(first))
                    {
                        edges[first].Add(second);
                    }
                    else
                    {
                        edges.Add(first, new HashSet<string> { second });
                    }
                }

                result.Add($"Case #{ti + 1}: {n - (firsts.Count + ModifiedHopcroftKarp(firsts, seconds, edges))}");

                lines = lines.Skip(1 + n).ToList();
            }

            File.WriteAllLines(outputFile, result);
        }

        private static bool HasAugmentingPath(IEnumerable<string> lefts,
                                              IReadOnlyDictionary<string, HashSet<string>> edges,
                                              IReadOnlyDictionary<string, string> toMatchedRight,
                                              IReadOnlyDictionary<string, string> toMatchedLeft,
                                              IDictionary<string, long> distances,
                                              Queue<string> q)
        {
            foreach (var left in lefts)
            {
                if (toMatchedRight[left] == "")
                {
                    distances[left] = 0;
                    q.Enqueue(left);
                }
                else
                {
                    distances[left] = long.MaxValue;
                }
            }

            distances[""] = long.MaxValue;

            while (0 < q.Count)
            {
                var left = q.Dequeue();

                if (distances[left] < distances[""])
                {
                    foreach (var right in edges[left])
                    {
                        var nextLeft = toMatchedLeft[right];
                        if (distances[nextLeft] == long.MaxValue)
                        {
                            distances[nextLeft] = distances[left] + 1;
                            q.Enqueue(nextLeft);
                        }
                    }
                }
            }

            return distances[""] != long.MaxValue;
        }

        private static bool TryMatching(string left,
                                        IReadOnlyDictionary<string, HashSet<string>> edges,
                                        IDictionary<string, string> toMatchedRight,
                                        IDictionary<string, string> toMatchedLeft,
                                        IDictionary<string, long> distances)
        {
            if (left == "")
            {
                return true;
            }

            foreach (var right in edges[left])
            {
                var nextLeft = toMatchedLeft[right];
                if (distances[nextLeft] == distances[left] + 1)
                {
                    if (TryMatching(nextLeft, edges, toMatchedRight, toMatchedLeft, distances))
                    {
                        toMatchedLeft[right] = left;
                        toMatchedRight[left] = right;
                        return true;
                    }
                }
            }

            distances[left] = long.MaxValue;

            return false;
        }

        private static int ModifiedHopcroftKarp(HashSet<string> lefts,
                                                IEnumerable<string> rights,
                                                IReadOnlyDictionary<string, HashSet<string>> edges)
        {
            var distances = new Dictionary<string, long>();
            var q = new Queue<string>();

            var toMatchedRight = lefts.ToDictionary(s => s, s => "");
            var toMatchedLeft = rights.ToDictionary(s => s, s => "");

            while (HasAugmentingPath(lefts, edges, toMatchedRight, toMatchedLeft, distances, q))
            {
                foreach (var unmatchedLeft in lefts.Where(left => toMatchedRight[left] == ""))
                {
                    TryMatching(unmatchedLeft, edges, toMatchedRight, toMatchedLeft, distances);
                }
            }

            return toMatchedLeft.Count(kvp => kvp.Value == "");
        }
    }
}
