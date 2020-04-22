using System.Collections;
using System.Collections.Generic;
using Mogoo.Base;
using Mogoo.Mono;
using UnityEngine;
using UnityEngine.Events;

public class ResManager : Singleton<ResManager>
{
    public T Load<T>(string path) where T:Object
    {
        T t = Resources.Load<T>(path);
        if (t is GameObject)
        {
            return GameObject.Instantiate(t);
        }
        else
        {
            return t;
        }
    }

    public void LoadAsync<T>(string path, UnityAction<T> func) where T:Object
    {
        MonoManager.Instance.StartCoroutine(loadAsync(path, func));
    }

    private IEnumerator loadAsync<T>(string path, UnityAction<T> func) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync(path);
        yield return r;
        if (r.asset is GameObject)
        {
            func(GameObject.Instantiate(r.asset) as T);
        }
        else
        {
            func(r.asset as T);
        }
    }
}
