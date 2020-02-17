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
            foreach(var items in queue.OrderBy(n => n).Take(agents)){
                time += 20;
                if (items.Contains(name)) break;
            }
            
            Console.WriteLine(time);
        }
    }
}
