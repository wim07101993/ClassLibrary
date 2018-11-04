﻿using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Interop;

namespace Library.WPF.Behaviors
{
    public class HideCloseButtonBehavior : Behavior<Window>
    {
        #region bunch of native methods

        private const int GwlStyle = -16;
        private const int WsSysMenu = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += OnLoaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= OnLoaded;
            base.OnDetaching();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var handle = new WindowInteropHelper(AssociatedObject).Handle;
            SetWindowLong(handle, GwlStyle, GetWindowLong(handle, GwlStyle) & ~WsSysMenu);
        }
    }
}
