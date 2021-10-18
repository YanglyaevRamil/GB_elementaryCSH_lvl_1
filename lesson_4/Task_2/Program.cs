using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:

            // а) для целых чисел;
            List<int> list = new List<int>();
            Dictionary<int, int> frequencyList0 = new Dictionary<int, int>();

            list.AddRange(new int[] { 20, 1, -1, 2, 9, 2, 4, 16, 20, 2 });

            // Способ 1
            list.ForEach(a => frequencyList0[a] = frequencyList0.ContainsKey(a) ? ++frequencyList0[a] : 1);

            // Способ 2
            //list.ForEach(a => 
            //{
            //    if (frequencyList0.ContainsKey(a))
            //    {
            //        frequencyList0[a]++;
            //    }
            //    else
            //    {
            //        frequencyList0[a] = 1;
            //    }
            //}
            //);

            // Способ 3
            //list.ForEach(delegate (int a)
            //{
            //    if (frequencyList0.ContainsKey(a))
            //    {
            //        frequencyList0[a]++;
            //    }
            //    else
            //    {
            //        frequencyList0[a] = 1;
            //    }
            //}
            //);

            foreach (KeyValuePair<int, int> entry in frequencyList0)
            {
                Console.WriteLine($"{entry.Key} - {entry.Value}");
            }
            Console.WriteLine("-----------------------------");

            // б) *для НЕобобщенной коллекции;
            ArrayList arrayList = new ArrayList();
            Dictionary<object, int> frequencyList1 = new Dictionary<object, int>();
            arrayList.AddRange(new object[] { "sss", 1, -1, 2, 9, 2, 4, 16, "sss", 2, "124wdq1", new Random(1) });

            list.ForEach(a => frequencyList1[a] = frequencyList1.ContainsKey(a) ? ++frequencyList1[a] : 1);

            foreach (KeyValuePair<object, int> entry in frequencyList1)
            {
                Console.WriteLine($"{entry.Key} - {entry.Value}");
            }
            
            Console.WriteLine("-----------------------------");

            // в) *используя Linq.

            var uniqLinq = frequencyList1.GroupBy(p => p).Select(g => new {g.Key});

            foreach (var entry in uniqLinq)
            {
                Console.WriteLine($"{entry.Key.Key} - {entry.Key.Value}");
            }

            Console.ReadLine();
        }
    }
}
