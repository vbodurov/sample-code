using System;
using NUnit.Framework;
using UnityEngine;

namespace MiscCodeTests
{
    [TestFixture]
    public class ProjectionTests
    {
        [Test]
        public void CanTransform()
        {
            var arr = new []
            {
                new Vector2(-180,-90f),
                new Vector2(180,90f),
                new Vector2(0,0),
                new Vector2(45,45),
                new Vector2(-45,-45)
            };

            foreach (var angles in arr)
            {
                var point = FromLatLongToPoint(angles);
                var backDeg = FromPointToLatLong(point);
                Console.WriteLine(angles.x+", "+angles.y+" = "+Math.Round(point.x,3)+", "+Math.Round(point.y,3)
                    +" ("+Math.Round(backDeg.x,5)+", "+Math.Round(backDeg.y,5)+")");

                
            }
        }


        // Web Mercator
        private static Vector2 FromLatLongToPoint(Vector2 angles)
        {
            var degX = angles.x;
            var degY = angles.y*0.94501254f;
            var x = (degX + 180) / 360;
            var y = (float)((1 - Math.Log(Math.Tan(degY * Math.PI / 180) + 1 / Math.Cos(degY * Math.PI / 180)) / Math.PI) / 2);
            return new Vector2(x, y);
        }
        private static Vector2 FromPointToLatLong(Vector2 point)
        {
            var lng = point.x * 360 - 180;
            var n = Math.PI - 2 * Math.PI * point.y;
            var lat = (float)(180 / Math.PI * Math.Atan(0.5 * (Math.Exp(n) - Math.Exp(-n))));
            return new Vector2(lng, lat/0.94501254f);
        }
    }
}