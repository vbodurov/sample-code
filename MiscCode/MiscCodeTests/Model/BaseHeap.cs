using System.Collections.Generic;
using System.Linq;

namespace MiscCodeTests.Model
{
    public class MaxHeap : BaseHeap { protected override bool Compare(int a, int b) { return a > b; } }
    public class MinHeap : BaseHeap { protected override bool Compare(int a, int b) { return a < b; } }

    public abstract class BaseHeap
    {
        private readonly List<int> _array = new List<int>(128);
        private int _heapSize = -1;

        protected abstract bool Compare(int a, int b);

        public virtual void Insert(int key)
        {
            _heapSize++;
            if (_array.Count <= _heapSize)
            {
                _array.AddRange(
                    Enumerable.Range(0, _heapSize - (_array.Count - 1))
                              .Select(i => 0));
            }
            _array[_heapSize] = key;
            Heapify(_heapSize);
        }
        public virtual int GetTop()
        {
            int max = -1;

            if (_heapSize >= 0)
            {
                max = _array[0];
            }

            return max;
        }
        public virtual int ExtractTop()
        {
            var del = -1;

            if (_heapSize > -1)
            {
                del = _array[0];

                Swap(_array, 0, _heapSize);
                _heapSize--;
                Heapify(GetParent(_heapSize + 1));
            }

            return del;
        }
        public virtual int GetCount()
        {
            return _heapSize + 1;
        }
        public override string ToString()
        {
            return "{" + string.Join(";", _array) + "}";
        }


        private static void Swap(IList<int> array, int aIndex, int bIndex )
        {
            var aux = array[aIndex];
            array[aIndex] = array[bIndex];
            array[bIndex] = aux;
        }

// if you need those...
//        private int GetLeft(int i) { return 2 * i + 1; }
//        private int GetRight(int i) { return 2 * (i + 1); }

        private int GetParent(int i)
        {
            if( i <= 0 )
            {
                return -1;
            }
 
            return (i - 1)/2;
        }

        private void Heapify(int i)
        {
            int p = GetParent(i);
 
            if( p >= 0 && Compare(_array[i], _array[p]) )
            {
                Swap(_array, i, p);
                Heapify(p);
            }
        }

    }
}