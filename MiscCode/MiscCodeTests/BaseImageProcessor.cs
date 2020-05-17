using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MiscCodeTests
{
    public abstract class BaseImageProcessor
    {
        protected static T ReducePixels<T>(T state, Bitmap bitmap, Func<T, Bitmap, int, int, T> eachPixel)
        {
            for (int y = 0; y < bitmap.Height; ++y)
            {
                for (int x = 0; x < bitmap.Width; ++x)
                {
                    state = eachPixel(state, bitmap, x, y);
                } 
            }
            return state;
        }

        protected static Bitmap ForEachRgbPixel(Image originalImage, Func<(byte r, byte g, byte b), (byte r, byte g, byte b)> process)
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

                        var t = process((r:red, g:green, b:blue));

                        pNew[0] = t.b;// BLUE 
                        pNew[1] = t.g;// GREEN
                        pNew[2] = t.r;// RED

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
        }
    }
}