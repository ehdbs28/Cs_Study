using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {   
        delegate int Comparer<T>(T a, T b);

        static int AscendCompare<T>(T a, T b) where T : IComparable{
            return a.CompareTo(b);
        }

        static int DescendCompare<T>(T a, T b) where T : IComparable{
            return a.CompareTo(b) * -1;
        }

        static void BubbleSort<T>(T[] array, Comparer<T> comparer){
            for(int i = 0; i < array.Length; i++){
                for(int j = 0; j < array.Length - (i + 1); j++){
                    if(comparer(array[j], array[j + 1]) > 0){
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            foreach(T i in array){
                Console.Write($"{i} ");
            }
            Console.Write("\n");
        }

        static void Main(string[] args)
        {
            // int[] array = {5, 3, 7, 9, 1};
            // BubbleSort<int>(array, AscendCompare);
            // int[] array2 = {6, 4, 8, 2 , 10};
            // BubbleSort(array2, DescendCompare);

            // char[] charArr = {'b', 'c', 'd', 'a', 'f', 'e', 'g'};
            // BubbleSort<char>(charArr, AscendCompare);
            // BubbleSort<char>(charArr, DescendCompare);

            int[] intArr = {5, 3, 4, 6, 1};
            string[] strArr = {"가나다", "마바사", "아자차"};

            BubbleSort<int>(intArr, AscendCompare);
            BubbleSort<string>(strArr, DescendCompare);
        }
    }
}