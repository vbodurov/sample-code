using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingProblemsTests.Structures
{
    public class HeapOfInt
    {
        readonly Func<int, int, int> _compare;
        int _size = 0;
        readonly List<int> _elements = new List<int>(Enumerable.Range(0, 128).Select(i => 0));
        public HeapOfInt(Func<int, int, int> compare)
        {
            _compare = compare;
        }
        public int Count => _size;
        public int Peek()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            return _elements[0];
        }

        public int Pop()
        {
            if (_size == 0)
                throw new IndexOutOfRangeException();

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            HeapifyDown();

            return result;
        }

        public void Add(int element)
        {
            if (_size >= _elements.Count)
                throw new IndexOutOfRangeException();

            _elements[_size] = element;
            _size++;

            HeapifyUp();
        }
        public override string ToString()
        {
            return string.Join(",", _elements);
        }
        int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        bool IsRoot(int elementIndex) => elementIndex == 0;

        int GetLeftChild(int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        int GetRightChild(int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        int GetParent(int elementIndex) => _elements[GetParentIndex(elementIndex)];

        void Swap(int firstIndex, int secondIndex)
        {
            var temp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = temp;
        }
        void HeapifyDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && _compare(GetRightChild(index), GetLeftChild(index)) < 0)
                {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (_compare(_elements[smallerIndex], _elements[index]) >= 0)
                {
                    break;
                }

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        void HeapifyUp()
        {
            var index = _size - 1;
            while (!IsRoot(index) && _compare(_elements[index], GetParent(index)) < 0)
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
    }
}