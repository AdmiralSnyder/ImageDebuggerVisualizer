using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace ImageVisualizer
{
    [Serializable]
    public class SerializableBitmapImage : ISerializable
    {
        public BitmapSource BitmapSource;
        public SoftwareBitmap SoftwareBitmap { get; private set; }
        private readonly string Expression;

        public BitmapImage Image { get; private set; }

        public SerializableBitmapImage(BitmapImage image) => Image = image;

        public SerializableBitmapImage(BitmapSource source) => BitmapSource = source;

        protected SerializableBitmapImage(SerializationInfo info, StreamingContext context)
        {
            byte[] bytes = null;
            BitmapPixelFormat bitmapPixelFormat = default;
            BitmapAlphaMode bitmapAlphaMode = default;
            int width = default;
            int height = default;

            foreach (var i in info)
            {
                if (string.Equals(i.Name, "Image", StringComparison.OrdinalIgnoreCase))
                {

                    try
                    {
                        if (i.Value is byte[] array)
                        {
                            var stream = new MemoryStream(array);
                            stream.Seek(0, SeekOrigin.Begin);

                            Image = new BitmapImage
                            {
                                CacheOption = BitmapCacheOption.OnLoad
                            };
                            Image.BeginInit();
                            Image.StreamSource = stream;
                            Image.EndInit();
                            Image.Freeze();
                        }
                    }
                    catch (ExternalException)
                    {
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (OutOfMemoryException)
                    {
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    catch (NotImplementedException)
                    {
                    }
                    catch (FileNotFoundException)
                    {
                    }
                }
                else if (string.Equals(i.Name, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    Expression = (string)i.Value;
                }
                else if (i.Name == "SoftwareBitmap")
                {
                    if (i.Value is byte[] data)
                    {
                        bytes = data;
                    }
                }
                else if (i.Name == nameof(SoftwareBitmap.BitmapPixelFormat) + "_int")
                {
                    var intval = (int)i.Value;
                    bitmapPixelFormat = (BitmapPixelFormat)intval;
                }
                else if (i.Name == nameof(SoftwareBitmap.BitmapAlphaMode) + "_int")
                {
                    var intval = (int)i.Value;
                    bitmapAlphaMode = (BitmapAlphaMode)intval;
                }
                else if (i.Name == nameof(SoftwareBitmap.PixelWidth))
                {
                    width = (int)i.Value;
                }
                else if (i.Name == nameof(SoftwareBitmap.PixelHeight))
                {
                    height = (int)i.Value;
                }
            }

            if (bytes != null)
            {
                //using (var ms = new MemoryStream(bytes.Length))
                //{
                //    ms.Read(bytes, 0, bytes.Length);
                //    ms.Seek(0, SeekOrigin.Begin);
                //    var buf = ms.GetWindowsRuntimeBuffer();
                    var buffer = bytes.AsBuffer();
                    SoftwareBitmap = new SoftwareBitmap(bitmapPixelFormat, width, height, bitmapAlphaMode);
                    SoftwareBitmap.CopyFromBuffer(buffer);
                //}
            }
        }

        public SerializableBitmapImage(SoftwareBitmap softwareBitmap)
        {
            this.SoftwareBitmap = softwareBitmap;
        }

        public static implicit operator SerializableBitmapImage(BitmapImage bitmapImage)
        {
            return new SerializableBitmapImage(bitmapImage);
        }

        public static implicit operator BitmapImage(SerializableBitmapImage serializableBitmapImage)
        {
            return serializableBitmapImage.Image;
        }

        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var source = Image ?? BitmapSource;

            if (source != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(source));
                    encoder.Save(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    info.AddValue("Image", memoryStream.ToArray(), typeof(byte[]));
                }

                info.AddValue("Name", "TODO");// source.ToString());
            }
            else if (SoftwareBitmap != null)
            {
                var arr = new byte[4 * SoftwareBitmap.PixelHeight * SoftwareBitmap.PixelWidth];
                var buf = arr.AsBuffer();
                    
                   //var buf = ms.GetWindowsRuntimeBuffer();
                    SoftwareBitmap.CopyToBuffer(buf);
                //var arr = ms.ToArray();
                info.AddValue("SoftwareBitmap", arr, typeof(byte[]));
                info.AddValue("BitmapPixelFormat_int", (int)SoftwareBitmap.BitmapPixelFormat, typeof(int));
                info.AddValue("PixelWidth", SoftwareBitmap.PixelWidth, typeof(int));
                info.AddValue("PixelHeight", SoftwareBitmap.PixelHeight, typeof(int));
                info.AddValue(nameof(SoftwareBitmap.BitmapAlphaMode) + "_int", (int)SoftwareBitmap.BitmapAlphaMode, typeof(int));

                //using (var s = new Windows.Storage.Streams.InMemoryRandomAccessStream())
                //{
                //    var enc = await Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId, s);
                //    enc.SetSoftwareBitmap(SoftwareBitmap);
                //    try
                //    {
                //        await enc.FlushAsync();
                //    }
                //    catch (Exception e)
                //    {

                //    }
                //    //var encoder = await Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId, s);
                //    //encoder.SetSoftwareBitmap(SoftwareBitmap);
                //    //encoder.IsThumbnailGenerated = false;
                //    //await encoder.FlushAsync();
                //    var array = new byte[s.Size];
                //    await s.ReadAsync(array.AsBuffer(), (uint)s.Size, InputStreamOptions.None);

                //    info.AddValue("Image", array, typeof(byte[]));
                //}


                info.AddValue("Name", SoftwareBitmap.ToString());
            }
        }



        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Expression))
                return Expression;

            return base.ToString();
        }
    }

    [Serializable]
    public class BitmapData : ISerializable
    {
        public BitmapData(BitmapPixelFormat bitmapPixelFormat, int width, int height, byte[] bytes)
            => (BitmapPixelFormat, Width, Height, Bytes) = (bitmapPixelFormat, width, height, bytes);
        public BitmapData(SerializationInfo info, StreamingContext context)
        {
            foreach (var i in info)
            {
                if (i.Name == nameof(BitmapPixelFormat))
                { BitmapPixelFormat = (BitmapPixelFormat)i.Value; }
                if (i.Name == nameof(Width))
                { Width = (int)i.Value; }
                if (i.Name == nameof(Height))
                { Height = (int)i.Value; }
                if (i.Name == nameof(Bytes))
                { Bytes = (byte[])i.Value; }
            }
        }

        public BitmapPixelFormat BitmapPixelFormat { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Bytes { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(BitmapPixelFormat), BitmapPixelFormat, typeof(BitmapPixelFormat));
            info.AddValue(nameof(Width), Width, typeof(int));
            info.AddValue(nameof(Height), Height, typeof(int));
            info.AddValue(nameof(Bytes), Bytes, typeof(byte[]));
        }
    }
}
