using System;
using System.Collections.Generic;
using System.Linq;

namespace GCJ
{
    static class Program
    {
        static void Main()
        {
            Solve();
        }

        static void Solve()
        {
            var testCaseCount = int.Parse(Console.ReadLine());

            for (var testCaseId = 1; testCaseId <= testCaseCount; testCaseId++)
            {
                var n = int.Parse(Console.ReadLine());

                var flavorOccurrences = new List<int>();

                for (int i = 0; i < n; i++)
                {
                    flavorOccurrences.Add(0);
                }

                var remainingFlavors = Enumerable.Range(0, n).ToList();

                for (int i = 0; i < n; i++)
                {
                    var tokens = Console.ReadLine().Split().Select(s => int.Parse(s)).ToList();

                    if (tokens[0] == 0)
                    {
                        Console.WriteLine(-1);
                        continue;
                    }

                    var hisFavoriteFlavors = tokens.Skip(1).ToList();

                    foreach (var hisFavoriteFlavor in hisFavoriteFlavors)
                    {
                        flavorOccurrences[hisFavoriteFlavor] += 1;
                    }

                    var sellableFlavors = remainingFlavors.Intersect(hisFavoriteFlavors).ToList();

                    if (sellableFlavors.Any())
                    {
                        var flavor = GetLeastPopularFlavor(sellableFlavors, flavorOccurrences);
                        Console.WriteLine(flavor);
                        remainingFlavors = remainingFlavors.Where(x => x != flavor).ToList();
                    }
                    else
                    {
                        Console.WriteLine(-1);
                    }
                }
            }
        }

        static int GetLeastPopularFlavor(List<int> sellableFlavors, List<int> flavorOccurrences)
        {
            var min = flavorOccurrences[sellableFlavors[0]];
            var indexOfMin = 0;

            var n = sellableFlavors.Count;
            for (var i = 1; i < n; i++)
            {
                var occurrence = flavorOccurrences[sellableFlavors[i]];
                if (occurrence < min)
                {
                    min = occurrence;
                    indexOfMin = i;
                }
            }

            return sellableFlavors[indexOfMin];
        }
    }
}