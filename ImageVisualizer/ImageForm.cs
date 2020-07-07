using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Toolkit.Forms.UI.XamlHost;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ImageVisualizer
{
    public partial class ImageForm : Form
    {
        [DllImport("gdi32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DeleteObject(IntPtr hObject);

        public static Font UIFont
        {
            get
            {
//#if VS10
//                var dteProgID = "VisualStudio.DTE.10.0";
//#elif VS11
//                var dteProgID = "VisualStudio.DTE.11.0";
//#elif VS12
//                var dteProgID = "VisualStudio.DTE.12.0";
//#elif VS13
//                var dteProgID = "VisualStudio.DTE.13.0";
//#elif VS14
//                var dteProgID = "VisualStudio.DTE.14.0";
//#elif VS15
//                var dteProgID = "VisualStudio.DTE.15.0";
//#elif VS16
                var dteProgID = "VisualStudio.DTE.16.0";
//#endif
                var dte = (EnvDTE.DTE)Marshal.GetActiveObject(dteProgID);
                var fontProperty = dte.Properties["FontsAndColors", "Dialogs and Tool Windows"];
                if (fontProperty != null)
                {
                    object objValue = fontProperty.Item("FontFamily").Value;
                    var fontFamily = objValue.ToString();
                    objValue = fontProperty.Item("FontSize").Value;
                    var fontSize = Convert.ToSingle(objValue);
                    var font = new Font(fontFamily, fontSize);

                    return font;
                }

                return System.Drawing.SystemFonts.DefaultFont;
            }
        }

        public ImageForm(IVisualizerObjectProvider objectProvider)
        {
            InitializeComponent();

            var uwphost = new WindowsXamlHost();
            uwphost.Parent = UwpTP;
            uwphost.InitialTypeName = "Windows.UI.Xaml.Controls.CalendarView";

#if DEBUG
            this.ShowInTaskbar = true;
#endif

            this.label1.Font = UIFont;
            SetFontAndScale(this.label1, UIFont);
            this.label2.Font = UIFont;
            this.txtExpression.Font = UIFont;
            this.btnClose.Font = UIFont;

            object objectBitmap = objectProvider.GetObject();
            if (objectBitmap != null)
            {
#if DEBUG
                string expression = objectBitmap.ToString();
#endif

                var method = objectBitmap.GetType().GetMethod("ToBitmap", new Type[] { });
                if (method != null)
                {
                    objectBitmap = method.Invoke(objectBitmap, null);
                }

                System.Windows.Media.ImageSource bitmapSource = null;

                if (objectBitmap is Bitmap)
                {
                    var hObject = ((Bitmap)objectBitmap).GetHbitmap();

                    try
                    {
                        bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                               hObject,
                               IntPtr.Zero,
                               Int32Rect.Empty,
                               BitmapSizeOptions.FromEmptyOptions());
                    }
                    catch (Win32Exception)
                    {
                        bitmapSource = null;
                    }
                    finally
                    {
                        DeleteObject(hObject);
                    }
                }
                else if (objectBitmap is SerializableBitmapImage serializableBitmapImage)
                {
                    if (serializableBitmapImage.Image != null)
                    {
                        bitmapSource = (System.Windows.Media.Imaging.BitmapSource)serializableBitmapImage;
                    }
                    else if (serializableBitmapImage.SoftwareBitmap != null)
                    {
                        using (var stream = new Windows.Storage.Streams.InMemoryRandomAccessStream())
                        {
                            
                            Windows.Graphics.Imaging.BitmapEncoder encoder = Windows.Graphics.Imaging.BitmapEncoder.CreateAsync(Windows.Graphics.Imaging.BitmapEncoder.PngEncoderId, stream).GetResults();
                            encoder.SetSoftwareBitmap(serializableBitmapImage.SoftwareBitmap);
                            var t = Task.Run(async () =>  await encoder.FlushAsync());
                            t.GetAwaiter().GetResult();

                            var bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.StreamSource = stream.AsStream();
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.EndInit();
                            bitmap.Freeze();
                            bitmapSource = bitmap;
                            // use bitmap
                        }
                        //using (var sb = serializableBitmapImage.SoftwareBitmap.LockBuffer())
                        //{

                        //    var source = WriteableBitmap.Create(serializableBitmapImage.SoftwareBitmap.PixelWidth, serializableBitmapImage.SoftwareBitmap.PixelHeight, serializableBitmapImage.SoftwareBitmap.DpiX, serializableBitmapImage.SoftwareBitmap.DpiY,
                        //        serializableBitmapImage.SoftwareBitmap.BitmapPixelFormat, null, sb.CreateReference()., serializableBitmapImage.SoftwareBitmap.PixelWidth);
                        //    source.

                        //}
                        //    var bs = new SoftwareBitmapSource();
                        //    await bs.SetBitmapAsync(serializableBitmapImage.SoftwareBitmap);
                        //bitmapSource = bs;
                    }
                }

                if (bitmapSource != null)
                {
                    imageControl.SetImage(bitmapSource);
                }
            
                txtExpression.Text = objectBitmap.ToString();
            }
     
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SetFontAndScale(Control ctlControl, Font objFont)
        {
            float sngRatio = objFont.Size / ctlControl.Font.Size;
            if (ctlControl is Form form)
            {
                form.AutoScaleMode = AutoScaleMode.None;
            }
            ctlControl.Font = objFont;
            ctlControl.Scale(new SizeF(sngRatio, sngRatio));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
