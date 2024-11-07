/* [grial-metadata] id: Grial#ScrollViewHandler.cs version: 1.0.1 */
#if IOS
using Microsoft.Maui.Layouts;
using CoreGraphics;
using UIKit;
#else
using Microsoft.Maui;
#endif

namespace FastType;

#if NET8_0_OR_GREATER
/// <summary>
/// This handler fixes: https://github.com/dotnet/maui/issues/18513
/// </summary>
public class ScrollViewHandler : Microsoft.Maui.Handlers.ScrollViewHandler, ICrossPlatformLayout
{
#if IOS
    Size ICrossPlatformLayout.CrossPlatformMeasure(double widthConstraint, double heightConstraint)
    {
        var scrollView = VirtualView;
        var crossPlatformLayout = scrollView as ICrossPlatformLayout;
        var platformScrollView = PlatformView;

        var presentedContent = scrollView.PresentedContent;
        if (presentedContent == null)
        {
            return Size.Zero;
        }

        var scrollViewBounds = platformScrollView.Bounds;
        var padding = scrollView.Padding;

        if (widthConstraint == 0)
        {
            widthConstraint = scrollViewBounds.Width;
        }

        if (heightConstraint == 0)
        {
            heightConstraint = scrollViewBounds.Height;
        }

        // Account for the ScrollView Padding before measuring the content
        var accountForPaddingMethod = typeof(Microsoft.Maui.Handlers.ScrollViewHandler).GetMethod("AccountForPadding", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        widthConstraint = (double)accountForPaddingMethod.Invoke(this, new object[] { widthConstraint, padding.HorizontalThickness });
        heightConstraint = (double)accountForPaddingMethod.Invoke(this, new object[] { heightConstraint, padding.VerticalThickness });

        // Now handle the actual cross-platform measurement of the ScrollView's content
        var result = crossPlatformLayout.CrossPlatformMeasure(widthConstraint, heightConstraint);

        return result.AdjustForFill(new Rect(0, 0, widthConstraint, heightConstraint), presentedContent);

    }

    Size ICrossPlatformLayout.CrossPlatformArrange(Rect bounds)
    {
        var scrollView = VirtualView;
        var crossPlatformLayout = scrollView as ICrossPlatformLayout;
        var platformScrollView = PlatformView;

        var contentSize = crossPlatformLayout.CrossPlatformArrange(bounds);

        // The UIScrollView's bounds are available, so we can use them to make sure the ContentSize makes sense
        // for the ScrollView orientation
        var viewportBounds = platformScrollView.Bounds;
        var viewportHeight = viewportBounds.Height;
        var viewportWidth = viewportBounds.Width;
        var setContentSizeForOrientationMethod = typeof(Microsoft.Maui.Handlers.ScrollViewHandler).GetMethod("SetContentSizeForOrientation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        setContentSizeForOrientationMethod.Invoke(this, new object[] { platformScrollView, (double)viewportWidth, (double)viewportHeight, scrollView.Orientation, contentSize });

        var getContentViewMehtod = typeof(Microsoft.Maui.Handlers.ScrollViewHandler).GetMethod("GetContentView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        var container = getContentViewMehtod.Invoke(this, new object[] { platformScrollView }) as UIView;

        if (container?.Superview is UIScrollView uiScrollView)
        {
            // Ensure the container is at least the size of the UIScrollView itself, so that the 
            // cross-platform layout logic makes sense and the contents don't arrange outside the 
            // container. (Everything will look correct if they do, but hit testing won't work properly.)

            var scrollViewBounds = uiScrollView.Bounds;
            var containerBounds = contentSize;

            container.Bounds = new CGRect(0, 0,
                Math.Max(containerBounds.Width, scrollViewBounds.Width),
                Math.Max(containerBounds.Height, scrollViewBounds.Height));

            container.Center = new CGPoint(container.Bounds.GetMidX(), container.Bounds.GetMidY());
        }

        return contentSize;
    }
#endif
}
#else
public class ScrollViewHandler : Microsoft.Maui.Handlers.ScrollViewHandler
{

}
#endif

