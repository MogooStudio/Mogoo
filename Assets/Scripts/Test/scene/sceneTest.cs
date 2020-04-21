using System.Collections;
using System.Collections.Generic;
using Mogoo.Mono;
using UnityEngine;

public class sceneTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartLoadScene", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartLoadScene()
    {
        SceneController.Instance.LoadScene("Game", OnLoadSceneEnd);
    }

    private void OnLoadSceneEnd()
    {
        print("load end");
    }
}
