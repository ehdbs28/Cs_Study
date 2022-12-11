using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {   
        struct student{
            public int id;
            public string name;
            public DateTime birthday;
        }

        static void Main(string[] args)
        {   
            //TryParse 예제
            Console.WriteLine((DateTime.TryParse(Console.ReadLine(), out DateTime date) ? $"오늘 날짜는 {date.ToString().Replace("오전 12:00:00", "")} 입니다." : "날짜형으로 변환할 수 없습니다"));


            //구조체 예제
            student s;
            s.id = 10113;
            s.name = "도윤";
            s.birthday = DateTime.Parse("2006 07 04");

            Console.WriteLine($"학번은 {s.id} 이름은 {s.name} 생일은 {s.birthday.ToString().Replace("오전 12:00:00", "")} 입니다");
        }
    }
}
