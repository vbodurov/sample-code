using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingProblemsTests.Structures
{
    public class MinHeap<T> : BaseHeap<T> where T : IComparable<T>
    {
        public MinHeap() : base(true) { }
    }
    public class MaxHeap<T> : BaseHeap<T> where T : IComparable<T>
    {
        public MaxHeap() : base(false) { }
    }
    public abstract class BaseHeap<T> where T : IComparable<T>
    {
        readonly SortedDictionary<T, Queue<T>> _dict;
        protected BaseHeap(bool isMin)
        {
            _dict = isMin 
                ? new SortedDictionary<T, Queue<T>>()
                : new SortedDictionary<T, Queue<T>>(Comparer<T>.Create((a,b)=> -a.CompareTo(b)))
                ;
        }
        public int Count { get; private set; }
        public void Add(T i)
        {
            if (!_dict.TryGetValue(i, out var queue))
                _dict[i] = queue = new Queue<T>();

            ++Count;
            queue.Enqueue(i);
        }
        public T Pop()
        {
            if (_dict.Count == 0) throw new InvalidOperationException();
            var kvp = _dict.First();
            var val = kvp.Value.Dequeue();
            if (kvp.Value.Count == 0) _dict.Remove(kvp.Key);
            --Count;
            return val;
        }
        public T Peek()
        {
            if (_dict.Count == 0) throw new InvalidOperationException();
            var kvp = _dict.First();
            var val = kvp.Value.Peek();
            --Count;
            return val;
        }
    }
}