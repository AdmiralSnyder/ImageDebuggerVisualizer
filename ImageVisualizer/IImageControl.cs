using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageVisualizer
{
    public interface IImageControl
    {
        void SetImage(object image);
    }

    public interface IImageControl<TImage> : IImageControl
    {
        void SetImage(TImage image);
    }
}
