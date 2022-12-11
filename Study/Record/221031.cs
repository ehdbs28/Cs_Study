using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {   
        delegate void SayDelegate();

        static void Hi(){
            Console.WriteLine("안녕하세요");
        }

        delegate int MyDelegate(int a, int b);

        class Calculator{
            public int Plus(int a, int b){
                return a + b;
            }

            public static int Minus(int a, int b){
                return a - b;
            }
        }

        static void Main(string[] args)
        {
            //delegate 예제 1번
            SayDelegate say = Hi;
            say?.Invoke();
            SayDelegate hi = new SayDelegate(Hi);
            hi?.Invoke();

            //delegate 예제 2번
            Calculator clac = new Calculator();
            MyDelegate Callback;

            Callback = clac.Plus;
            Console.WriteLine(Callback?.Invoke(3, 4));
            Callback = Calculator.Minus;
            Console.WriteLine(Callback?.Invoke(7, 5));
        }
    }
}