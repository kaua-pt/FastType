/* [grial-metadata] id: Grial#RoundedIcon.xaml version: 1.0.1 */
using System;
using System.Collections.Generic;
using UXDivers.Grial;

namespace FastType
{
    public partial class RoundedIcon : ContentView
    {
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(
                nameof(CornerRadius),
                typeof(CornerRadius),
                typeof(RoundedIcon),
                defaultValue: default(CornerRadius),
                propertyChanged: (b, o, n) => ((RoundedIcon)b).UpdateCornerRadius());

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty IsCircularProperty =
            BindableProperty.Create(
                nameof(IsCircular),
                typeof(bool),
                typeof(RoundedIcon),
                defaultValue: false,
                propertyChanged: (b, o, n) => ((RoundedIcon)b).UpdateCornerRadius());

        public bool IsCircular
        {
            get { return (bool)GetValue(IsCircularProperty); }
            set { SetValue(IsCircularProperty, value); }
        }

        public static readonly BindableProperty IconFontSizeProperty = BindableProperty.Create(
            nameof(IconFontSize),
            typeof(double),
            typeof(RoundedIcon),
            defaultValue: 11d);

        public double IconFontSize
        {
            get { return (double)GetValue(IconFontSizeProperty); }
            set { SetValue(IconFontSizeProperty, value); }
        }

        public static readonly BindableProperty IconFontFamilyProperty = BindableProperty.Create(
            nameof(IconFontFamily),
            typeof(string),
            typeof(RoundedIcon),
            defaultValue: null);

        public string IconFontFamily
        {
            get { return (string)GetValue(IconFontFamilyProperty); }
            set { SetValue(IconFontFamilyProperty, value); }
        }

        public static readonly BindableProperty IconTextProperty = BindableProperty.Create(
            nameof(IconText),
            typeof(string),
            typeof(RoundedIcon),
            defaultValue: null);

        public string IconText
        {
            get { return (string)GetValue(IconTextProperty); }
            set { SetValue(IconTextProperty, value); }
        }

        public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
            nameof(IconColor),
            typeof(Color),
            typeof(RoundedIcon),
            defaultValue: Colors.Black);

        public Color IconColor
        {
            get { return (Color)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        public static readonly BindableProperty IconBackgroundOpacityProperty = BindableProperty.Create(
            nameof(IconBackgroundOpacity),
            typeof(double),
            typeof(RoundedIcon),
            defaultValue: 1d);

        public double IconBackgroundOpacity
        {
            get { return (double)GetValue(IconBackgroundOpacityProperty); }
            set { SetValue(IconBackgroundOpacityProperty, value); }
        }

        public static readonly BindableProperty IsIconBackgroundCircularProperty = BindableProperty.Create(
            nameof(IsIconBackgroundCircular),
            typeof(bool),
            typeof(RoundedIcon),
            propertyChanged: (b, o, n) => ((RoundedIcon)b).UpdateIconCornerRadius());

        public bool IsIconBackgroundCircular
        {
            get { return (bool)GetValue(IsIconBackgroundCircularProperty); }
            set { SetValue(IsIconBackgroundCircularProperty, value); }
        }

        public static readonly BindableProperty IsIconBackgroundVisibleProperty =
            BindableProperty.Create(
                nameof(IsIconBackgroundVisible),
                typeof(bool),
                typeof(RoundedIcon),
                defaultValue: true);

        public bool IsIconBackgroundVisible
        {
            get { return (bool)GetValue(IsIconBackgroundVisibleProperty); }
            set { SetValue(IsIconBackgroundVisibleProperty, value); }
        }

        public static readonly BindableProperty IconBackgroundColorProperty = BindableProperty.Create(
           nameof(IconBackgroundColor),
           typeof(Color),
           typeof(RoundedIcon),
           null);

        public Color IconBackgroundColor
        {
            get { return (Color)GetValue(IconBackgroundColorProperty); }
            set { SetValue(IconBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty IconPaddingProperty = BindableProperty.Create(
            nameof(IconPadding),
            typeof(Thickness),
            typeof(RoundedIcon),
            defaultValue: default(Thickness));

        public Thickness IconPadding
        {
            get { return (Thickness)GetValue(IconPaddingProperty); }
            set { SetValue(IconPaddingProperty, value); }
        }

        public static readonly BindableProperty IconBackgroundCornerRadiusProperty = BindableProperty.Create(
           nameof(IconBackgroundCornerRadius),
           typeof(CornerRadius),
           typeof(RoundedIcon),
           default(CornerRadius),
           propertyChanged: (b, o, n) => ((RoundedIcon)b).UpdateIconCornerRadius());

        public CornerRadius IconBackgroundCornerRadius
        {
            get { return (CornerRadius)GetValue(IconBackgroundCornerRadiusProperty); }
            set { SetValue(IconBackgroundCornerRadiusProperty, value); }
        }

        public static readonly BindableProperty IconHorizontalOptionsProperty =
            BindableProperty.Create(
                nameof(IconHorizontalOptions),
                typeof(LayoutOptions),
                typeof(RoundedIcon),
                defaultValue: LayoutOptions.Fill);

        public LayoutOptions IconHorizontalOptions
        {
            get { return (LayoutOptions)GetValue(IconHorizontalOptionsProperty); }
            set { SetValue(IconHorizontalOptionsProperty, value); }
        }

        public static readonly BindableProperty IconVerticalOptionsProperty =
            BindableProperty.Create(
                nameof(IconVerticalOptions),
                typeof(LayoutOptions),
                typeof(RoundedIcon),
                defaultValue: LayoutOptions.Fill);

        public LayoutOptions IconVerticalOptions
        {
            get { return (LayoutOptions)GetValue(IconVerticalOptionsProperty); }
            set { SetValue(IconVerticalOptionsProperty, value); }
        }

        public RoundedIcon()
        {
            InitializeComponent();
        }

        protected RoundedIcon(bool initialize)
        {
            if (initialize)
            {
                InitializeComponent();
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (IsIconBackgroundCircular)
            {
                UpdateIconCornerRadius();
            }

            if (IsCircular)
            {
                UpdateCornerRadius();
            }
        }

        protected virtual View GetBackground() => background;

        protected virtual View GetRoot() => root;

        private void UpdateIconCornerRadius()
        {
            var background = GetBackground();
            if (IsIconBackgroundCircular)
            {
                UXDivers.Grial.Effects.SetCornerRadius(background, Math.Min(background.Width, background.Height) / 2);
            }
            else
            {
                UXDivers.Grial.Effects.SetCornerRadius(background, IconBackgroundCornerRadius);
            }
        }

        private void UpdateCornerRadius()
        {
            if (IsCircular)
            {
                UXDivers.Grial.Effects.SetCornerRadius(GetRoot(), Math.Min(Width, Height) / 2);
            }
            else
            {
                UXDivers.Grial.Effects.SetCornerRadius(GetRoot(), CornerRadius);
            }
        }
    }
}
