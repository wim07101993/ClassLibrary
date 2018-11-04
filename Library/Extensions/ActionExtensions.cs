using System;
using System.Threading.Tasks;

namespace Library.Extensions
{
    public static class ActionExtensions
    {
        public static async Task RunAsync(this Action action)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action();
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }

        public static async Task RunAsync<TIn>(this Action<TIn> action, TIn arg1)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action(arg1);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }

        public static async Task RunAsync<TIn1, TIn2>(this Action<TIn1, TIn2> action, TIn1 arg1, TIn2 arg2)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action(arg1, arg2);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }

        public static async Task RunAsync<TIn1, TIn2, TIn3>(this Action<TIn1, TIn2, TIn3> action,
            TIn1 arg1, TIn2 arg2, TIn3 arg3)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action(arg1, arg2, arg3);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }

        public static async Task RunAsync<TIn1, TIn2, TIn3, TIn4>(this Action<TIn1, TIn2, TIn3, TIn4> action,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action(arg1, arg2, arg3, arg4);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }

        public static async Task RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5>(
            this Action<TIn1, TIn2, TIn3, TIn4, TIn5> action, TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action(arg1, arg2, arg3, arg4, arg5);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }

        public static async Task RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(
            this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6> action,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action(arg1, arg2, arg3, arg4, arg5, arg6);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }

        public static async Task RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(
            this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7> action,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }

        public static async Task RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8>(
            this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8> action,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TIn8 arg8)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }

        public static async Task RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9>(
            this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9> action,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TIn8 arg8, TIn9 arg9)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }

        public static async Task RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10>(
            this Action<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10> action,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TIn8 arg8, TIn9 arg9,
            TIn10 arg10)
        {
            Exception exception = null;
            await Task.Run(
                () =>
                {
                    try
                    {
                        action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                });

            if (exception != null)
                throw exception;
        }
    }
}