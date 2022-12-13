using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {   
        delegate bool MemberTest(int a);

        static int Count(int[] arr, MemberTest memberTest){
            int cnt = 0;
            foreach(int i in arr){
                if(memberTest(i)) cnt++;
            }
            return cnt;
        }

        delegate void EventHandler(string str);
        class MyNotifier{
            public event EventHandler SomthingHappened;
            public void Dosomething(int number){
                if(number != 0 && number % 3 == 0){
                    SomthingHappened($"{number} : 3의 배수\n");
                }
            }
        }

        static void MyHandler(string str){
            Console.Write(str);
        }

        static void Main(string[] args)
        {   
            int[] arr = {1, 2, 3, 4, 5};
            int even = Count(arr, delegate(int a){
                return a % 2 == 0;
            });
            int odd = Count(arr, delegate(int a){
                return a % 2 == 1;
            });

            Console.WriteLine($"홀수 : {odd}개\n짝수 : {even}개");

            MyNotifier notifier = new MyNotifier();
            notifier.SomthingHappened += MyHandler;
            for(int i = 0; i < 31; i++){
                notifier.Dosomething(i);
            }
        }
    }
}