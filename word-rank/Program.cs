using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;

namespace SoloLearn
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "ConsoleArgs";
            app.Description = ".NET Core console app with argument parsing.";

            app.HelpOption("-?|-h|--help");

            var wordOption = app.Option("-w|--word=<WORD>", "The word to rank", CommandOptionType.SingleValue);
            var sortOption = app.Option("-s|--sort", "Sort the result", CommandOptionType.NoValue);
            var distOption = app.Option("-d|--distinct", "Distinct values", CommandOptionType.NoValue);
            var fileOption = app.Option("-o|--output", "Output file", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                string word;
                if (wordOption.HasValue())
                {
                    word = wordOption.Value();
                }
                else
                {
                    Console.Write("Enter a word: ");
                    word = Console.ReadLine();
                }
                var list = word.GetPermutations().Select(c => new string(c.ToArray())).ToList();

                if (distOption.HasValue())
                {
                    list = list.Distinct().ToList();
                }

                if (sortOption.HasValue())
                {
                    list.Sort();
                }

                if (fileOption.HasValue())
                {
                    File.WriteAllLines(fileOption.Value(), list);
                }

                Console.WriteLine(list.IndexOf(word) + 1);
                Console.WriteLine("word-rank has finished.");
                return 0;
            });


            app.Execute(args);
            await Task.CompletedTask;
        }
    }

    public static class WordRankExtensions
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();

            var factorials = Enumerable.Range(0, array.Length + 1)
                .Select(Factorial)
                .ToArray();

            for (var i = 0L; i < factorials[array.Length]; i++)
            {
                var sequence = GenerateSequence(i, array.Length - 1, factorials);
                yield return GeneratePermutation(array, sequence);
            }
        }

        private static IEnumerable<T> GeneratePermutation<T>(T[] array, IReadOnlyList<int> sequence)
        {
            var clone = (T[])array.Clone();

            for (int i = 0; i < clone.Length - 1; i++)
            {
                Swap(ref clone[i], ref clone[i + sequence[i]]);
            }

            return clone;
        }

        private static int[] GenerateSequence(long number, int size, IReadOnlyList<long> factorials)
        {
            var sequence = new int[size];

            for (var j = 0; j < sequence.Length; j++)
            {
                var facto = factorials[sequence.Length - j];

                sequence[j] = (int)(number / facto);
                number = (int)(number % facto);
            }

            return sequence;
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        private static long Factorial(int n)
        {
            long result = n;

            for (int i = 1; i < n; i++)
            {
                result = result * i;
            }

            return result;
        }
    }

}
