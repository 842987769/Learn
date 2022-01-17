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
    /// 排序
    /// </summary>
    /// <typeparam name="T">比较元素</typeparam>
    static class MySort<T> where T:IComparable
    {
        /// <summary>
        /// 交换
        /// </summary>
        /// <param name="List">待排列表</param>
        /// <param name="Lo">左边界</param>
        /// <param name="Hi">右边界</param>
        private static void Swap(T[] List, int Lo, int Hi)
       {
            T Temp = List[Lo];
            List[Lo] = List[Hi];
            List[Hi] = Temp;
        }

        /// <summary>
        /// 选排
        /// </summary>
        /// <param name="List">待排列表</param>
        /// <param name="Lo">左边界</param>
        /// <param name="Hi">右边界</param>
        /// <param name="comparer">比较器</param>
        public static void SelectedSort(ref T[] List, int Lo, int Hi, IComparer<T> comparer)
        {
            while (Hi >= Lo)
            {
                int pMax = Lo;
                for (int pNow = Lo; pNow <= Hi; pNow++)
                {
                    if (comparer.Compare(List[pNow], List[pMax]) > 0)
                    {
                        pMax = pNow;
                    }
                }
                Swap(List, pMax, Hi);
                --Hi;//Hi指针往Lo指针移动
            }
        }
        public static void SelectedSort(ref T[] List, int Lo, int Hi)
        {
            SelectedSort(ref List, Lo, Hi, Comparer<T>.Default);
        }
        public static void SelectedSort(ref T[] List)
        {
            SelectedSort(ref List, List.GetLowerBound(0), List.GetUpperBound(0));
        }

        /// <summary>
        /// 快排
        /// </summary>
        /// <param name="List">待排列表</param>
        /// <param name="Lo">左边界</param>
        /// <param name="Hi">右边界</param>
        /// <param name="comparer">比较器</param>
        public static void QuickSort(T[] List, int Lo, int Hi, IComparer<T> comparer)
        {
            if (Hi <= Lo) return;

            if ((Hi - Lo) <= 8)
            {
                SelectedSort(ref List, Lo, Hi, comparer);
            }
            else
            {
                int pLo = Lo;
                int pHi = Hi;
                T vPivot = List[Lo + (Hi - Lo) >> 1];//标志
                while (pLo <= pHi)
                {
                    //扫描左半区间的元素是否小于标志
                    while (pLo < Hi && (comparer.Compare(List[pLo], vPivot) < 0)) ++pLo;
                    //扫描右半区间的元素是否大于标志
                    while (pHi > Lo && (comparer.Compare(vPivot, List[pHi]) < 0)) --pHi;
                    if (pLo <= pHi)
                    {
                        Swap(List, pLo, pHi);
                        ++pLo;
                        --pHi;
                    }
                }
                if (Lo < pHi)
                {
                    QuickSort(List, Lo, pHi, comparer);//左半区间递归((pHi --> Hi))
                }
                if (pLo < Hi)
                {
                    QuickSort(List, pLo, Hi, comparer);//右半区间递归((Lo --> pLo))
                }
            }
        }
        public static void QuickSort(T[] List, int Lo, int Hi)
        {
            QuickSort(List, Lo, Hi, Comparer<T>.Default);
        }
        public static void QuickSort(T[] List)
        {
            QuickSort(List, List.GetLowerBound(0), List.GetUpperBound(0));
        }

        /// <summary>
        /// 堆排序
        /// </summary>
        /// <param name="List">待排列表</param>
        public static void HeapSort(T[] List)
        {
            BuildMaxHeap(List);
            for (int i = List.Length - 1; i > 0; i--)
            {
                Swap(List, 0, i);//堆顶与最后交换
                MaxHeapify(List, 0, i);
            }
        }
        /// <summary>
        /// 创建大顶堆
        /// </summary>
        /// <param name="List">待排列表</param>
        private static void BuildMaxHeap(T[] List)
        {
            for (int i = List.Length / 2 - 1; i >= 0; i--)
            {
                MaxHeapify(List, i, List.Length);
            }
        }
        /// <summary>
        /// 大顶堆调整过程
        /// </summary>
        /// <param name="list">待排列表</param>
        /// <param name="i">待调整节点索引</param>
        /// <param name="length">元素个数</param>
        private static void MaxHeapify(T[] list, int i, int length)
        {
            int left = i * 2 + 1;//左节点索引
            int right = i * 2 + 2;//右节点索引
            int max = i;//最大值索引
            if (left < length && list[left].CompareTo(list[max]) > 0) 
            {
                max = left;
            }
            if (right < length && list[right].CompareTo(list[max]) > 0)
            {
                max = right;
            }
            if (i != max)
            {
                Swap(list, i, max);
                MaxHeapify(list, max, length);
            }
        }
    }
}
