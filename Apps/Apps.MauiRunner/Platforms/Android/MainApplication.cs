using Android.App;
using Android.Runtime;
using Apps.MauiRunner.Xml.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace Apps.MauiRunner
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp()
        { 
            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                if (v is BorderlessEditor)
                {
                    h.PlatformView.BackgroundTintList =
                        Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
                }
            });
            
            return MauiProgram.CreateMauiApp();
        }
    }
}
