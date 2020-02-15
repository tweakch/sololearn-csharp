using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SoloLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            var word = Console.ReadLine();
            var list = Permute(word).Distinct().ToList();
            list.Sort(); 
            Console.WriteLine(list.IndexOf(word));
        }
        static List<string> Permute(string word)
        {
            var p = new List<string>();

            if(word.Length == 1)  {
                p.Add(word);
            } else {
                for(int i = 0; i < word.Length; i++) {
                    foreach(var sw in Permute(word.Remove(i,1))){
                        p.Add(word[i] + sw);        
                    }
                }
            }
            return p;
        }
    }
}
