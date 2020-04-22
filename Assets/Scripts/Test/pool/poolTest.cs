using Mogoo.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolManager.Instance.Spawn("Test/Cube", (GameObject o) =>
            {
                Debug.Log("load cube");
            });
        }

        if (Input.GetMouseButtonDown(1))
        {
            PoolManager.Instance.Spawn("Test/Sphere", (GameObject o) =>
            {
                Debug.Log("load sphere");
            });
        }
    }
}
