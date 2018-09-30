
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
            string s = "if while 2  \t  *   343+     \n 12";

            Translator t = new Translator();

            Console.WriteLine(t.Translate(s));            

            return;
        }
    }
}
