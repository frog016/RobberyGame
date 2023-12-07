using Structure.Netcode;
using UnityEngine.Pool;

namespace Creation.Pool
{
    public abstract class PoolElement<T> : ServerBehaviour where T : class
    {
        protected IObjectPool<T> Pool { get; private set; }

        public void Initialize(IObjectPool<T> pool)
        {
            Pool = pool;
        }

        public virtual void OnPoolGet() { }
        public virtual void OnPoolRelease() { }
    }
}