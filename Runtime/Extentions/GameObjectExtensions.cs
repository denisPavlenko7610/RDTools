using UnityEngine;

namespace RDragonTools.Extensions
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent<T>(out T requestedComponent))
            {
                return requestedComponent;
            }
            
            return gameObject.AddComponent<T>();
        }
    }
}
