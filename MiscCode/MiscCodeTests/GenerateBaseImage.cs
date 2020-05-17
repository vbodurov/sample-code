using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using MiscCodeTests.Extensions;
using MiscCodeTests.Utils;
using NUnit.Framework;
using static MiscCodeTests.Utils.BezierFunc;

namespace MiscCodeTests
{
    [TestFixture]
    public class GenerateBaseImage : BaseImageProcessor
    {
        const string Input = @"..\..\Files\eurupa_altitude_1024x1024.png";
        //const string Output = @"..\..\Files\eurupa_out_1024x1024.png";
        const string Output = @"C:\GitHub\podcast-visualizer\PodcastVisualizer\Assets\Textures\greece_italy_height_1024x1024";
        [Test]
        public void GenerateHeightsMap()
        {
            var original = Image.FromFile(Input);
            var output = ForEachRgbPixel(original, input =>
            {
                var hsv = RgbToHsv(input.r, input.g, input.b);
                var hue01 = (1f - Clamp(hsv.Hue / 300f, 0, 1));
                hue01 = bezier2parts(hue01,
                    0.00, 0.00, 0.00, 0.20, 0.10, 0.30,
                    0.50, 0.30, 0.90, 0.30, 0.80, 1.00, 1.00, 1.00);
                var n = (byte)(hue01 * 0xFF);
                return (r: n, g: n, b: n);
            });
            var list = ReducePixels(new List<(int x, int y, Color color)>(), output, (l, b, x, y) =>
            {
                if (NeighbouringPixelsAreApartMoreThan(b, x, y, 70, out Color avg))
                {
                    l.Add((x, y, avg));
                }

                return l;
            });

            foreach (var t in list)
            {
                output.SetPixel(t.x, t.y, t.color);
            }

            if (File.Exists(Output))
            {
                File.Delete(Output);
            }
            var outDir = Path.GetDirectoryName(Output);
            if (outDir != null && !Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }


            output.Save(Output, ImageFormat.Png);

            Console.WriteLine("Output:" + Output);
        }
        Color Avg(params Color[] colors)
        {
            var e = colors.Select(c => c.B + c.G + c.B).Sum() / (3 * colors.Length);
            return Color.FromArgb(1, e, e, e);
        } 
        static bool NeighbouringPixelsAreApartMoreThan(Bitmap b, int x, int y, byte limit, out Color avg)
        {
            avg = Color.Transparent;
            if (x <= 0 || y <= 0 || x >= b.Width - 1 || y >= b.Height - 1) return false;

            return 
                PixelsAreApartMoreThan(b, x - 1, y + 0, x + 1, y + 0, limit, out avg) ||
                PixelsAreApartMoreThan(b, x - 1, y + 1, x + 1, y - 1, limit, out avg) ||
                PixelsAreApartMoreThan(b, x - 1, y - 1, x + 1, y + 1, limit, out avg) ||
                PixelsAreApartMoreThan(b, x + 0, y + 1, x + 0, y - 1, limit, out avg)
                ;
        }
        static bool PixelsAreApartMoreThan(Bitmap b, int x1, int y1, int x2, int y2, byte limit, out Color avg)
        {
            var p1 = b.GetPixel(x1, y1);
            var p2 = b.GetPixel(x2, y2);
            if (Math.Abs((p1.R + p1.G + p1.B) / 3f - (p2.R + p2.G + p2.B) / 3f) > limit)
            {
                const float dg = 0.85f;
                avg = p1.R > p2.R 
                    ? Color.FromArgb(0xFF, (int)(p1.R * dg), (int)(p1.R * dg), (int)(p1.R * dg)) 
                    : Color.FromArgb(0xFF, (int)(p2.R * dg), (int)(p2.R * dg), (int)(p2.R * dg))
                    ;
                // avg = Color.Red;
                return true;
            }
            avg = Color.Transparent;
            return false;
        }


        /*static Image Process(Image originalImage)
        {

            Bitmap newImage = new Bitmap(originalImage);
            BitmapData originalData = (originalImage as Bitmap).LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData newData = (newImage as Bitmap).LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int originalStride = originalData.Stride;
            System.IntPtr originalScan = originalData.Scan0;

            System.IntPtr newScan0 = newData.Scan0;

            unsafe
            {
                byte* pOriginalRGB = (byte*)(void*)originalScan;
                byte* pNew = (byte*)(void*)newScan0;

                int nOffset = originalStride - originalImage.Width * 3;

                //                byte red, green, blue, alpha;
                byte red, green, blue, alpha;

// var dbgDist = new SortedDictionary<int, int>(Comparer<int>.Create((a, b) => -a.CompareTo(b)));
                for (int y = 0; y < originalImage.Height; ++y)
                {
                    for (int x = 0; x < originalImage.Width; ++x)
                    {
                        blue = pOriginalRGB[0];
                        green = pOriginalRGB[1];
                        red = pOriginalRGB[2];

                        //var curr = (byte)((0.3 * red) + (0.59 * green) + (0.11 * blue));
                        var hsv = RgbToHsv(red, green, blue);
                        var hue01 = (1f - Clamp(hsv.Hue / 300f, 0, 1));
                        hue01 = bezier2parts(hue01,
                            0.00, 0.00, 0.00, 0.20, 0.10, 0.30,
                            0.50, 0.30, 0.90, 0.30, 0.80, 1.00, 1.00, 1.00);
// dbgDist.Increment((int)Math.Round(hue01*1000));
                        
                        var curr = (byte) (0xFF * hue01);
                        pNew[0] = curr;// BLUE 
                        pNew[1] = curr;// GREEN
                        pNew[2] = curr;// RED


                        pOriginalRGB += 3;
                        pNew += 3;
                    }
                    pOriginalRGB += nOffset;
                    pNew += nOffset;
                }
// File.WriteAllText(@"C:\GitHub\sample-code\MiscCode\MiscCodeTests\Files\__log.txt", dbgDist.Select(k => k.Key + "\t" + k.Value).JoinAsString("\n"));
            }


            (originalImage as Bitmap).UnlockBits(originalData);
            (newImage as Bitmap).UnlockBits(newData);
            return newImage;
        }*/
        static float Clamp(float val, float min, float max)
        {
            if (val < min) return min;
            if (val > max) return max;
            return val;
        }
        [Test]
        [TestCase(0xFF, 0x00, 0x00, 000f, 1f, 1f)]
        [TestCase(0xFF, 0x00, 0xFF, 300f, 1f, 1f)]
        [TestCase(0x88, 0x00, 0x77, 308f, 1f, 0.533f)]
        public void TestRgbToHsv(byte r, byte g, byte b, float h, float s, float v)
        {
            var hsv = RgbToHsv(r, g, b);
            Assert.That(hsv.Hue, Is.EqualTo(h).Within(1));
            Assert.That(hsv.Saturation, Is.EqualTo(s).Within(0.01));
            Assert.That(hsv.Value, Is.EqualTo(v).Within(0.01));
        }
        public struct HSV
        {
            public float Hue { get; }// 0 - 360
            public float Saturation { get; } // 0 - 100
            public float Value { get; }// 0 - 100
            public HSV(float h, float s, float v)
            {
                Hue = h;
                Saturation = s;
                Value = v;
            }
        }
        /// <summary>
        /// red green blue to Hue Saturation Value
        /// </summary>
        static HSV RgbToHsv(byte red, byte green, byte blue)
        {
            var color = System.Drawing.Color.FromArgb(0xFF, red, green, blue);
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            return new HSV (color.GetHue(), color.GetSaturation(), max / 255f);
        }

    }
}