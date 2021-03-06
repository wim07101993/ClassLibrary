﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Library.WPF.Controls
{
    public class IconButton : Button
    {
        #region DEPENDENCY PROPERTIES

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(VisualBrush),
            typeof(IconButton));


        public static readonly DependencyProperty IconVisibilityProperty = DependencyProperty.Register(
            nameof(IconVisibility),
            typeof(Visibility),
            typeof(IconButton));

        public static readonly DependencyProperty TextVisibilityProperty = DependencyProperty.Register(
            nameof(TextVisibility),
            typeof(Visibility),
            typeof(IconButton));


        public new static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(
            nameof(Background),
            typeof(Brush),
            typeof(IconButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public new static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
            nameof(Foreground),
            typeof(Brush),
            typeof(IconButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));


        public static readonly DependencyProperty HoverForegroundProperty = DependencyProperty.Register(
            nameof(HoverForeground),
            typeof(Brush),
            typeof(IconButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        public static readonly DependencyProperty HoverBackgroundProperty = DependencyProperty.Register(
            nameof(HoverBackground),
            typeof(Brush),
            typeof(IconButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));


        public static readonly DependencyProperty ClickForegroundProperty = DependencyProperty.Register(
            nameof(ClickForeground),
            typeof(Brush),
            typeof(IconButton),
            new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public static readonly DependencyProperty ClickBackgroundProperty = DependencyProperty.Register(
            nameof(ClickBackground),
            typeof(Brush),
            typeof(IconButton),
            new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));


        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(IconButton));

        private new static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(
            nameof(BorderBrush),
            typeof(Brush),
            typeof(IconButton));

        public static readonly DependencyProperty IsBorderVisibleProperty = DependencyProperty.Register(
            nameof(IsBorderVisible),
            typeof(bool),
            typeof(IconButton));

        #endregion DEPENDENCY PROPERTIES


        #region CONSTRUCTOR

        static IconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton),
                new FrameworkPropertyMetadata(typeof(IconButton)));
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public VisualBrush Icon
        {
            get => (VisualBrush) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }


        public Visibility IconVisibility
        {
            get => (Visibility) GetValue(IconVisibilityProperty);
            set => SetValue(IconVisibilityProperty, value);
        }

        public Visibility TextVisibility
        {
            get => (Visibility) GetValue(TextVisibilityProperty);
            set => SetValue(TextVisibilityProperty, value);
        }


        public new Brush Background
        {
            get => (Brush) GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public new Brush Foreground
        {
            get => (Brush) GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }


        public Brush HoverForeground
        {
            get => (Brush) GetValue(HoverForegroundProperty);
            set => SetValue(HoverForegroundProperty, value);
        }

        public Brush HoverBackground
        {
            get => (Brush) GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }


        public Brush ClickForeground
        {
            get => (Brush) GetValue(ClickForegroundProperty);
            set => SetValue(ClickForegroundProperty, value);
        }

        public Brush ClickBackground
        {
            get => (Brush) GetValue(ClickBackgroundProperty);
            set => SetValue(ClickBackgroundProperty, value);
        }


        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private new Brush BorderBrush
        {
            get => (Brush) GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public bool IsBorderVisible
        {
            get => (bool) GetValue(IsBorderVisibleProperty);
            set => SetValue(IsBorderVisibleProperty, value);
        }

        #endregion PROPERTIES
    }
}