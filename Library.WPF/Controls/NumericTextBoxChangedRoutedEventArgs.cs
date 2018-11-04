using System.Windows;

namespace Library.WPF.Controls
{
    public class NumericTextBoxChangedRoutedEventArgs : RoutedEventArgs
    {
        public double Interval { get; set; }

        public NumericTextBoxChangedRoutedEventArgs(RoutedEvent routedEvent, double interval) : base(routedEvent)
            => Interval = interval;
    }
}
