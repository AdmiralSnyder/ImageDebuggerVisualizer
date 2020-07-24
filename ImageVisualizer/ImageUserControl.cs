using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageVisualizer
{

    public abstract class ImageUserControl : UserControl, IImageControl
    {
        public abstract void SetImage(object image);
    }

    public abstract class ImageUserControl<TImage> : ImageUserControl, IImageControl<TImage>
    {
        public abstract void SetImage(TImage image);
        public override void SetImage(object image) => SetImage((TImage)image);
    }
}
