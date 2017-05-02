using System;
using System.Windows.Threading;


namespace WindowsClassLibrary
{
    /// <summary>
    /// Static class to use an object of the class <see cref="Timer"/>.
    /// </summary>
    public static class Timer
    {
        /// <summary>
        /// Private constructor for the static class to instantiate a <see cref="DispatcherTimer"/>
        /// </summary>
        static Timer()
        {
            var dispatcherTimer = new DispatcherTimer(
                TimeSpan.FromMilliseconds(1),
                DispatcherPriority.Background,
                (sender, e) => Tick?.Invoke(sender, e),
                Dispatcher.CurrentDispatcher);
        }

        /// <summary>
        /// Occures when one millisecond has elapsed.
        /// </summary>
        public static event EventHandler Tick;
    }
}
