using ImageVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Windows.Graphics.Imaging;

//System.Drawing.Bitmap
[assembly: System.Diagnostics.DebuggerVisualizer(typeof(ImageVisualizer.ImageDebuggerVisualizer), typeof(VisualizerObjectSource), Target = typeof(System.Drawing.Bitmap), Description = "Image Visualizer")]
//System.Windows.Media.ImageSource, System.Windows.Media.Imaging.BitmapImage, System.Windows.Media.Imaging.BitmapSource
[assembly: System.Diagnostics.DebuggerVisualizer(typeof(ImageVisualizer.ImageDebuggerVisualizer), typeof(ImageVisualizerObjectSource), Target = typeof(System.Windows.Media.ImageSource), Description = "Image Visualizer")]
//Windows.Graphics.Imaging.SoftwareBitmap
[assembly: System.Diagnostics.DebuggerVisualizer(typeof(ImageVisualizer.ImageDebuggerVisualizer), typeof(ImageVisualizerObjectSource), Target = typeof(Windows.Graphics.Imaging.SoftwareBitmap), Description = "Image Visualizer")]

namespace ImageVisualizer
{

    // TODO: Add the following to SomeType's definition to see this visualizer when debugging instances of SomeType:
    // 
    //  [DebuggerVisualizer(typeof(ImageVisualizer))]
    //  [Serializable]
    //  public class SomeType
    //  {
    //   ...
    //  }
    // 
    /// <summary>
    /// A Visualizer for SomeType.  
    /// </summary>
    public class ImageDebuggerVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            if (windowService is null)
                throw new ArgumentNullException("windowService");
            if (objectProvider is null)
                throw new ArgumentNullException("objectProvider");

            
            
            //var page = new UWPForm();
            //var rootFrame = Windows.UI.Xaml.Window.Current.Content as Windows.UI.Xaml.Controls.Frame;
            //rootFrame.Content = page;

            using (DpiAwareness.EnterDpiScope(DpiAwarenessContext.SystemAware))
            using (var imageForm = new ImageForm(objectProvider))
            {
                windowService.ShowDialog(imageForm);
            }
        }

        // TODO: Add the following to your testing code to test the visualizer:
        // 
        //    ImageVisualizer.TestShowVisualizer(new SomeType());
        // 
        /// <summary>
        /// Tests the visualizer by hosting it outside of the debugger.
        /// </summary>
        /// <param name="objectToVisualize">The object to display in the visualizer.</param>
        public static void TestShowVisualizer(object objectToVisualize)
        {
            var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(ImageDebuggerVisualizer), typeof(ImageVisualizerObjectSource));
            var t = new Thread(() => visualizerHost.ShowVisualizer());
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }
    }
}
