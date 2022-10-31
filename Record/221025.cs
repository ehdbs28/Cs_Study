using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {   
        static void Main(string[] args)
        {  
            //예제 1
            int[] arr = {1, 2, 3};

            try{
                Console.WriteLine(arr[100]);
            }
            catch{
                Console.WriteLine("오류가 발생하였습니다");
            }

            //예제 2
            try{
                int x, y;
                Console.Write("X 값 입력 : "); x = int.Parse(Console.ReadLine());
                Console.Write("Y 값 입력 : "); y = int.Parse(Console.ReadLine());

                Console.WriteLine(x / y);
            }
            catch(DivideByZeroException e){
                Console.WriteLine($"예외가 발생하였습니다. {e.Message}");
            }
            catch(Exception e){
                Console.WriteLine($"예외가 발생하였습니다. {e.Message}");
            }

            //예제 3
            try{
                DoSomething(3);
                DoSomething(13);
                DoSomething(5);
            }
            catch(Exception e){
                Console.WriteLine($"예외가 발생하였습니다. {e.Message}");
            }

            //예제 4
            try{
                int divisor, dividend;
                Console.Write("divisor : "); divisor = int.Parse(Console.ReadLine());
                Console.Write("dividend : "); dividend = int.Parse(Console.ReadLine());

                Console.WriteLine(Divide(divisor, dividend));
            }
            catch(FormatException e){
                Console.WriteLine(e);
            }
            catch(DivideByZeroException e){
                Console.WriteLine(e);
            }
            finally{
                Console.WriteLine("프로그램 종료");
            }
        }

        static int Divide(int divisor, int dividend){
            try{
                return divisor / dividend;
            }
            catch(DivideByZeroException e){
                Console.WriteLine("예외가 발생함");
                throw e;
            }
            finally{
                Console.WriteLine("Divide() 끝");
            }
        }

        static void DoSomething(int n){
            if(n <= 10){
                Console.WriteLine(n);
            }
            else throw new Exception("n이 10보다 큽니다.");
        }
    }
}