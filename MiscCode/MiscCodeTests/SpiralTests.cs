using System;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class SpiralTests
    {
        [Test]
        public void CanIterate()
        {
            Spiral(10,10);
        }

        void Spiral( int numX, int numY){
            int y,dx;
            var x = y = dx = 0;
            var dy = -1;
            int t = Math.Max(numX,numY);
            int maxI = t*t;
            for(int i =0; i < maxI; i++){
                if ((-numX/2 <= x) && (x <= numX/2) && (-numY/2 <= y) && (y <= numY/2)){
                    // do work here
                    Console.WriteLine(x+";"+y);
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