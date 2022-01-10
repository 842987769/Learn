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
    /// 树
    /// </summary>
    class TreeNodes<T>
    {
        private T _data;//节点数据
        private int layer = 1;//节点层级
        private int num = 0;//节点度
        private TreeNodes<T> _parent;//父节点
        private List<TreeNodes<T>> _children;//子节点
        public enum ESearchWay { BFS, DFS }
        public T Data { get => _data; set => _data = value; }
        public List<TreeNodes<T>> Children { get => _children; set => _children = value; }
        public int Layer { get => layer; set => layer = value; }
        public int Num { get => num; set => num = value; }
        public TreeNodes<T> Parent { get => _parent; set => _parent = value; }

        public TreeNodes() { _children = new List<TreeNodes<T>>(); }
        public TreeNodes(T data)
        {
            _data = data;
            _children = new List<TreeNodes<T>>();
        }
        //增加当前节点子节点
        public virtual void AddChild(TreeNodes<T> node)
        {
            if (!_children.Contains(node))
            {
                _children.Add(node);
                node._parent = this;
                node.layer = layer + 1;
                num++;
            }
        }
        //在某处添加子节点,无返回false
        public virtual bool AddChild(T target, TreeNodes<T> node, ESearchWay s = ESearchWay.BFS)
        {
            TreeNodes<T> cur = null;
            switch (s)
            {
                case ESearchWay.BFS:
                    cur = BreadthFirstSearch(target);
                    break;
                case ESearchWay.DFS:
                    cur = DeepthFirstSearch(target);
                    break;
            }
            if (cur != null)
            {
                cur.AddChild(node);
                if (cur.Num > num) num = cur.Num;
                return true;
            }
            return false;
        }
        //删除子节点
        public virtual void RemoveChild(TreeNodes<T> node)
        {
            if (_children.Contains(node))
            {
                _children.Remove(node);
                node._parent = null;
                node.layer = 1;
                num--;
            }
        }
        //清空 
        public void RemoveAll()
        {
            foreach (TreeNodes<T> t in _children)
            {
                t._parent = null;
                t.layer = 1;
            }
            _children.Clear();
            num = 0;
        }
        //插在前面
        public void Insert(TreeNodes<T> t) 
        {
            if (_parent == null)
            {
                _parent = t;
                t.Children.Add(this);
            }
            else 
            {
                _parent.Children.Add(t);
                t.Parent = _parent;
                t.Children.Add(this);
                _parent.Children.Remove(this);
                _parent = t;
            }
            t.ReHeight();
        }
        //在树的某处插入节点,若无返回false
        public bool Insert(T target, TreeNodes<T> node, ESearchWay s = ESearchWay.BFS)
        {
            TreeNodes<T> cur = null;
            switch (s)
            {
                case ESearchWay.BFS:
                    cur = BreadthFirstSearch(target);
                    break;
                case ESearchWay.DFS:
                    cur = DeepthFirstSearch(target);
                    break;
            }
            if (cur != null)
            {
                cur.Insert(node);
                return true;
            }
            return false;
        }
        //删除某节点
        public bool Delete(T target, ESearchWay s = ESearchWay.BFS)
        {
            TreeNodes<T> cur = null;
            switch (s)
            {
                case ESearchWay.BFS:
                    cur = BreadthFirstSearch(target);
                    break;
                case ESearchWay.DFS:
                    cur = DeepthFirstSearch(target);
                    break;
            }
            if (cur != null)
            {
                foreach (TreeNodes<T> t in cur.Children)
                {
                    cur.Parent.AddChild(t);
                }
                cur.Parent.RemoveChild(cur);
            }
            cur.ReHeight();
            return true;
        }
        //广度遍历
        public TreeNodes<T> BreadthFirstSearch(T target)
        {
            Queue<TreeNodes<T>> q = new Queue<TreeNodes<T>>();
            q.Enqueue(this);
            while (q.Count != 0)
            {
                TreeNodes<T> current = q.Dequeue();
                if (current != null)
                {
                    if (current.Data.Equals(target)) return current;
                    foreach (TreeNodes<T> t in current.Children)
                    {
                        q.Enqueue(t);
                    }
                }
            }
            return null;
        }
        //深度搜索
        public TreeNodes<T> DeepthFirstSearch(T target)
        {
            Stack<TreeNodes<T>> s = new Stack<TreeNodes<T>>();
            s.Push(this);
            while (s.Count != 0)
            {
                TreeNodes<T> current = s.Pop();
                if (current != null)
                {
                    if (current.Data.Equals(target)) return current;
                    foreach (TreeNodes<T> t in current.Children)
                    {
                        s.Push(t);
                    }
                }
            }
            return null;
        }
        //遍历更新树度和节点信息,并返回树高度
        public int ReHeight()
        {
            Queue<TreeNodes<T>> q = new Queue<TreeNodes<T>>();
            q.Enqueue(this);
            int h = 1;
            int cw = 1;
            int nw = 0;
            int maxn = 0;
            int curn;
            while (q.Count != 0)
            {
                while (cw > 0 && q.Count > 0)
                {
                    TreeNodes<T> current = q.Dequeue();
                    if (current != null)
                    {
                        curn = current.Children.Count;
                        nw += current.Children.Count;
                        foreach (TreeNodes<T> t in current.Children)
                        {
                            if (t != null)
                            {
                                t.Layer = h + 1;
                            }
                            q.Enqueue(t);
                        }
                        maxn = maxn > curn ? maxn : curn;
                    }
                    cw--;
                }
                cw = nw;
                nw = 0;
                h++;
            }
            num = maxn;
            return h-2;
        }
    }
}
