using System;
using System.Collections.Generic;
using System.Linq;

namespace Gcj
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
                var tokens = Console.ReadLine().Split();

                var N = int.Parse(tokens[0]);
                var L = int.Parse(tokens[1]);

                var css = new List<HashSet<char>>(L);

                for (int i = 0; i < L; i++)
                {
                    css.Add(new HashSet<char>());
                }

                var inputWords = new List<string>();

                for (int i = 0; i < N; i++)
                {
                    var inputWord = Console.ReadLine();
                    inputWords.Add(inputWord);

                    var cs = inputWord.ToCharArray();

                    for (var j = 0; j < L; j++)
                    {
                        css[j].Add(cs[j]);
                    }
                }

                var foundNewWord = false;

                foreach (var word in CreateWords(new[] { "" }, css))
                {
                    if (!inputWords.Contains(word))
                    {
                        Console.WriteLine($"Case #{testCaseId}: {word}");
                        foundNewWord = true;
                        break;
                    }
                }

                if (!foundNewWord)
                {
                    Console.WriteLine($"Case #{testCaseId}: -");
                }
            }
        }

        static IEnumerable<string> CreateWords(IEnumerable<string> words, IEnumerable<IEnumerable<char>> listOfCandidateChars)
        {
            if (listOfCandidateChars.Any())
            {
                var candidateChars = listOfCandidateChars.First();
                listOfCandidateChars = listOfCandidateChars.Skip(1);

                return candidateChars.SelectMany(candidateChar => CreateWords(words.Select(word => word + candidateChar), listOfCandidateChars));
            }

            return words;
        }
    }
}