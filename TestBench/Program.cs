using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ImageVisualizer;
//using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace TestBench
{
    class Program
    {
        static string BmpPath = Path.Combine(Environment.CurrentDirectory, "TestBenchImage.jpg");

        static async Task Main(string[] args)
        {
            await TestUWPSoftwareBitmap();
            //TestSDImage();

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

        private static void TestSDImage()
        {
            using var img = Image.FromFile(BmpPath);
            ImageDebuggerVisualizer.TestShowVisualizer(img);
        }

        private static async Task TestUWPSoftwareBitmap()
        {
            var sf = await StorageFile.GetFileFromPathAsync(BmpPath);
            using IRandomAccessStream stream = await sf.OpenAsync(FileAccessMode.Read);
            // Create the decoder from the stream
            var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);

            // Get the SoftwareBitmap representation of the file
            var sb = await decoder.GetSoftwareBitmapAsync();
            ImageDebuggerVisualizer.TestShowVisualizer(sb);
        }

        private static async Task TestWPFBitmapImage()
        {
            using var fs = File.OpenRead(BmpPath);
            //var decoder = System.Windows.Media.Imaging.BitmapDecoder.Create(fs, System.Windows.Media.Imaging.BitmapCreateOptions.None, System.Windows.Media.Imaging.BitmapCacheOption.None);

            // Get the SoftwareBitmap representation of the file
            var img = new BitmapImage();
            img.BeginInit();
            img.StreamSource = fs;
            img.EndInit();
            
            ImageDebuggerVisualizer.TestShowVisualizer(img);
        }
    }
}
