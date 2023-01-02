using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 예제 1 Func
            Action act1;
            act1 = () => Console.WriteLine("Action()");
            act1.Invoke();

            int result = 0;
            Action<int> act2;
            act2 = (x) => result = x * x;

            act2.Invoke(3);
            Console.WriteLine("Result : {0}", result);

            Action<float, float> act3;
            act3 = (x, y) => Console.WriteLine(x / y);
            act3.Invoke(9, 3);
            #endregion
        
            #region 예제 2 Predicate1
            Predicate<int> isOdd;
            isOdd = (x) => x % 2 == 1;
            Console.WriteLine(isOdd(3));

            Predicate<string> isLowerCase;
            isLowerCase = (str) => string.Equals(str, str.ToLower());
            Console.WriteLine(isLowerCase(Console.ReadLine()));
            #endregion

            #region 예제 3 Predicate 홀짝 프로그램
            int Count(int[] param, Predicate<int> fomula){
                int cnt = 0;

                foreach(int arg in param){
                    if(fomula(arg)) cnt++;
                }

                return cnt;
            }

            int[] arr = {1, 2, 3, 4, 5};

            //짝수
            Console.WriteLine($"배열 속 짝수의 갯수는 {Count(arr, (x) => x % 2 == 0)}개");
            //홀수
            Console.WriteLine($"배열 속 짝수의 갯수는 {Count(arr, (x) => x % 2 == 1)}개");
            #endregion

            #region 예제 4 Predicate 리스트
            List<string> myList = new List<string>{"cat", "rabbit", "tiger", "elephant", "zebra", "lion", "snake"};
            bool exist = myList.Exists((s) => s.Contains("s"));
            Console.WriteLine(exist);

            string name = myList.Find((s) => s.Length == 3);
            Console.WriteLine(name);

            List<string> longName = myList.FindAll((s) => s.Length >= 6);
            longName.ForEach((s) => Console.WriteLine(s));
            #endregion
        }
    }
}