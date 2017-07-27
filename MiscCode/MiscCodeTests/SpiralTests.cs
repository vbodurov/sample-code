using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class SpiralTests
    {
        [Test]
        [TestCase(0,"0,0")]
        [TestCase(1,"0,0|1,0|1,1|0,1|-1,1|-1,0|-1,-1|0,-1|1,-1")]
        [TestCase(2,"0,0|1,0|1,1|0,1|-1,1|-1,0|-1,-1|0,-1|1,-1|2,-1|2,0|2,1|2,2|1,2|0,2|-1,2|-2,2|-2,1|-2,0|-2,-1|-2,-2|-1,-2|0,-2|1,-2|2,-2")]
        [TestCase(3,"0,0|1,0|1,1|0,1|-1,1|-1,0|-1,-1|0,-1|1,-1|2,-1|2,0|2,1|2,2|1,2|0,2|-1,2|-2,2|-2,1|-2,0|-2,-1|-2,-2|-1,-2|0,-2|1,-2|2,-2|3,-2|3,-1|3,0|3,1|3,2|3,3|2,3|1,3|0,3|-1,3|-2,3|-3,3|-3,2|-3,1|-3,0|-3,-1|-3,-2|-3,-3|-2,-3|-1,-3|0,-3|1,-3|2,-3|3,-3")]
        public void CanIterateIterator(int numberLoops, string expectedSequence)
        {
            var it = new SpiralEnumerator(numberLoops);

            var sb = new StringBuilder();
            while (it.MoveNext())
            {
                if(sb.Length > 0) sb.Append("|");
                sb.Append(it.Current.Key+","+it.Current.Value);
            }
            Assert.That(sb.ToString(), Is.EqualTo(expectedSequence));
        }

        [Test]
        public void CanIterateFunc()
        {
            SpiralAsFunc(7,7);
        }

        void SpiralAsFunc( int numX, int numY){
            int y,dx;
            var x = y = dx = 0;
            var dy = -1;
            int t = Math.Max(numX,numY);
            int maxI = t*t;
            for(int i =0; i < maxI; i++){
                if ((-numX/2 <= x) && (x <= numX/2) && (-numY/2 <= y) && (y <= numY/2)){
                    // do work here
                    
                    Console.Write("|"+x+","+y);
                }
                if( (x == y) || ((x < 0) && (x == -y)) || ((x > 0) && (x == 1-y))){
                    var temp = dx;
                    dx = -dy;
                    dy = temp;
                }
                x += dx;
                y += dy;
            }
        }

        
    }

    public struct SpiralEnumerator : IEnumerator<KeyValuePair<int,int>>
    {
        private readonly int _loops;
        private readonly int _loopsSq;
        private int _i, _x, _y, _dx, _dy;
        private KeyValuePair<int,int> _current; 
        internal SpiralEnumerator(int loops)
        {
            _loops = Math.Abs(loops)*2+1;
            _loopsSq = _loops*_loops;
            _i = _x = _y = _dx = 0;
            _dy = -1;
            _current = new KeyValuePair<int, int>(0,0);
        }
        

        public bool MoveNext()
        {
            var found = false;
            while(_i < _loopsSq)
            {
                if ((-_loops/2 <= _x) && (_x <= _loops/2) && (-_loops/2 <= _y) && (_y <= _loops/2)){
                    _current = new KeyValuePair<int,int>(_x,_y);
                    found = true;
                }
                if( (_x == _y) || ((_x < 0) && (_x == -_y)) || ((_x > 0) && (_x == 1-_y))){
                    var temp = _dx;
                    _dx = -_dy;
                    _dy = temp;
                }
                _x += _dx;
                _y += _dy;
                _i++;
                if(found)
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset() { _i = 0; _current = new KeyValuePair<int, int>(0,0); }
        public KeyValuePair<int, int> Current { get { return _current; } }
        object IEnumerator.Current { get { return Current; } }
        public void Dispose() { }
    }
}
/*
Spiral(10,10);
0;0
1;0
1;1
0;1
-1;1
-1;0
-1;-1
0;-1
1;-1
2;-1
2;0
2;1
2;2
1;2
0;2
-1;2
-2;2
-2;1
-2;0
-2;-1
-2;-2
-1;-2
0;-2
1;-2
2;-2
3;-2
3;-1
3;0
3;1
3;2
3;3
2;3
1;3
0;3
-1;3
-2;3
-3;3
-3;2
-3;1
-3;0
-3;-1
-3;-2
-3;-3
-2;-3
-1;-3
0;-3
1;-3
2;-3
3;-3
4;-3
4;-2
4;-1
4;0
4;1
4;2
4;3
4;4
3;4
2;4
1;4
0;4
-1;4
-2;4
-3;4
-4;4
-4;3
-4;2
-4;1
-4;0
-4;-1
-4;-2
-4;-3
-4;-4
-3;-4
-2;-4
-1;-4
0;-4
1;-4
2;-4
3;-4
4;-4
5;-4
5;-3
5;-2
5;-1
5;0
5;1
5;2
5;3
5;4
5;5
4;5
3;5
2;5
1;5
0;5
-1;5
-2;5
-3;5
-4;5

 */