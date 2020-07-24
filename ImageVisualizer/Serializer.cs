using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Windows.Graphics.Imaging;

namespace ImageVisualizer
{
    class Serializer
    {
        internal static ISerializable From(SoftwareBitmap softwareBitmap) => new ByteArraySerializableWrapper<SoftwareBitmap>(FromSoftwareBitmap(softwareBitmap));

        internal static ISerializable From(BitmapSource softwareBitmap) => new SerializableBitmapImage(softwareBitmap);

        internal static ISerializable From(ISerializable serializable) => serializable;
        internal static ISerializable From(object obj) => 
            obj is SoftwareBitmap sb ? From(sb)
            : obj is BitmapSource bs ? From(bs)
            : obj is ISerializable s ? From(s)
            : throw new NotImplementedException($"obj of type {obj.GetType().Name} is not serializable");

        internal static SoftwareBitmap ToSoftwareBitmap(byte[] bytes)
        {
            using var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream();

            Windows.Graphics.Imaging.BitmapDecoder bd = Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(Windows.Graphics.Imaging.BitmapDecoder.PngDecoderId, stream).AsTask().GetAwaiter().GetResult();
            bd.GetSoftwareBitmapAsync().AsTask().GetAwaiter().GetResult();
        }

        internal static byte[] FromSoftwareBitmap(SoftwareBitmap softwareBitmap)
        {
            using var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream();

            Windows.Graphics.Imaging.BitmapEncoder be = Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId, stream).AsTask().GetAwaiter().GetResult();

            be.SetSoftwareBitmap(softwareBitmap);
            be.FlushAsync().AsTask().GetAwaiter().GetResult();
            var bytes = new byte[stream.Size];
            var buf = bytes.AsBuffer();
            stream.WriteAsync(buf).AsTask().GetAwaiter().GetResult();
            return bytes;
        }
    }

    [Serializable]
    internal class ByteArraySerializableWrapper<T> : ISerializable
    {
        public ByteArraySerializableWrapper(byte[] bytes) => Bytes = bytes;
        public ByteArraySerializableWrapper(SerializationInfo info, StreamingContext context)
        {
            Bytes = (byte[])info.GetValue(nameof(Bytes), typeof(byte[]));
        }

        public byte[] Bytes { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Bytes), Bytes);
        }
    }
}
