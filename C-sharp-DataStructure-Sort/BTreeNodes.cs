using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_sharp_DataStructure_Sort
{
    /// <summary>
    /// Author:Jigger
    /// Date:2022/1
    /// 二叉树
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class BTreeNodes<T>:Nodes<T>
    {
        public Nodes<T> BinaryRoot { get => this; }
        private BTreeNodes<T> _leftNode;
        private BTreeNodes<T> _rightNode;
        public BTreeNodes() 
        {
            _leftNode = null;
            _rightNode = null;
            Num = 2;
        }
        public BTreeNodes(T root)
        { 
            Data = root;
            _leftNode = null;
            _rightNode = null;
            Num = 2;
        }
        public BTreeNodes<T> LeftChild 
        {
            get => _leftNode;
            set => _leftNode = value;
        }
        public BTreeNodes<T> RightChild
        {
            get => _rightNode;
            set => _rightNode = value;
        }
        public override Nodes<T> GetChild(int i)
        {
            return i switch
            {
                0 => LeftChild,
                1 => RightChild,
                _ => null,
            };
        }
        //先序遍历-递归
        public void PreOrder(BTreeNodes<T> node)
        {
            if (node == null) return;
            Console.Write("先：" + node.Data + "\t");
            PreOrder(node.LeftChild);
            PreOrder(node.RightChild);
        }
        //先序遍历-非递归（深搜）
        public void PreOrder()
        {
            Stack<BTreeNodes<T>> s = new();
            s.Push(this);
            while (s.Count > 0)
            {
                BTreeNodes<T> node = s.Pop();
                if (node != null)
                {
                    Console.Write("先：" + node.Data + "\t");
                    s.Push(node.RightChild);
                    s.Push(node.LeftChild);
                }
            }
        }
        //中序遍历-递归
        public void InOrder(BTreeNodes<T> node)
        {
            if (node == null) return;
            InOrder(node.LeftChild);
            Console.Write("中：" + node.Data + "\t");
            InOrder(node.RightChild);
        }
        //中序遍历-非递归
        public void InOrder()
        {
            Stack<BTreeNodes<T>> s = new();
            BTreeNodes<T> node = this;
            while (node != null || s.Count != 0)
            {
                if (node != null)
                {
                    s.Push(node);
                    node = node.LeftChild;
                }
                else
                {
                    BTreeNodes<T> cur = s.Pop();
                    Console.Write("中：" + cur.Data + "\t");
                    node = cur.RightChild;
                }

            }
        }
        //后序遍历-递归
        public void PostOrder(BTreeNodes<T> node)
        {
            if (node == null) return;
            PostOrder(node.LeftChild);
            PostOrder(node.RightChild);
            Console.Write("后：" + node.Data + "\t");
        }
        //后序遍历-非递归-单栈
        public void PostOrder()
        {
            Stack<BTreeNodes<T>> s = new();
            BTreeNodes<T> node = this;
            BTreeNodes<T> pre = null;
            while (node != null || s.Count != 0)
            {
                while (node != null)
                {
                    s.Push(node);
                    node = node.LeftChild;
                }
                if (s.Count != 0)
                {
                    node = s.Pop();
                    BTreeNodes<T> loc = node.RightChild;
                    if (loc == null || pre == loc)
                    {
                        Console.Write("后：" + node.Data + "\t");
                        pre = node;
                        node = null;
                    }
                    else
                    {
                        s.Push(node);
                        node = node.RightChild;
                    }
                }
            }
        }
        //层序遍历
        public void LevelOrder()
        {
            Queue<BTreeNodes<T>> q = new();
            q.Enqueue(this);
            int n1 = 1;
            int h = 1;
            while (q.Count > 0) {
                int n2 = n1;
                while (n2 > 0)
                {
                    BTreeNodes<T> cur = q.Dequeue();
                    if (cur != null)
                    {
                        Console.Write("层" + h + ":" + cur.Data + "\t");
                        q.Enqueue(cur.LeftChild);
                        q.Enqueue(cur.RightChild);
                        n1 += 1;
                    }
                    else 
                    {
                        n1--;
                    }
                    n2--;
                }
                h++;
            }
        }
    }
}
