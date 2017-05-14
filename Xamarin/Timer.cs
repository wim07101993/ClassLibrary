using System;
using Xamarin.Forms;


namespace ClassLibrary.Xamarin
{
    /// <summary>
    /// Static class to use the Device.StartTimer() function with an event.
    /// </summary>
    public static class Timer
    {
        /// <summary>
        /// Private constructor for the static class.
        /// </summary>
        static Timer()
        {
            Device.StartTimer(
                TimeSpan.FromMilliseconds(1),
                () =>
                {
                    Tick?.Invoke(null, EventArgs.Empty);
                    return true;
                });

            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {
                TenTicks?.Invoke(null, EventArgs.Empty);
                return true;
            });

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                HundredTicks?.Invoke(null, EventArgs.Empty);
                return true;
            });

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                ThousandTicks?.Invoke(null, EventArgs.Empty);
                return true;
            });
        }

        /// <summary>
        /// Occures when 1 millisecond has elapsed.
        /// </summary>
        public static event EventHandler Tick;
        /// <summary>
        /// Occures when 10 milliseconds has elapsed.
        /// </summary>
        public static event EventHandler TenTicks;
        /// <summary>
        /// Occures when 100 milliseconds has elapsed.
        /// </summary>
        public static event EventHandler HundredTicks;
        /// <summary>
        /// Occures when 1 second has elapsed.
        /// </summary>
        public static event EventHandler ThousandTicks;
    }
}