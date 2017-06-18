using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Portable.Interfaces;


namespace ClassLibrary.Portable.Extensions
{
    public static class ModelWithIDExtensions
    {
        /// <summary>
        /// Finds the index of an item by comparing the id's.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="itemToFindIndexOf"></param>
        /// <returns></returns>
        public static int FindIndexOfModelWithID<T>(this IList<T> This, T itemToFindIndexOf)
            where T : IModelWithID
            => This.FindIndex(x => x.CompareToModelWithIDs(itemToFindIndexOf));

        /// <summary>
        /// Checks if the <see cref="itemToCompareWith"/>'s id equals this models id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="itemToCompareWith"></param>
        /// <returns></returns>
        public static bool CompareToModelWithIDs<T>(this T This, T itemToCompareWith)
            where T : IModelWithID
            => This?.ID == itemToCompareWith?.ID;

        /// <summary>
        /// Checks if this list contains an item with as id, id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ContainsID<T>(this IEnumerable<T> This, int id)
            where T : IModelWithID
            => This.Any(x => Equals(x.ID, id));

        /// <summary>
        /// Checks if this list contains the item <see cref="itemFind"/> by looking for the id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="itemFind"></param>
        /// <returns></returns>
        public static bool ContainsModelWithWithID<T>(this IEnumerable<T> This, T itemFind)
            where T : IModelWithID
            => This.Any(x => CompareToModelWithIDs(x, itemFind));

        /// <summary>
        /// Removes the item <see cref="itemToRemove"/> by looking for the id. If the item is not found, false is returned, else true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="itemToRemove"></param>
        /// <returns></returns>
        public static bool RemoveModelWithID<T>(this IList<T> This, T itemToRemove)
            where T : IModelWithID
        {
            var index = This.FindIndexOfModelWithID(itemToRemove);
            
            if (index == -1)
                return false;

            This.RemoveAt(index);
            return true;
        }
    }
}