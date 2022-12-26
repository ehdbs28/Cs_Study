using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Student{
        public int Age {get; set;}
        public string Name {get; set;}
    }

    delegate bool IsTeenAger(Student student);
    delegate bool IsAdult(Student student);

    class Program
    {   
        delegate int Area(int a, int b);
        delegate void Line();
        delegate double CalcMethod(double a, double b);

        delegate string Concatenate(string[] strArr);

        static void Main(string[] args)
        {   
            Area square = (a, b) => a * b;
            Console.WriteLine(square(3, 4));

            Line line = () => Console.WriteLine();
            line();

            CalcMethod add = (a, b) => a + b;
            CalcMethod sub = (a, b) => a - b;
            Console.WriteLine(add(1, 2));
            Console.WriteLine(sub(2, 1));

            Concatenate concat = (strArr) => {
                string result = "";

                foreach (string str in strArr)
                    result += str;

                return result;
            };
            string[] arr = {"안", "녕하", "세요", "?"};
            Console.WriteLine(concat(arr));

            IsTeenAger isTeen = (student) => student.Age >= 13 && student.Age <= 19;
            Student s1 = new Student{Age = 17, Name = "이도윤"};
            Console.WriteLine($"{s1.Name} 은 " + ((isTeen(s1)) ? "청소년 입니다." : "청소년이 아닙니다"));
            
            IsAdult isAdult = (student) => {
                Console.WriteLine($"나이 : {student.Age}");
                return student.Age >= 20;
            };
            Student s2 = new Student{Age = 24, Name = "아저씨"};
            Console.WriteLine($"{s2.Name} 은 " + ((isAdult(s2)) ? "성인 입니다." : "성인이 아닙니다"));
        
            int[] intarr = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            int n = Count(intarr, (int i) => i % 2 == 0);
            Console.WriteLine($"짝수 갯수 : {n}개");
            n = Count(intarr, (int i) => i % 2 == 1);
            Console.WriteLine($"홀수 갯수 : {n}개");
        }

        static int Count(int[] arr, Func<int, bool> func){
            int cnt = 0;

            foreach(int i in arr){
                if(func(i)) cnt++;
            }

            return cnt;
        }
    }
}