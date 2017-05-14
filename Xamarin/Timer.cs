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
        }

        /// <summary>
        /// Occures when one millisecond has elapsed.
        /// </summary>
        public static event EventHandler Tick;
    }
}