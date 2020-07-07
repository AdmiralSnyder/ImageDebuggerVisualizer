using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageVisualizer;
//using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace TestBench
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bmpPath = @"C:\Users\AGayk\Pictures\Camera Roll\WIN_20200612_19_52_09_Pro.jpg";
            
            var sf = await StorageFile.GetFileFromPathAsync(bmpPath);
            using (IRandomAccessStream stream = await sf.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);

                // Get the SoftwareBitmap representation of the file
                var sb = await decoder.GetSoftwareBitmapAsync();
                ImageVisualizer.ImageDebuggerVisualizer.TestShowVisualizer(sb);
            }

            //var stream = File.OpenRead(bmpPath);
            //var img = new System.Windows.Media.Imaging.BitmapImage();
            //img.BeginInit();
            //img.StreamSource = stream;
            //img.EndInit();

            //ImageVisualizer.ImageVisualizer.TestShowVisualizer(img);

            //var bmp = Image.FromFile(bmpPath);
            //if (bmp is Bitmap bitmap)
            //{
            //    ImageVisualizer.ImageVisualizer.TestShowVisualizer(bitmap);
            //}
            Console.ReadLine();
        }
    }
}
