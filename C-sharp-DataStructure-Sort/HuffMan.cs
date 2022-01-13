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
    /// 哈夫曼树
    /// </summary>
    class HuffNode<T> : BTreeNodes<T>
    {
        private int _huffNum;//频率、权重
        public int HuffNum { get => _huffNum; set => _huffNum = value; }
        public HuffNode()
        {
            Data = default;
            HuffNum = 1;
            LeftChild = null;
            RightChild = null;
        }
        public HuffNode(T data)
        {
            Data = data;
            HuffNum = 1;
            LeftChild = null;
            RightChild = null;
        }
    }
    class HuffMan<T>
    {
        public HuffNode<T> Head;
        public HuffMan(List<T> huffs) 
        {
            if (huffs == null || huffs.Count < 2)
            {
                Console.WriteLine("元素过少建立失败");
                return;
            }
            List<HuffNode<T>> sorts = new List<HuffNode<T>>();
            sorts.AddRange(from m in huffs group m by m into g select new HuffNode<T> { Data = g.Key, HuffNum = g.Count() });
            sorts = sorts.OrderBy(i => i.HuffNum).ToList();
            while (sorts.Count > 0) 
            {
                HuffNode<T> newParent = new HuffNode<T>();
                newParent.LeftChild = sorts[0];
                newParent.RightChild = sorts[1];
                newParent.HuffNum = sorts[0].HuffNum + sorts[1].HuffNum;
                sorts.RemoveAt(0);
                sorts.RemoveAt(0);
                if (sorts.Count == 0) 
                {
                    Head = newParent;
                    break;
                }
                sorts.Add(newParent);
                sorts = sorts.OrderBy(i => i.HuffNum).ToList();
            }
        }
    }
}
