
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorNS
{
    class Program
    {       
        
        static void Main(string[] args)
        {

            string[] prog = System.IO.File.ReadAllLines(@"prog.txt");
            
           Translator t = new Translator();

           for (int i = 0; i < prog.Length; i++)
           {
               try
               {
                   Console.WriteLine(t.Translate(prog[i].ToLower()));                       
               }
               catch (Exception)
               {
                   Console.WriteLine("Error in Line {0}", i+1);
                   throw;
               }
              
           }


           

            return;
        }
    }
}
