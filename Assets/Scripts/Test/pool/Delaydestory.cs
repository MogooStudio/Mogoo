using System;
using System.Collections;
using System.Collections.Generic;
using Mogoo.Base;
using Mogoo.Pool;
using UnityEngine;

public class Delaydestory : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("OnDelayDestroy",1);
    }

    private void OnDelayDestroy()
    {
        ObjectPoolManager.Instance.Despawn(this.gameObject.name, this.gameObject);
    }
}
