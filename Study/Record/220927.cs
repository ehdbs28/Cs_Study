using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Study
{
    class Program
    {   
        struct student{
            public int id;
            public string name;
            public DateTime birthday;

            public void Print(){
                Console.WriteLine("- - -");
                Console.WriteLine($"id : {this.id}\nname : {this.name}\nbirthday : {this.birthday}");
                Console.WriteLine("- - -\n");
            }
        }

        struct Member{
            public string name;
            public int age;
        }

        static void Print(string name, int age){
            Console.WriteLine($"name : {name}, age : {age}");
        }

        static void Print(Member m){
            Console.WriteLine($"name : {m.name}, age : {m.age}");
        }

        static void Main(string[] args){
            //student 구조체
            student[] students = new student[3];

            int[] ids = new int[3] {1, 2, 3};
            string[] names = new string[3] {"도윤", "민채", "지은"};
            string[] birthdays = new string[3] {"2006 07 04", "2006 08 17", "2006 06 09"};

            for(int i = 0; i < students.Length; i++){
                students[i].id = ids[i];
                students[i].name = names[i];
                students[i].birthday = DateTime.Parse(birthdays[i]);
            }

            foreach(student s in students){
                s.Print();
            }

            //member 구조체
            string name = "홍길동";
            int age = 17;
            Print(name, age);

            Member member;
            member.name = "이도윤";
            member.age = 17;
            Print(member);

            //TimeSpan 예제
            Console.WriteLine(Math.Abs((int)(Convert.ToDateTime(Console.ReadLine()) - DateTime.Today).TotalDays));
        }
    }
        
}
