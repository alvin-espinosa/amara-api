using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_app
{
    public class Test
    {

    }

    public class Animal
    {
        private int age { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string getName()
        {
            return this.Name + "-hey";
        }

        private Animal()
        {
            Console.WriteLine("Default Constructor Invoked");
        }

        //public Animal()
        //{
        //    Console.WriteLine("Default Constructor Invoked");
        //}

        static Animal()
        {
            Console.WriteLine("Static Constructor Invoked");
        }

        public static void print()
        {
            Console.WriteLine("Static Print method called");
        }

        public int testMethod(out int c, ref int d)
        {
            c = 10;
            d = d + 2;
            return c + d;
        }
    }

    public class Dog
    {

        public string getName(string hi)
        {
            //var test = new Employee();


            //return this.Name + hi;
            return "";
        }
    }

    public class Fist
    {
        public Fist()
        {
            //this.age = 3;
        }
    }

    public abstract class Employee
    {
        public Employee()
        {
            
        }

        public string getName()
        {
            var person = new Person();
            person.Name = "test";
            person.getName();
            return "hey";
        }
    }

    struct Person
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public string getName()
        {
            return "";
        }
    }
}
