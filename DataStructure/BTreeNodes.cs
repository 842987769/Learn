using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_sharp_DataStructure_Sort
{
    class BTreeNodes<T>:TreeNodes<T>
    {
        private const int MaxNum = 2;//二叉树最大度为2
        public TreeNodes<T> BinaryRoot { get => this; }
        public BTreeNodes() 
        {
            Children = new List<TreeNodes<T>>(2)
            {
                null,
                null
            };
            Num = 2;
        }
        public BTreeNodes(T root)
        { 
            Data = root;
            Children = new List<TreeNodes<T>>(2)
            {
                null,
                null
            };
            Num = 2;
        }
        public BTreeNodes<T> LeftChild 
        {
            get => (BTreeNodes<T>)Children[0];
            set => Children[0] = value;
        }
        public BTreeNodes<T> RightChild
        {
            get => (BTreeNodes<T>)Children[1];
            set => Children[1] = value;
        }
        //增加当前节点子节点
        public override void AddChild(TreeNodes<T> node)
        {
            if (!Children.Contains(node))
            {
                if (LeftChild != null)
                {
                    LeftChild = (BTreeNodes<T>)node;
                }
                else if (RightChild != null)
                {
                    RightChild = (BTreeNodes<T>)node;
                }
                else 
                {
                    Console.WriteLine("该分支已满！");
                    return;
                }
                node.Parent = this;
                node.Layer = Layer + 1;
            }
        }
        //在某处添加子节点,无返回false
        public override bool AddChild(T target, TreeNodes<T> node, ESearchWay s = ESearchWay.BFS)
        {
            BTreeNodes<T> cur = null;
            switch (s)
            {
                case ESearchWay.BFS:
                    cur = (BTreeNodes<T>)BreadthFirstSearch(target);
                    break;
                case ESearchWay.DFS:
                    cur = (BTreeNodes<T>)DeepthFirstSearch(target);
                    break;
            }
            if (cur != null)
            {
                if (cur.LeftChild != null)
                {
                    LeftChild = (BTreeNodes<T>)node;
                }
                else if (RightChild != null)
                {
                    RightChild = (BTreeNodes<T>)node;
                }
                else
                {
                    Console.WriteLine("该分支已满！");
                    return false;
                }
            }
            return false;
        }
        public override void RemoveChild(TreeNodes<T> node)
        {
            if (Children[0] == node)
            {
                Children[0] = null;
            }
            else if (Children[1] == node)
            {
                Children[1] = null;
            }
            else 
            {
                Console.WriteLine("没有该子节点！");
                return;
            }
            node.Parent = null;
            node.Layer = 1;
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
            Stack<BTreeNodes<T>> s = new Stack<BTreeNodes<T>>();
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
            Stack<BTreeNodes<T>> s = new Stack<BTreeNodes<T>>();
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
            Stack<BTreeNodes<T>> s = new Stack<BTreeNodes<T>>();
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
            Queue<BTreeNodes<T>> q = new Queue<BTreeNodes<T>>();
            q.Enqueue(this);
            int n1 = 1;
            int n2 = 1;
            int h = 1;
            while (q.Count > 0) {
                n2 = n1;
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
