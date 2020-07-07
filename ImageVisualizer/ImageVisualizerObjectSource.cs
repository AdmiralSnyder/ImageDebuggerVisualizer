using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Windows.Graphics.Imaging;

namespace ImageVisualizer
{
    public class ImageVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, Stream outgoingData)
        {
            if (target is SoftwareBitmap softwareBitmap)
            {
                base.GetData(new SerializableBitmapImage(softwareBitmap), outgoingData);
            }
            else if (target is ImageSource image)
                base.GetData(new SerializableBitmapImage((BitmapSource)image), outgoingData);
            else
                base.GetData(target, outgoingData);
        }
    }
}
