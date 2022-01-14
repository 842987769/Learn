using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;

namespace C_sharp_DataStructure_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------树2----------");
            TreeNodes<int> t3 = new TreeNodes<int>(0);
            t3.AddChild(new TreeNodes<int>(1));
            t3.AddChild(new TreeNodes<int>(2));
            t3.AddChild(new TreeNodes<int>(3));
            t3.Insert(1, new TreeNodes<int>(4));
            Console.WriteLine("度：" + t3.Num);
            t3.Insert(4, new TreeNodes<int>(5));
            t3.Insert(5, new TreeNodes<int>(6));
            Console.WriteLine("度：" + t3.Num);
            foreach (Nodes<int> t in t3.BreadthFirstSearch())
            {
                Console.Write("宽：" + t.Data + "\t");
            }
            t3.Delete(3);
            t3.Delete(2);
            Console.WriteLine();
            Console.WriteLine("度：" + t3.Num);
            foreach (Nodes<int> t in t3.BreadthFirstSearch())
            {
                Console.Write("宽：" + t.Data + "\t");
            }
            Console.WriteLine();
            Console.WriteLine("--------二叉树1.1-------");
            BTreeNodes<int> t4 = new BTreeNodes<int>(0);
            t4.LeftChild = new BTreeNodes<int>(1);
            t4.RightChild = new BTreeNodes<int>(2);
            t4.LeftChild.RightChild = new BTreeNodes<int>(4);
            t4.PreOrder(t4);
            Console.WriteLine();
            t4.PreOrder();
            Console.WriteLine();
            t4.InOrder();
            Console.WriteLine();
            t4.InOrder(t4);
            Console.WriteLine();
            t4.PostOrder();
            Console.WriteLine();
            t4.PostOrder(t4);
            Console.WriteLine();
            t4.InOrder(t4);
            Console.WriteLine();
            t4.LevelOrder();
            Console.WriteLine();
            Console.WriteLine("---------跳表--------");
            SkipList<int> s = new SkipList<int>();
            s.Add(5);
            s.Add(8);
            s.Add(3);
            s.Add(9);
            s.Remove(3);
            s.Add(-1);
            s.Add(-2);
            s.Find(8);
            Console.WriteLine("--------哈夫曼树-------");
            HuffMan<String> t5 = new HuffMan<string>(new List<string>() { "A","B","D","E","A","A","D","B","D","A"});
            t5.Head.PreOrder();
            Console.WriteLine();
            t5.Head.InOrder();
            Console.WriteLine();
            t5.Head.PostOrder();
        }
    }
}
