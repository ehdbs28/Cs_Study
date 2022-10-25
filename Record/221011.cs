using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CS_Study
{
    class Program
    {
        // class Good : IBattery
        // {
        //     public string GetName()
        //     {
        //         return "Good";
        //     }
        // }

        // class Bad : IBattery
        // {
        //     public string GetName()
        //     {
        //         return "Bad";
        //     }
        // }

        // class Car{
        //     private IBattery _battery;

        //     public Car(IBattery battery){
        //         _battery = battery;
        //     }

        //     public void Run(){
        //         Console.WriteLine(_battery.GetName());
        //     }
        // }

        // class Dog : IAnimal, IDog
        // {
        //     public void Eat()
        //     {
        //         Console.WriteLine("먹다");
        //     }

        //     public void Yelp()
        //     {
        //         Console.WriteLine("짖다");
        //     }
        // }

        class Product : IComparable
        {
            public string Name {get; set;}
            public float Price {get; set;}

            public override string ToString()
            {
                return $"{Name} : {Price} 원";
            }

            public Product(string name, float price){
                this.Name = name;
                this.Price = price;
            }

            public int CompareTo(object obj)
            {
                return this.Price.CompareTo((obj as Product).Price);
            }
        }

        static void Main(string[] args)
        {   
            // Car car1 = new Car(new Good());
            // Car car2 = new Car(new Bad());

            // car1.Run();
            // car2.Run();

            // Dog dog = new Dog();
            // dog.Eat();
            // dog.Yelp();

            List<Product> list = new List<Product>() {new Product("고구마", 1500), new Product("사과", 2400), new Product("바나나", 1000), new Product("배", 3000)};
            list.Sort();

            foreach(Product p in list){
                Console.WriteLine(p.ToString());
            }
        }
    }
}