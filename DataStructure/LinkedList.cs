using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_sharp_DataStructure_Sort
{
    /// <summary>
    /// Author:Jigger
    /// Date:2022/1
    /// 单链表
    /// </summary>
    //单链表节点
    class LinkedNode<T>
    {
        private T _data;
        public T Data
        {
            get => _data;
            set => _data = value;
        }
        public LinkedNode<T> NextNode;
        public LinkedNode()
        {
            NextNode = null;
        }
        public LinkedNode(T data)
        {
            _data = data;
            NextNode = null;
        }
    }
    //单链表
    class LinkedList<T>
    {
        private LinkedNode<T> _head;//头节点，不计入链表长度，不作为存放数据的节点
        private LinkedNode<T> _tail;//尾节点，最后一个数据节点
        private int _length = 0;
        public int Length { get => _length; set => _length = value; }
        public LinkedNode<T> Head { get => _head; set => _head = value; }
        public LinkedNode<T> Tail { get => _tail; set => _tail = value; }

        public LinkedList() 
        {
            Head = new LinkedNode<T>();
            Tail = Head;
        }
        //构造定长单链表
        public LinkedList(int len) 
        {
            Length = len;
            int i = 0;
            Head = new LinkedNode<T>();
            LinkedNode<T> cur = Head;
            while (i < len)
            {
                cur.NextNode = new LinkedNode<T>();
                cur = cur.NextNode;
                i++;
            }
            Tail = cur;
        }
        //判空
        public bool IsEmpty() 
        {
            if (Head == Tail)
            {
                return true;
            }
            return false;
        }
        //尾插
        public void PushBack(LinkedNode<T> newNode) 
        {
            Tail.NextNode = newNode;
            Tail = newNode;
            Length++;
        }
        //头插
        public void PushFront(LinkedNode<T> newNode)
        {
            newNode.NextNode = Head.NextNode;
            Head.NextNode = newNode;
            Length++;
        }
        //尾删,并弹出(即Append方法)
        public LinkedNode<T> PopBack()
        {
            if (IsEmpty() || HasCircle() != null) 
            {
                return null;
            }
            LinkedNode<T> cur = Head;
            while (cur.NextNode != Tail) 
            {
                cur = cur.NextNode;    
            }           
            Tail = cur;
            Length--;
            return cur.NextNode;
        }
        //头删，并弹出
        public LinkedNode<T> PopFront() 
        {
            if (IsEmpty())
            {
                return null;
            }
            LinkedNode<T> cur = Head;
            Head = cur.NextNode;
            Length--;
            return cur;
        }
        //删除索引位置的节点（从0开始，0为第一个）
        public void Delete(int index)
        {
            if (index >= Length || HasCircle() != null) 
            {
                Console.WriteLine("错误！");
                return;
            }
            LinkedNode<T> pre = Head;
            int i = 0;
            while (i < index)
            {
                pre = pre.NextNode;
                i++;
            }
            pre.NextNode = pre.NextNode.NextNode;
            Length--;
        }
        //清空
        public void Clear() 
        {
            Head.NextNode = null;
            Tail = Head;
            Length = 0;
        }
        //查找
        public LinkedNode<T> Find(T data) 
        {
            if (IsEmpty() || HasCircle() != null)
            {
                return null;
            }
            LinkedNode<T> cur = Head;
            while (cur != Tail && !cur.Data.Equals(data))
            {
                cur = cur.NextNode;
            }
            if (cur.Data.Equals(data)) return cur;
            return null;
        }
        //查找索引
        public int FindIndex(T data)
        {
            int i = -1;
            if (IsEmpty() || HasCircle() != null)
            {
                return -1;
            }
            LinkedNode<T> cur = Head;
            while (cur != Tail && !cur.Data.Equals(data))
            {
                cur = cur.NextNode;
                i++;
            }
            if (cur.Data.Equals(data)) return i;
            return -1;
        }
        //查找索引处的值
        public T FindData(int index) 
        {
            if (index >= Length || HasCircle() != null)
            {
                Console.WriteLine("错误！");
                return default(T);
            }
            LinkedNode<T> pre = Head;
            int i = -1;
            while (i < index)
            {
                pre = pre.NextNode;
                i++;
            }
            return pre.Data;
        }
        //索引插入
        public void Insert(int index, LinkedNode<T> newNode)
        {
            if (index >= Length || HasCircle() != null)
            {
                Console.WriteLine("越界！");
                return;
            }
            LinkedNode<T> pre = Head;
            int i = -1;
            while (i < index-1)
            {
                pre = pre.NextNode;
                i++;
            }
            newNode.NextNode = pre.NextNode;
            pre.NextNode = newNode;
            Length++;
        }
        //节点插入
        public bool Insert(LinkedNode<T> insertNode, LinkedNode<T> newNode) 
        {
            if (IsEmpty() || HasCircle() != null)
            {
                return false;
            }
            LinkedNode<T> cur = Head;
            while (cur.NextNode != Tail && cur.NextNode != insertNode)
            {
                cur = cur.NextNode;
            }
            if (cur == insertNode)
            {
                newNode.NextNode = cur.NextNode;
                cur.NextNode = newNode;
                return true;
            }
            Length++;
            return false;
        }
        //反转链表
        public void Reverse() 
        {
            LinkedNode<T> p = Head.NextNode, q = null, r = Head;
            Tail = Head.NextNode;
            Head = Head.NextNode;
            while (p != null) 
            {
                p = p.NextNode;
                Head.NextNode = q;
                q = Head;
                Head = p;
            }
            Head = r;
            Head.NextNode = q;
        }
        //判断是否有环并返回入口节点
        public LinkedNode<T> HasCircle() 
        {
            List<LinkedNode<T>> tag = new();
            tag.Add(Head);
            LinkedNode<T> cur = Head;
            while (cur != null && cur.NextNode != null) 
            {
                if (tag.Contains(cur.NextNode)) 
                {
                    return cur;
                }
                tag.Add(cur.NextNode);
                cur = cur.NextNode;
            }
            return null;
        }
    }
}
