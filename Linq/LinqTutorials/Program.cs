using System;
using System.Collections.Generic;
using System.Linq;
using LinqTutorials.Models;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            var first = LinqTasks.Task1();
            Show(first);


            var second = LinqTasks.Task2();
            Show(second);

            var third = LinqTasks.Task3();
            Console.WriteLine(third);
            Separator();

            var fourth = LinqTasks.Task4();
            Show(fourth);

            var fifth = LinqTasks.Task5();
            Show(fifth);

            var sixth = LinqTasks.Task6();
            Show(sixth);

            var seventh = LinqTasks.Task7();
            Show(seventh);

            var eight = LinqTasks.Task8();
            Console.WriteLine(eight);
            Separator();

            var ninth = LinqTasks.Task9();
            Console.WriteLine(ninth);
            Separator();

            var tenth = LinqTasks.Task10();
            Show(tenth);

            var eleventh = LinqTasks.Task11();
            Show(eleventh);
            
            var twelve = LinqTasks.Task12();
            Show(twelve);

            var thirteenth = LinqTasks.Task13(new int[]{1,1,2,3,3});
            foreach (var t in thirteenth)
            {
                Console.WriteLine(t);
            }
            Separator();
            
            var fourteenth =  LinqTasks.Task14();
            Show(fourteenth);


        }

        private static void Show(IEnumerable<Object> list )
        {
            foreach (var l in list)
            {
                Console.WriteLine(l);
            }
            Console.WriteLine("--------------------------------------------------------");
        }

        private static void Separator()
        {
            Console.WriteLine("--------------------------------------------------------");
        }
    }
}
