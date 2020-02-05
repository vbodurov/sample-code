using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class GenerateImage
    {
        const string Input = @"..\..\Files\eurupa_altitude_1024x1024.png";
        const string Output = @"..\..\Files\eurupa_out_1024x1024.png";
        [Test]
        public void GenerateHeightsMap()
        {
            var original = Image.FromFile(Input);
            var output = Process(original);
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


        static Image Process(Image originalImage)
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

                for (int y = 0; y < originalImage.Height; ++y)
                {
                    for (int x = 0; x < originalImage.Width; ++x)
                    {
                        blue = pOriginalRGB[0];
                        green = pOriginalRGB[1];
                        red = pOriginalRGB[2];

                        //var curr = (byte)((0.3 * red) + (0.59 * green) + (0.11 * blue));
                        var hsv = RgbToHsv(red, green, blue);
                        var curr = (byte) (0xFF * (1f - hsv.Hue / 360f));
                        pNew[0] = curr;// BLUE 
                        pNew[1] = curr;// GREEN
                        pNew[2] = curr;// RED


                        pOriginalRGB += 3;
                        pNew += 3;
                    }
                    pOriginalRGB += nOffset;
                    pNew += nOffset;
                }
            }
            (originalImage as Bitmap).UnlockBits(originalData);
            (newImage as Bitmap).UnlockBits(newData);
            return newImage;
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