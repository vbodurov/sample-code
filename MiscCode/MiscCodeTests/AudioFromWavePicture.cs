using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class AudioFromWavePicture
    {
        // based on https://www.youtube.com/watch?v=VQOdmckqNro
        [Test]
        public void ConvertAudioFromWavPicture()
        {
            // saying "people known"
            Bitmap b = new Bitmap(@"../../Files/audiowav.png");

            var values = new System.Collections.Generic.List<int>();

            for (int x = 0; x < b.Width; x++)
            {
                int max = 0;
                int min = b.Height;

                for (int y = 0; y < b.Height; y++)
                {
                    if (b.GetPixel(x, y).GetBrightness() > 0.5)
                    {
                        min = Math.Min(y, min);
                        max = Math.Max(max, y);
                    }
                }

                values.Add(min);
                values.Add(max);
            }

            int filter = 4;

            for (int x = 0; x < values.Count - filter; x++)
                values[x] = (int)values.GetRange(x, filter).Average();

            var f = new System.IO.StreamWriter(@"../../Files/audiodata.txt");

            foreach (var v in values)
                f.WriteLine(v);

            f.Close();

            var wf = System.IO.File.OpenWrite(@"../../Files/audio.wav");

            var RIFF_HEADER = new byte[] { 0x52, 0x49, 0x46, 0x46 };
            var FORMAT_WAVE = new byte[] { 0x57, 0x41, 0x56, 0x45 };
            var FORMAT_TAG = new byte[] { 0x66, 0x6D, 0x74, 0x20 };
            var AUDIO_FORMAT = new byte[] { 0x1, 0x0 };
            var SUBCHUNK_ID = new byte[] { 0x64, 0x61, 0x74, 0x61 };
            int BYTES_PER_SAMPLE = 1;
            int samplerate = 48000;
            int channelcount = 1;
            int lastv2 = 0;
            int stretch = 11;
            int datalength = values.Count * stretch * BYTES_PER_SAMPLE;
            int byteRate = samplerate * channelcount * BYTES_PER_SAMPLE;
            int blockAlign = channelcount * BYTES_PER_SAMPLE;

            wf.Write(RIFF_HEADER, 0, RIFF_HEADER.Length);
            wf.Write(BitConverter.GetBytes(datalength + 40), 0, 4);
            wf.Write(FORMAT_WAVE, 0, FORMAT_WAVE.Length);
            wf.Write(FORMAT_TAG, 0, FORMAT_TAG.Length);
            wf.Write(BitConverter.GetBytes(16), 0, 4);
            wf.Write(AUDIO_FORMAT, 0, AUDIO_FORMAT.Length);
            wf.Write(BitConverter.GetBytes(channelcount), 0, 2);
            wf.Write(BitConverter.GetBytes(samplerate), 0, 4);
            wf.Write(BitConverter.GetBytes(byteRate), 0, 4);
            wf.Write(BitConverter.GetBytes(blockAlign), 0, 2);
            wf.Write(BitConverter.GetBytes(BYTES_PER_SAMPLE * 8), 0, 2);
            wf.Write(SUBCHUNK_ID, 0, SUBCHUNK_ID.Length);
            wf.Write(BitConverter.GetBytes(datalength), 0, 4);

            foreach (var v in values)
            {
                double v2 = (v - values.Min()) / (double)(values.Max() - values.Min()) * 255;

                for (int x = 0; x < stretch; x++)
                {
                    double v3 = x / (double)stretch * v2 + (1 - x / (double)stretch) * lastv2;
                    wf.WriteByte(Convert.ToByte(v3));
                }

                lastv2 = (int)v2;
            }

            wf.Close();
        }
        
    }
}