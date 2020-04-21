using System.Collections;
using System.Collections.Generic;
using Mogoo.Base;
using Mogoo.Event;
using Mogoo.Mono;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    //同步加载
    public void LoadScene(string sceneName, UnityAction func)
    {
        SceneManager.LoadScene(sceneName);
        func();
    }

    //异步加载
    public void LoadSceneAsync(string sceneName, UnityAction func)
    {
        MonoManager.Instance.StartCoroutine(loadSceneAsync(sceneName, func));
    }

    //异步加载协程
    private IEnumerator loadSceneAsync(string sceneName, UnityAction func)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        while (!ao.isDone)
        {
            EventCenter.Instance.DispatchEvent("load_scene_async_progress", ao.progress);
            yield return ao.progress;
        }
        func();
    }
}
