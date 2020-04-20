using System.Collections;
using System.Collections.Generic;
using Mogoo.Mono;
using UnityEngine;

public class TestUpdate
{
    public TestUpdate()
    {
        MonoManager.Instance.StartCoroutine(print1());
    }

    public void print()
    {
        Debug.Log("111111");
    }

    IEnumerator print1()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("22222");
    }
}

public class monoTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestUpdate test = new TestUpdate();
        MonoManager.Instance.AddUpdateListener(test.print);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
