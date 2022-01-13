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
    /// 双向链表
    /// </summary>
    //双向链表节点
    class DoublyLinkedNode<T>
    {
        private T _data;
        public T Data
        {
            get => _data;
            set => _data = value;
        }
        public DoublyLinkedNode<T> NextNode;//后继
        public DoublyLinkedNode<T> PreNode;//前驱
        public DoublyLinkedNode()
        {
            NextNode = null;
            PreNode = null;
        }
        public DoublyLinkedNode(T data)
        {
            _data = data;
            NextNode = null;
            PreNode = null;
        }
    }
    //双向链表
    class DoublyLinkedList<T>
    {
        private DoublyLinkedNode<T> _head;//头节点，不计入链表长度，不作为存放数据的节点
        private int _length = 0;

        public DoublyLinkedNode<T> Head { get => _head; set => _head = value; } 
        public int Length { get => _length; }

        public DoublyLinkedList()
        {
            Head = new DoublyLinkedNode<T>();
        }
        //构造定长双链表
        public DoublyLinkedList(int len)
        {
            _length = len;
            int i = 0;
            Head = new DoublyLinkedNode<T>();
            DoublyLinkedNode<T> pre = Head, cur = Head;
            while (i < len)
            {
                cur.NextNode = new DoublyLinkedNode<T>();
                cur = cur.NextNode;
                cur.PreNode = pre;
                pre = cur;
                i++;
            }
        }
        //判空
        public bool IsEmpty()
        {
            if (Head.NextNode == null)
            {
                return true;
            }
            return false;
        }
        //尾插
        public void PushBack(DoublyLinkedNode<T> newNode)
        {
            if (HasCircle() != null)
            {
                return;
            }
            DoublyLinkedNode<T> cur = Head;
            while (cur.NextNode != null)
            {
                cur = cur.NextNode;
            }
            cur.NextNode = newNode;
            newNode.PreNode = cur;
            _length++;
        }
        //头插
        public void PushFront(DoublyLinkedNode<T> newNode)
        {
            newNode.NextNode = Head.NextNode;
            Head.NextNode.PreNode = newNode;
            newNode.PreNode = Head;
            Head.NextNode = newNode;
            _length++;
        }
        //尾删,并弹出(即Append方法)
        public DoublyLinkedNode<T> PopBack()
        {
            if (IsEmpty() || HasCircle() != null)
            {
                return null;
            }
            DoublyLinkedNode<T> cur = Head;
            while (cur.NextNode != null)
            {
                cur = cur.NextNode;
            }
            cur.PreNode.NextNode = null;
            return cur;
        }
        //头删，并弹出
        public DoublyLinkedNode<T> PopFront()
        {
            if (IsEmpty())
            {
                return null;
            }
            DoublyLinkedNode<T> cur = Head.NextNode;
            Head.NextNode = cur.NextNode;
            cur.NextNode.PreNode = Head;
            return cur;
        }
        //删除索引位置的节点（从0开始，0为第一个）
        public void Delete(int index)
        {
            if (IsEmpty() || HasCircle() != null)
            {
                return;
            }
            DoublyLinkedNode<T> pre = Head;
            int i = 0;
            while (i < index)
            {
                pre = pre.NextNode;
                i++;
            }
            pre.NextNode = pre.NextNode.NextNode;
            pre.NextNode.PreNode = pre;
            _length--;
        }
        //清空
        public void Clear()
        {
            Head.NextNode = null;
            _length = 0;
        }
        //查找
        public DoublyLinkedNode<T> Find(T data) 
        {
            if (IsEmpty() || HasCircle() != null)
            {
                return null;
            }
            DoublyLinkedNode<T> cur = Head;
            while (cur.NextNode != null && !cur.Data.Equals(data))
            {
                cur = cur.NextNode;
            }
            if (cur.Data.Equals(data)) 
            {
                return cur;
            }
            return null;
        }
        //查找索引
        public int FindIndex(T data)
        {
            if (IsEmpty() || HasCircle() != null)
            {
                return -1;
            }
            DoublyLinkedNode<T> cur = Head;
            int i = -1;
            while (cur.NextNode != null && !cur.Data.Equals(data))
            {
                cur = cur.NextNode;
                i++;
            }
            if (cur.Data.Equals(data))
            {
                return i;
            }
            return -1;
        }
        //查找索引处的值
        public T FindData(int index) 
        {
            if (index >= Length || HasCircle() != null)
            {
                return default(T);
            }
            DoublyLinkedNode<T> pre = Head;
            int i = -1;
            while (i < index)
            {
                pre = pre.NextNode;
                i++;
            }
            return pre.Data;
        }
        //索引插入
        public void Insert(int index, DoublyLinkedNode<T> newNode) 
        {
            if (index >= Length || HasCircle() != null)
            {
                return;
            }
            DoublyLinkedNode<T> pre = Head;
            int i = -1;
            while (i < index-1)
            {
                pre = pre.NextNode;
                i++;
            }
            newNode.PreNode = pre;
            newNode.NextNode = pre.NextNode;
            newNode.NextNode.PreNode = newNode;
            pre.NextNode = newNode;
            _length++;
        }
        //节点位置插入
        public bool Insert(DoublyLinkedNode<T> insertNode, DoublyLinkedNode<T> newNode)
        {
            if (IsEmpty() || HasCircle() != null)
            {
                return false;
            }
            DoublyLinkedNode<T> cur = Head;
            while (cur.NextNode != null && cur.NextNode != insertNode)
            {
                cur = cur.NextNode;
            }
            if (cur.NextNode == insertNode)
            {
                newNode.NextNode = cur.NextNode;
                newNode.PreNode = cur;
                cur.NextNode.PreNode = newNode;
                cur.NextNode = newNode;
                _length++;
                return true;
            }
            return false;
        }
        //反转双向链表
        public void Reverse() 
        {
            DoublyLinkedNode<T> p = Head.NextNode, q = null, r = Head;
            Head = Head.NextNode;
            while (p != null)
            {
                p = p.NextNode;
                Head.NextNode = q;
                Head.PreNode = p;
                q = Head;
                Head = p;
            }
            Head = r;
            Head.NextNode = q;
            q.PreNode = Head;
        }
        //是否有环并返回入口节点,双向链表需要两次检查，一次向后，一次向前
        public DoublyLinkedNode<T> HasCircle()
        {
            if (IsEmpty())
            {
                return null;
            }
            DoublyLinkedNode<T> slow = Head, fast = Head.NextNode;
            while (slow != fast && fast.NextNode != null && fast.NextNode.NextNode != null)
            {
                slow = slow.NextNode;
                fast = fast.NextNode.NextNode;
            }
            if (fast.NextNode != null && fast.NextNode.NextNode != null)
            {
                slow = Head;
                while (slow != fast)
                {
                    slow = slow.NextNode;
                    fast = fast.NextNode;
                }
                return slow;
            }
            slow = fast;
            if (fast.NextNode.NextNode == null) 
            {
                slow = fast.NextNode;
            }
            DoublyLinkedNode<T> tail = slow;
            fast = slow.PreNode;
            while (slow != fast && fast.PreNode != null && fast.PreNode.PreNode != null)
            {
                slow = slow.PreNode;
                fast = fast.PreNode;
            }
            if (fast.PreNode != null && fast.PreNode.PreNode != null)
            {
                slow = tail;
                while (slow != fast)
                {
                    slow = slow.PreNode;
                    fast = fast.PreNode;
                }
                return slow;
            }
            return null;
        }
    }
}
