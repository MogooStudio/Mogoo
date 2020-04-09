using UnityEngine;

namespace Mogoo.Base
{
    /// <summary>
    /// 普通类单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : class, new()
    {
        private static T _instance;

        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    // 加锁
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new T();
                    }
                }
                return _instance;
            }
        }

    }

    /// <summary>
    /// 继承自MonoBehaviour的单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(T)) as T;
                    if (_instance == null)
                    {
                        var obj = new GameObject {hideFlags = HideFlags.HideAndDontSave};
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}
