using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {
        class Developer : IPerson
        {
            public void Work()
            {
                Console.WriteLine("개발을 합니다.");
            }
        }

        class Art : IPerson
        {
            public void Work()
            {
                Console.WriteLine("도트 찍습니다.");
            }
        }

        class ProjectManager : IPerson
        {
            public void Work()
            {
                Console.WriteLine("기획합니다.");
            }
        }

        static void Main(string[] args)
        {
            // Guid guid = new Guid();
            // string unique = guid.ToString();
            // Console.WriteLine(unique);

            new Developer().Work();
            new Art().Work();
            new ProjectManager().Work();
        }
    }
}