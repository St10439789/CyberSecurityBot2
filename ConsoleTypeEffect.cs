using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CyberSecurityBot2
{
    public class ConsoleTypeEffect

    {

        //implements a typing effect of the cyber security bot 

        public void typyingeffect(string message, int delay = 50)
        {
            //The character varible 'c' checks every character stored in variable 'message' through the foreach loop
            foreach (char c in message)
            {
                Console.Write(c);// prints on the characters one at a time
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}