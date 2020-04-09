using System.Collections;
using System.Collections.Generic;
using Mogoo.Base;
using Mogoo.Pool;
using UnityEngine;

public class TestMain : MonoBehaviour
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
            ObjectPoolManager.Instance.Spawn("Test/Cube");
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            ObjectPoolManager.Instance.Spawn("Test/Sphere");
        }
    }
}
