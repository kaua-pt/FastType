/* [grial-metadata] id: Grial#LabelHandler.cs version: 1.0.1 */
using MauiLabelHandler = Microsoft.Maui.Handlers.LabelHandler;

namespace FastType
{
    /// <summary>
    /// This handler fixes: https://github.com/dotnet/maui/issues/14125
    /// </summary>
    public class LabelHandler : MauiLabelHandler
    {
        public LabelHandler()
            : base(Mapper)
        {

        }

        private static void UpdateMaxLines(MauiLabelHandler handler, ILabel label)
        {
#if ANDROID
            var textView = handler.PlatformView;
            if (label is Label controlsLabel
                && textView.Ellipsize == Android.Text.TextUtils.TruncateAt.End
                && controlsLabel.MaxLines != -1)
            {
                textView.SetMaxLines(controlsLabel.MaxLines);
            }
#elif IOS
            var textView = handler.PlatformView;
            if( label is Label controlsLabel
                && textView.LineBreakMode == UIKit.UILineBreakMode.TailTruncation
                && controlsLabel.MaxLines != -1)
            {
                textView.Lines = controlsLabel.MaxLines;
            }  
#endif
        }
    }
}
