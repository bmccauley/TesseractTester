using System;
using System.Drawing;
using Tesseract;
using System.IO;
using System.Text;

namespace TesseractTester
{
    class TesseractTester
    {
        public static void Main()
        {
            string[] files = Directory.GetFiles(@"C:\Users\bmccauley.PCI\Pictures\SingleCharacter");
            string[] results = new string[files.Length];
            var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            for (int i = 0; i < files.Length; i++) {
                string fileName = files[i];
                var img = Pix.LoadFromFile(fileName);
                var page = engine.Process(img);
                var text = page.GetText();
                char[] textArray = text.ToCharArray();
                for (int j = 0; j < textArray.Length; j++)
                {
                    char c = textArray[j];
                    if (!Char.IsLetterOrDigit(c))
                    {
                        textArray[j] = '\0';
                    }
                }
                text = new String(textArray);
                StringBuilder builder = new StringBuilder();
                builder.Append(fileName).Append(",\"").Append(text).Append("\"");
                results[i] = builder.ToString();
                page.Dispose();
            }
            StreamWriter writer = new StreamWriter(@"C:\Users\bmccauley.PCI\Documents\TesseractOutput.csv");
            for (int i = 0; i < results.Length; i++)
            {
                writer.WriteLine(results[i]);
            }
            writer.Close();
        }
    }
}
