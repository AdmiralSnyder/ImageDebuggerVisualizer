using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Graphics.Imaging;
using Microsoft.Toolkit.Forms.UI.XamlHost;

namespace ImageVisualizer
{
    public partial class UwpImageControl : ImageUserControl<Windows.Graphics.Imaging.SoftwareBitmap>
    {
        public UwpImageControl()
        {
            InitializeComponent();

            var uwphost = new WindowsXamlHost();
            uwphost.Parent = this;
            uwphost.Dock = DockStyle.Fill;
            uwphost.Visible = true;
            uwphost.InitialTypeName = "Windows.UI.Xaml.Controls.Image";
            uwphost.ChildChanged += Uwphost_ChildChanged;
        }

        private void Uwphost_ChildChanged(object sender, EventArgs e)
        {
            WindowsXamlHost windowsXamlHost = (WindowsXamlHost)sender;

            Image = (Windows.UI.Xaml.Controls.Image)windowsXamlHost.Child;
        }

        Windows.UI.Xaml.Controls.Image Image;

        public override void SetImage(SoftwareBitmap image)
        {
            var source = new Windows.UI.Xaml.Media.Imaging.SoftwareBitmapSource();
            source.SetBitmapAsync(image).AsTask().GetAwaiter().GetResult();
            Image.Source = source;
        }
    }
}
