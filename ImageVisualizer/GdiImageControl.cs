using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageVisualizer
{
    public partial class GdiImageControl : ImageUserControl<System.Drawing.Image>
    {
        public GdiImageControl()
        {
            InitializeComponent();
        }

        public override void SetImage(Image image) => BackgroundImage = image;
    }
}
