using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {   
        class MyNotifier{
            public delegate void EventHandler(string st);
            public event EventHandler SomthingHappened;

            public void Dosomething(int num){
                if(num % 3 == 0){
                    SomthingHappened($"{num} : 3의 배수");
                }
            }
        }

        class Button{
            public delegate void EventHandler2();
            public event EventHandler2 Click;

            public void OnClick(){
                Click?.Invoke();
            }
        }

        static void Main(string[] args)
        {   
            // MyNotifier notifier = new MyNotifier();

            // notifier.SomthingHappened += MyHandler;

            // for(int i = 0; i <= 30; i++)
            //     notifier.Dosomething(i);

            Button btn = new Button();
            btn.Click += Hi1;
            btn.Click += Hi2;
            btn.OnClick();
            //btn.Click.Invoke(); //<- Event는 선언 한 클래스에서만 호출 가능하다.
        }

        static void MyHandler(string st){
            System.Console.WriteLine(st);
        }

        static void Hi1(){
            System.Console.WriteLine("C#");
        }

        static void Hi2(){
            System.Console.WriteLine(".NET");
        }
    }
}