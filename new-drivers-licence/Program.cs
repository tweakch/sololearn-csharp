using System;
using System.Collections.Generic;
using System.Linq;

namespace new_drivers_licence
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = Console.ReadLine();
            var agents = Convert.ToInt32(Console.ReadLine());
            var queue = Console.ReadLine().Split(" ").ToList();
            
            var time = 0;

            queue.Add(name);
            queue.Sort();

            foreach (var items in queue.Chunk(agents))
            {
                time += 20;
                if (items.Contains(name)) break;
            }

            Console.WriteLine(time);
        }
    }

    static class ListExtensions
    {
        ///<summary>
        /// Break a list of items into chunks of a specific size
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            while (source.Any())
            {
                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }
    }
}
