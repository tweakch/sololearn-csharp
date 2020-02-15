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
            
            var list = Permutate(new List<string>(),word).Distinct().ToList();
            list.Sort(); Console.WriteLine(list.IndexOf(word));
        }
        
        static List<string> Permutate(List<string> list, string word){
            if(list.Contains(word)){
                return list;
            }
            
            list.Add(word);
            
            for(var i = 0;i<word.Length-1;i++){
                var subword = word.Remove(i,1);
                var sublist = Permutate(new List<string>(),subword);
                
                foreach(var sw in sublist) {
                  list.Add($"{word[i]}{sw}");
                }
            }
            
            return list;
        }
    }
}
