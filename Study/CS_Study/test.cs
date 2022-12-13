using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {   
        static void Main(string[] args)
        {   
            Random rand = new Random();
            Console.WriteLine(rand.Next(10, 17));
        }
    }
}