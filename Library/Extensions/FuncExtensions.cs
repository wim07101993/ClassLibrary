using System;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class FuncExtensions
    {
        public static async Task<TOut> RunAsync<TOut>(this Func<TOut> func)
        {
            var result = default(TOut);
            await new Action(() => result = func()).RunAsync();
            return result;
        }

        public static async Task<TOut> RunAsync<TIn, TOut>(this Func<TIn, TOut> func, TIn arg1)
        {
            var result = default(TOut);
            await new Action(() => result = func(arg1)).RunAsync();
            return result;
        }

        public static async Task<TOut> RunAsync<TIn1, TIn2, TOut>(this Func<TIn1, TIn2, TOut> func,
            TIn1 arg1, TIn2 arg2)
        {
            var result = default(TOut);
            await new Action(() => result = func(arg1, arg2)).RunAsync();
            return result;
        }

        public static async Task<TOut> RunAsync<TIn1, TIn2, TIn3, TOut>(this Func<TIn1, TIn2, TIn3, TOut> func,
            TIn1 arg1, TIn2 arg2, TIn3 arg3)
        {
            var result = default(TOut);
            await new Action(() => result = func(arg1, arg2, arg3)).RunAsync();
            return result;
        }

        public static async Task<TOut> RunAsync<TIn1, TIn2, TIn3, TIn4, TOut>(
            this Func<TIn1, TIn2, TIn3, TIn4, TOut> func, TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4)
        {
            var result = default(TOut);
            await new Action(() => result = func(arg1, arg2, arg3, arg4)).RunAsync();
            return result;
        }

        public static async Task<TOut> RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(
            this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> func, TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5)
        {
            var result = default(TOut);
            await new Action(() => result = func(arg1, arg2, arg3, arg4, arg5)).RunAsync();
            return result;
        }

        public static async Task<TOut> RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut>(
            this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TOut> func,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6)
        {
            var result = default(TOut);
            await new Action(() => result = func(arg1, arg2, arg3, arg4, arg5, arg6)).RunAsync();
            return result;
        }

        public static async Task<TOut> RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut>(
            this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TOut> func,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7)
        {
            var result = default(TOut);
            await new Action(() => result = func(arg1, arg2, arg3, arg4, arg5, arg6, arg7)).RunAsync();
            return result;
        }

        public static async Task<TOut> RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut>(
            this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TOut> func,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TIn8 arg8)
        {
            var result = default(TOut);
            await new Action(() => result = func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)).RunAsync();
            return result;
        }

        public static async Task<TOut> RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TOut>(
            this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TOut> func,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TIn8 arg8, TIn9 arg9)
        {
            var result = default(TOut);
            await new Action(() => result = func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)).RunAsync();
            return result;
        }

        public static async Task<TOut> RunAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TOut>(
            this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10, TOut> func,
            TIn1 arg1, TIn2 arg2, TIn3 arg3, TIn4 arg4, TIn5 arg5, TIn6 arg6, TIn7 arg7, TIn8 arg8, TIn9 arg9,
            TIn10 arg10)
        {
            var result = default(TOut);
            await new Action(() => result = func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10))
                .RunAsync();
            return result;
        }
    }
}