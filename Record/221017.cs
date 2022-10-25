using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program{
        abstract class Shape{
            public abstract float GetArea();
        }

        class Square : Shape
        {
            float size;

            public Square(float size){
                this.size = size;
            }

            public override float GetArea()
            {
                return (size * size);
            }
        }

        class Triangle : Shape
        {
            float size;

            public Triangle(float size){
                this.size = size;
            }

            public override float GetArea()
            {
                return ((size * size) / 2);
            }
        }

        abstract class Product{
            static int serial = 0;
            public string Serial_ID{
                get => String.Format("0") + ++serial;
            }

            abstract public DateTime ProductDate{get; set;}
        }

        class MyProduct : Product{
            public override DateTime ProductDate { get; set; }
            public MyProduct(DateTime dateTime){
                ProductDate = dateTime;
            }

        }

        interface INamedValue{
            public string Name{get; set;}
            public string Value{get; set;}
        }

        class NamedValue : INamedValue
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public NamedValue(string name, string value){
                Name = name;
                Value = value;
            }
        }

        static void Main(string[] args){
            //추상 클래스 예제 1
            Console.Write("Set Square Size : ");
            Square squ = new Square(float.Parse(Console.ReadLine()));
            Console.Write("Set Triangle Size : ");
            Triangle tri = new Triangle(float.Parse(Console.ReadLine()));

            Console.WriteLine($"SquareArea : {squ.GetArea()}\nTriangleArea : {tri.GetArea()}");

            //추상 클래스 예제 2
            MyProduct myProduct = new MyProduct(DateTime.Now.Date);

            Console.WriteLine($"Serial_ID : {myProduct.Serial_ID}\nProductDate : {myProduct.ProductDate}".Replace("오전 12:00:00", ""));

            //추상 클래스 예제3
            NamedValue name = new NamedValue("이름", "이도윤");
            NamedValue height = new NamedValue("키", "173");
            NamedValue weight = new NamedValue("몸무게", "1400");

            Console.WriteLine($"{name.Name} {name.Value}\n{height.Name} {height.Value}\n{weight.Name} {weight.Value}");
        }
    }

    
}