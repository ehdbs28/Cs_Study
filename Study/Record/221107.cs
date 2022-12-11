using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study{
    class Program{
        delegate void SendString(string str);
        delegate int Comparer(int a, int b);

        static void Hello(string name){
            Console.WriteLine($"안녕하세요 {name}씨");
        }
        static void GoodBye(string name){
            Console.WriteLine($"안녕히가세요 {name}씨");
        }

        static void Main(string[] args){
            //delegate 예제 1
            SendString SayHello = Hello;
            SendString SayGoodBye = GoodBye;

            SendString MultipleDelegate;
            MultipleDelegate = SayHello + SayGoodBye;

            MultipleDelegate?.Invoke("도윤");

            MultipleDelegate -= SayGoodBye;
            MultipleDelegate?.Invoke("도윤");

            //delegate 예제 2(익명 메서드)
            int[] arr = {3, 2, 4, 5, 1};
            BubbleSort(arr, delegate(int a, int b){
                if(a > b) return 1;
                else return 0;
            });

            BubbleSort(arr, delegate(int a, int b){
                if(a < b) return 1;
                else return 0;
            });
        }

        static void BubbleSort(int[] arr, Comparer comparer){
            for(int i = 0; i < arr.Length; i++){
                for(int j = 0; j < arr.Length - (i + 1); j++){
                    if(comparer(arr[j], arr[j + 1]) > 0){
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }

            foreach(int i in arr){
                Console.Write($"{i} ");
            }
            Console.Write("\n");
        }
    }
}