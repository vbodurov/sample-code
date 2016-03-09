using System;
using System.Globalization;
using System.IO;
using System.Speech.Synthesis;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class TextToSpeech
    {
        [Test]
        public void SpeakToFile()
        {
            var reader = new SpeechSynthesizer();
            reader.SetOutputToWaveFile("../../Files/Speech.wav");

            var builder = new PromptBuilder();
            foreach (var voice in reader.GetInstalledVoices())
            {
                var info = voice.VoiceInfo;

                builder.StartVoice(new CultureInfo("en-US"));
                {
                    builder.StartVoice(info.Gender);
                    {
                        //builder.AppendText($"I am {info.Name}, I am {info.Gender}, I am {info.Age}");
                        builder.AppendText($"We are now in central Africa, you can see the following constellations");
                    }
                    builder.EndVoice();
                }
                builder.EndVoice();
            }
            reader.Rate = 0;
            reader.Speak(builder);

            Console.WriteLine("done");

        }
    }
}