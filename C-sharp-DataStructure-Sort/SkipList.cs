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
    /// 跳表
    /// </summary>
    //跳表节点
    class SkipNode<T> where T : IComparable
    {
        private T _data;
        private int _level = 0;

        public int Level//节点抽出的索引数量
        {
            get => _level;
            set => _level = value;
        }
        public SkipNode<T>[] Skips;//索引表，0处索引为下一节点
        public T Data
        {
            get => _data;
            set => _data = value;
        }
        public SkipNode(int level = 0)
        {
            Skips = new SkipNode<T>[level];
        }
        public SkipNode(T data, int level = 0)
        {
            _data = data;
            Skips = new SkipNode<T>[level];
        }
    }
    //跳表
    class SkipList<T> where T : IComparable
    {
        private SkipNode<T> _head;
        private int _level = 1;
        private double _skipLimit;
        public SkipNode<T> Head //头，不进入跳表排序；
        {
            get => _head;
            set => _head = value;
        }
        public int Level//节点抽出的索引数量
        {
            get => _level;
            set => _level = value;
        }
        public double SkipLimit //抽出索引限制，越大索引越细
        {
            get => _skipLimit;
            set => _skipLimit = value;
        }
        public int MaxLevel = 16; //索引最大层数
        public SkipList(double skipLimit = 0.5) 
        {
            Head = new(MaxLevel);
            Level = 1;
            _skipLimit = skipLimit;
            MaxLevel = 16;
        }
        //随机抽出索引函数
        public int RandomLevel() 
        {
            int level = 1;
            Random r = new();
            while (r.NextDouble() < _skipLimit && level < MaxLevel) 
            {
                level += 1;
            }
            return level;
        }
        //判空
        public bool IsEmpty() 
        {
            return Head == null;
        }
        //查找
        public SkipNode<T> Find(T data)
        {
            SkipNode<T> cur = Head;
            if (data.CompareTo(cur.Data) < 0)
            {
                return null;
            }
            int l = cur.Level;
            while (cur != null && l >= 0)
            {
                if (cur.Data.CompareTo(data) == 0)
                {
                    return cur;
                }
                else if (cur.Skips[l].Data.CompareTo(data) < 0)
                {
                    cur = cur.Skips[l];
                    Console.WriteLine(cur.Data);
                }
                else
                {
                    l--;
                }
            }
            return null;
        }
        //增加值
        public void Add(T data)
        {
            SkipNode<T> cur = Head;
            int l = RandomLevel();//记录节点最终的索引高度
            SkipNode<T> newNode = new(data, MaxLevel);
            newNode.Level = l;
            SkipNode<T>[] updata = new SkipNode<T>[l];//用于查找每一层上，上一个小于增加值的节点索引或节点值
            for (int i = 0; i < l; ++i)
            {
                updata[i] = Head;
            }
            for (int i = l - 1; i > -1; --i)
            {
                while (cur.Skips[i] != null && cur.Skips[i].Data.CompareTo(data) < 0)
                {
                    cur = cur.Skips[i];
                }
                updata[i] = cur;
            }
            for (int i = 0; i < l; i++)
            {
                newNode.Skips[i] = updata[i].Skips[i];
                updata[i].Skips[i] = newNode;//将得到的表与新节点的表做链接
            }
            if (Level < l) 
            {
                Level = l;//更新跳表索引高度
            }
        }
        //删除值
        public void Remove(T data)
        {
            //开头同插入，先记录每层第一个小于该节点的索引或值
            SkipNode<T>[] updata = new SkipNode<T>[Level];
            SkipNode<T> cur = Head;
            for (int i = Level - 1; i >= 0; i--) 
            {
                while (cur.Skips[i] != null && cur.Skips[i].Data.CompareTo(data) < 0)
                {
                    cur = cur.Skips[i];
                }
                updata[i] = cur;
            }
            if (cur.Skips[0] != null && cur.Skips[0].Data.Equals(data))
            {
                for (int i = Level - 1; i >= 0; i--)
                {
                    if (updata[i].Skips[i] != null && updata[i].Skips[i].Data.Equals(data))
                    {
                        updata[i].Skips[i] = updata[i].Skips[i].Skips[i];//跳过要删除的节点从而达到删除目的
                    }
                }
            }
            while (Level > 1 && Head.Skips[Level] == null) //验证删除后的索引高是否为最大高度，不是则下降
            {
                Level--;
            }
        }
    }
}
