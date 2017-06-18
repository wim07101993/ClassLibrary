using System;

namespace ClassLibrary.Portable.Extensions
{
    /// <summary>
    /// Static class for extenstions of the <see cref="Exception"/> class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Creates new string with all the error messages of this Exception seperated by newline and tab.
        /// <para>
        /// message one
        /// \r\n\t message two
        /// \r\n\t message three
        /// ...
        /// </para>
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
        public static string CombineInnerMessagege(this Exception This)
        {
            // check if this is  null
            if (This == null)
                return "";

            // return combined message
            return This.Message + "\r\n\t" + CombineInnerMessagege(This.InnerException);
        }
    }
}
