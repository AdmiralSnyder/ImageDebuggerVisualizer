using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Windows.Graphics.Imaging;

namespace ImageVisualizer
{
    public class ImageVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, Stream outgoingData) => base.GetData(Serializer.From(target), outgoingData);
    }
}
