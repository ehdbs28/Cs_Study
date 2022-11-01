using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {   
        delegate int Comparer(int a, int b);

        static int AscendCompare(int a, int b){
            if(a > b) return 1;
            else if(a == b) return 0;
            else return -1;
        }

        static int DescendCompare(int a, int b){
            if(a < b) return 1;
            else if(a == b) return 0;
            else return -1;
        }

        static void BubbleSort(int[] array, Comparer comparer){
            for(int i = 0; i < array.Length; i++){
                for(int j = 0; j < array.Length - (i + 1); j++){
                    if(comparer(array[j], array[j + 1]) > 0){
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            foreach(int i in array){
                Console.Write($"{i} ");
            }
            Console.Write("\n");
        }

        static void Main(string[] args)
        {
            int[] array = {5, 3, 7, 9, 1};
            BubbleSort(array, AscendCompare);
            int[] array2 = {6, 4, 8, 2 , 10};
            BubbleSort(array2, DescendCompare);
        }
    }
}