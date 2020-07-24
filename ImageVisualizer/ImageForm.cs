using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Toolkit.Forms.UI.XamlHost;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Windows.Graphics.Imaging;

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
                var dteProgID = "VisualStudio.DTE.16.0";
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

        public Dictionary<Type, Func<ImageUserControl>> ImageControls = new Dictionary<Type, Func<ImageUserControl>>
        {
            [typeof(System.Drawing.Image)] = () => new GdiImageControl(),
            [typeof(System.Windows.Media.ImageSource)] = () => new WpfImageControl(),
            [typeof(Windows.Graphics.Imaging.SoftwareBitmap)] = () => new UwpImageControl(),
        };

        public ImageForm(IVisualizerObjectProvider objectProvider)
        {
            InitializeComponent();

#if DEBUG
            ShowInTaskbar = true;
#endif

            label1.Font = UIFont;
            SetFontAndScale(label1, UIFont);
            label2.Font = UIFont;
            txtExpression.Font = UIFont;
            CloseB.Font = UIFont;

            object objectBitmap = objectProvider.GetObject();

            object image = null;

            if (objectBitmap != null)
            {            
                if (objectBitmap is ByteArraySerializableWrapper<SoftwareBitmap> wrapper)
                {
                    image = Serializer.ToSoftwareBitmap(wrapper.Bytes);
                }
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
                    image = objectBitmap;
                    //var hObject = ((Bitmap)objectBitmap).GetHbitmap();

                    //try
                    //{
                    //    bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    //           hObject,
                    //           IntPtr.Zero,
                    //           Int32Rect.Empty,
                    //           BitmapSizeOptions.FromEmptyOptions());
                    //    image = bitmapSource;
                    //}
                    //catch (Win32Exception)
                    //{
                    //    bitmapSource = null;
                    //}
                    //finally
                    //{
                    //    DeleteObject(hObject);
                    //}
                }
                else 
                if (objectBitmap is SerializableBitmapImage serializableBitmapImage)
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
                            image = (ImageSource)bitmap;
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

                //if (bitmapSource != null)
                //{
                //    imageControl.SetImage(bitmapSource);
                //}

                var type = image.GetType();
                Func<ImageUserControl> factory = null;
                while(type != null && !ImageControls.TryGetValue(type, out factory))
                {
                    type = type.BaseType;
                }
                if (factory is { })
                {
                    var imageControl = ImageControls[type]();
                    imageControl.Parent = panel1;
                    imageControl.Dock = DockStyle.Fill;
                    imageControl.SetImage(image);
                }

                txtExpression.Text = objectBitmap.ToString();
            }
     
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                Close();
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

        private void CloseB_Click(object sender, EventArgs e)
        {
            var imageCtrl = panel1.Controls.OfType<Control>().FirstOrDefault();
            if (imageCtrl is WpfImageControl wpfImageControl)
            {
                wpfImageControl.DoClose();

                imageCtrl.Visible = false;
                imageCtrl.Dispose();
            }

            Close();
        }
    }
}
