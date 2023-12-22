using System;
using System.Collections;
using System.Collections.Generic;

namespace Structure
{
    public class ObservableCollection<T> : ICollection<T>
    {
        public int Count => Container.Count;
        public bool IsReadOnly => false;

        public event Action<T> Added;
        public event Action<T> Removed;

        protected readonly List<T> Container = new();

        public virtual void Add(T item)
        {
            Container.Add(item);
            Added?.Invoke(item);
            OnAdded(item);
        }

        public virtual bool Remove(T item)
        {
            if (Container.Remove(item) == false)
                return false;

            Removed?.Invoke(item);
            OnRemoved(item);

            return true;
        }

        public bool Contains(T item)
        {
            return Container.Contains(item);
        }

        public void Clear()
        {
            Container.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Container.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Container.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void OnAdded(T item) { }
        protected virtual void OnRemoved(T item) { }
    }
}