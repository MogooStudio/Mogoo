using UnityEngine;

namespace Mogoo.Utils
{
    /// <summary>
    /// 普通类单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : class, new()
    {
        private static T instance;

        static readonly object syncLock = new object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // 加锁
                    lock (syncLock)
                    {
                        if (instance == null)
                            instance = new T();
                    }
                }
                return instance;
            }
        }

    }

    /// <summary>
    /// 继承自MonoBehaviour的单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}
