using System;
using System.Windows.Threading;


namespace WindowsClassLibrary
{
    public static class Timer
    {
        static Timer()
        {
            var dispatcherTimer = new DispatcherTimer(
                TimeSpan.FromMilliseconds(1),
                DispatcherPriority.Background,
                (sender, e) => { Tick?.Invoke(sender, e); },
                Dispatcher.CurrentDispatcher);
        }

        public static event EventHandler Tick;
    }
}
