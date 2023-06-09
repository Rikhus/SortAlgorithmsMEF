﻿using Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    [Export(typeof(ISort))]
    public class QuickSort: ISort
    {
        [ImportingConstructor]
        public QuickSort() 
        {
        }

        public string Name => "QuickSort";

        public string Description => "Алгоритм быстрой сортировки";


        public IEnumerator<List<int>> Sort(List<int> array)
        {
            var list = array.ToList();
            var enumerator = Sort(list, 0, list.Count - 1);
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
            yield return enumerator.Current;
        }        

        //быстрая сортировка
        IEnumerator<List<int>> Sort(List<int> array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                yield return array;
            }
            else
            {
                var pivot = minIndex - 1;
                for (var i = minIndex; i < maxIndex; i++)
                {
                    if (array[i] < array[maxIndex])
                    {
                        pivot++;
                        (array[i], array[pivot]) = (array[pivot], array[i]);
                        yield return array;
                    }
                }

                pivot++;
                (array[maxIndex], array[pivot]) = (array[pivot], array[maxIndex]);
                yield return array;
                var pivotIndex = pivot;
                var enumerator = Sort(array, minIndex, pivotIndex - 1);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
                enumerator = Sort(array, pivotIndex + 1, maxIndex);
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }

                yield return array;
            }            
        }


    }
}
