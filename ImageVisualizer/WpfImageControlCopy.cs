using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace ImageVisualizer
{
    public partial class WpfImageControlCopy : UserControl
    {
        public WpfImageControlCopy()
        {
            InitializeComponent();
        }
        //public override void SetImage(ImageSource image) => imageControl1.SetImage(image);

        public void DoClose()
        {
            imageControl1.DisplayImage.ReleaseAllTouchCaptures();
            imageControl1.DisplayImage.ReleaseMouseCapture();
            imageControl1.DisplayImage.ReleaseStylusCapture();
            imageControl1.ReleaseAllTouchCaptures();
            imageControl1.ReleaseMouseCapture();
            imageControl1.ReleaseStylusCapture();
        }
    }
}
