using System.Collections.Generic;

namespace Structure
{
    public class LimitedSizeList<T> : List<T>
    {
        private readonly int _limit;

        public LimitedSizeList(int limit)
        {
            _limit = limit;
        }

        public new void Add(T item)
        {
            base.Add(item);
            if (Count >= _limit)
                RemoveAt(0);
        }
    }
}