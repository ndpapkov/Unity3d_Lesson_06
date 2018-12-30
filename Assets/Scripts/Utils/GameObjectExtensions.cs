using UnityEngine;

namespace Utils
{
    public static class GameObjectExtensions
    {
        public static T GetComponentAlways<T>(this GameObject gameObject) 
            where T : MonoBehaviour
        {
            var component = gameObject.GetComponent<T>();
            
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
    }
}