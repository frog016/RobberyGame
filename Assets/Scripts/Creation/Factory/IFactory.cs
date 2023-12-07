using System;
using UnityEngine;

namespace Creation.Factory
{
    public interface IFactory
    {
        T Create<T>();
        object Create(Type type);
        TComponent CreateForComponent<TComponent>(TComponent prefab, Vector3 position = default, Quaternion rotation = default) where TComponent : Component;
        GameObject CreateGameObject(GameObject prefab, Vector3 position = default, Quaternion rotation = default);
    }
}
