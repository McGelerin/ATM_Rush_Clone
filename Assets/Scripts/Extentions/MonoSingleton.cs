using UnityEngine;

namespace Extentions
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static  T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(T)) as T;
                }

                return _instance;
            }
        }

        
    }
}