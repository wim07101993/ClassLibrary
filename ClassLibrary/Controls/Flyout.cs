using System;
using System.Windows;
using System.Windows.Controls;


namespace WindowsClassLibrary.Controls
{
    public class Flyout : UserControl
    {
        #region FIELDS

        public static DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(int), typeof(Flyout), new FrameworkPropertyMetadata(50));

        public static DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Flyout));

        public static DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Flyout),
                new PropertyMetadata(Orientation.Horizontal, OnOrientationChanged));

        public new static DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(Flyout));
        public new static DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(double), typeof(Flyout));
        
        #endregion FIELDS


        #region PROPERTIES

        public int Speed
        {
            get => (int)GetValue(SpeedProperty);
            set => SetValue(SpeedProperty, value);
        }

        public bool IsOpen
        {
            get => (bool) GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public Orientation Orientation
        {
            get => (Orientation) GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public new double Width
        {
            get => (double) GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }
        public new double Height
        {
            get => (double) GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        private double BaseWidth
        {
            set => base.Width = value;
        }
        private double BaseHeight
        {
            set => base.Height = value;
        }

        #endregion PROPERTIES


        #region CONSTRUCTOR

        public Flyout()
        {
            if (Orientation == Orientation.Horizontal)
                BaseWidth = 0;
            else BaseHeight = 0;

            Timer.Tick += Timer_Tick;
        }

        #endregion CONSTRUCTOR


        #region METHODS

        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var flyout = (Flyout) d;

            if (flyout.Orientation == Orientation.Vertical)
            {
                flyout.BaseWidth = flyout.Width;
                flyout.BaseHeight = 0;
            }
            else
            {
                flyout.BaseHeight = flyout.Height;
                flyout.BaseWidth = 0;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Orientation == Orientation.Vertical)
            {
                if (IsOpen && ActualHeight <= Height - Speed)
                    base.Height += Speed;
                else if (IsOpen)
                    base.Height = Height;
                else if (!IsOpen && ActualHeight >= Speed)
                    base.Height -= Speed;
                else if (!IsOpen) base.Height = 0;
            }
            else
            {
                if (IsOpen && ActualWidth <= Width - Speed)
                    base.Width += Speed;
                else if (IsOpen)
                    base.Width = Width;
                else if (!IsOpen && ActualWidth >= Speed)
                    base.Width -= Speed;
                else if (!IsOpen) base.Width = 0;
            }
        }

        #endregion METHODS
    }
}