
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

           string prog = System.IO.File.ReadAllText(@"prog.txt");
           string[] statements = prog.Split(';');
 
           Translator t = new Translator();

           for (int i = 0; i < statements.Length; i++)
           {
               try
               {
                   Console.WriteLine(t.Translate(statements[i].ToLower()));                       
               }
               catch (Exception e)
               {
                   Console.WriteLine("Line {0} ({1})", i+1,e.Message);
                   
               }
              
           }


           

            return;
        }
    }
}
