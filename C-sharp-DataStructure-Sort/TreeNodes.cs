using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_sharp_DataStructure_Sort
{
    abstract class Nodes<T>
    {
        private T _data;//节点数据
        private int layer = 1;//节点层级
        private int num = 0;//节点度
        public T Data { get => _data; set => _data = value; }
        public int Layer { get => layer; set => layer = value; }
        public int Num { get => num; set => num = value; }
        public abstract Nodes<T> GetChild(int i);

        public Nodes() { }
        public Nodes(T data)
        {
            _data = data;
        }
        //广度遍历迭代器
        public IEnumerable<Nodes<T>> BreadthFirstSearch()
        {
            Queue<Nodes<T>> q = new Queue<Nodes<T>>();
            q.Enqueue(this);
            while (q.Count != 0)
            {
                Nodes<T> current = q.Dequeue();
                if (current != null)
                {
                    yield return current;
                    int i = 0;
                    Nodes<T> t = current.GetChild(i);
                    while (t != null)
                    {
                        q.Enqueue(t);
                        i++;
                        t = current.GetChild(i);
                    }
                }
            }
        }
        //深度搜索迭代器
        public IEnumerable<Nodes<T>> DeepthFirstSearch()
        {
            Stack<Nodes<T>> s = new Stack<Nodes<T>>();
            s.Push(this);
            while (s.Count != 0)
            {
                Nodes<T> current = s.Pop();
                if (current != null)
                {
                    yield return current;
                    int i = 0;
                    Nodes<T> t = current.GetChild(i);
                    while(t != null)
                    {
                        s.Push(t);
                        i++;
                        t = current.GetChild(i);
                    }
                }
            }
        }
    }

    class TreeNodes<T>:Nodes<T>
    {
        private TreeNodes<T> _parent;//父节点
        private List<TreeNodes<T>> _children;//子节点
        public enum ESearchWay { BFS, DFS }
        public List<TreeNodes<T>> Children { get => _children; set => _children = value; }
        public TreeNodes<T> Parent { get => _parent; set => _parent = value; }
        public TreeNodes() 
        {
            _children = new List<TreeNodes<T>>(); 
        }
        public TreeNodes(T data)
        {
            Data = data;
            _children = new List<TreeNodes<T>>();
        }
        public override Nodes<T> GetChild(int i)
        {
            return _children.Count <= i ? null : _children[i];
        }
        //增加当前节点子节点
        public virtual void AddChild(TreeNodes<T> node)
        {
            if (!_children.Contains(node))
            {
                _children.Add(node);
                node.Parent = this;
                node.Layer = Layer + 1;
                Num++;
            }
        }
        //在某处添加子节点,无返回false
        public virtual bool AddChild(T target, TreeNodes<T> node, ESearchWay s = ESearchWay.BFS)
        {
            TreeNodes<T> cur = null;
            IEnumerable i = null;
            switch (s)
            {
                case ESearchWay.BFS:
                    i = BreadthFirstSearch();
                    break;
                case ESearchWay.DFS:
                    i = DeepthFirstSearch();
                    break;
            }
            foreach(Nodes<T> t in i) 
            {
                if (t.Data.Equals(target))
                {
                    cur = (TreeNodes<T>)t;
                }
            }
            if (cur != null)
            {
                cur.AddChild(node);
                if (cur.Num > Num) Num = cur.Num;
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
                node.Parent = null;
                node.Layer = 1;
                Num--;
            }
        }
        //清空 
        public void RemoveAll()
        {
            foreach (TreeNodes<T> t in _children)
            {
                t.Parent = null;
                t.Layer = 1;
            }
            _children.Clear();
            Num = 0;
        }
        //插在前面
        public void Insert(TreeNodes<T> t) 
        {
            if (Parent == null)
            {
                Parent = t;
                t.Children.Add(this);
            }
            else 
            {
                Parent.Children.Add(t);
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
            IEnumerable i = null;
            switch (s)
            {
                case ESearchWay.BFS:
                    i = BreadthFirstSearch();
                    break;
                case ESearchWay.DFS:
                    i = DeepthFirstSearch();
                    break;
            }
            foreach (Nodes<T> t in i)
            {
                if (t.Data.Equals(target))
                {
                    cur = (TreeNodes<T>)t;
                }
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
            IEnumerable i = null;
            switch (s)
            {
                case ESearchWay.BFS:
                    i = BreadthFirstSearch();
                    break;
                case ESearchWay.DFS:
                    i = DeepthFirstSearch();
                    break;
            }
            foreach (Nodes<T> t in i)
            {
                if (t.Data.Equals(target))
                {
                    cur = (TreeNodes<T>)t;
                }
            }
            if (cur != null)
            {
                foreach (TreeNodes<T> t in cur.Children)
                {
                    cur.Parent.AddChild(t);
                }
                cur.Parent.RemoveChild(cur);
            }
            else 
            {
                return true;
            }
            cur.ReHeight();
            return true;
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
            Num = maxn;
            return h-2;
        }
    }
}
