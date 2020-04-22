using Mogoo.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resTest : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ResManager.Instance.Load<GameObject>("Test/Cube");
        }

        if (Input.GetMouseButtonDown(1))
        {
            ResManager.Instance.LoadAsync<GameObject>("Test/Cube", (GameObject obj) =>
            {
                Debug.Log("GetMouseButtonDown 1");
            });
        }
    }
}
