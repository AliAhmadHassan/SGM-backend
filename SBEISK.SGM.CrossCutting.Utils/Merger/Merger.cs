using System;
using System.Collections.Generic;
using System.Linq;

namespace SBEISK.SGM.CrossCutting.Utils.Merger
{
    public class Merger<T>
    {
        private readonly Func<T, T, bool> identityCheck;
        private readonly Func<T, T, bool> equalityCheck;

        public Merger(Func<T, T, bool> identityCheck, Func<T, T, bool> equalityCheck)
        {
            this.identityCheck = identityCheck;
            this.equalityCheck = equalityCheck;
        }

        public static MergeResult<T> Merge(Func<T, T, bool> identityCheck, Func<T, T, bool> equalityCheck, IList<T> originalList, IList<T> otherList)
        {
            Merger<T> instance = new Merger<T>(identityCheck, equalityCheck);
            MergeResult<T> returnValue = instance.Merge(originalList, otherList);
            return returnValue;
        }


        public MergeResult<T> Merge(IList<T> originalList, IList<T> otherList)
        {
            if (originalList != null)
            {
                originalList = originalList.Where(it => it != null).ToList();
            }

            if (otherList != null)
            {
                otherList = otherList.Where(it => it != null).ToList();
            }

            MergeResult<T> result = new MergeResult<T>();
            result.ItemsToDelete = new List<T>();
            result.ItemsToInsert = null;
            result.ItemsToUpdate = new List<ItemToUpdate<T>>();

            if (originalList != null)
            {
                foreach (T originalItem in originalList)
                {
                    T otherItem = otherList.FirstOrDefault(it => identityCheck(it, originalItem));
                    if (otherItem == null)
                    {
                        result.ItemsToDelete.Add(originalItem);
                    }
                    else if (!equalityCheck(originalItem, otherItem))
                    {
                        result.ItemsToUpdate.Add(new ItemToUpdate<T>() { Original = originalItem, Modified = otherItem });
                    }
                }
            }

            if (otherList != null)
            {
                result.ItemsToInsert = otherList.Where(it => !originalList.Any(it2 => this.identityCheck(it, it2))).ToList();
            }
            else
            {
                result.ItemsToInsert = new List<T>();
            }

            return result;
        }

    }

    public class MergeResult<T>
    {
        public IList<T> ItemsToInsert { get; set; }
        public IList<ItemToUpdate<T>> ItemsToUpdate { get; set; }
        public IList<T> ItemsToDelete { get; set; }
    }

    public class ItemToUpdate<T>
    {
        public T Original { get; set; }
        public T Modified { get; set; }
    }
}
