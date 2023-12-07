using System;
using UnityEngine;

namespace Creation.Factory
{
    public class UnityFactory : IFactory
    {
        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        public object Create(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public TComponent CreateForComponent<TComponent>(TComponent prefab, Vector3 position = default,
            Quaternion rotation = default) where TComponent : Component
        {
            return Instantiate(prefab, position, rotation).GetComponent<TComponent>();

        }

        public GameObject CreateGameObject(GameObject prefab, Vector3 position = default, Quaternion rotation = default)
        {
            return Instantiate(prefab, position, rotation);
        }

        private static T Instantiate<T>(T prefab, Vector3 position = default, Quaternion rotation = default) where T : UnityEngine.Object
        {
            return UnityEngine.Object.Instantiate(prefab, position, rotation, null);
        }
    }
}