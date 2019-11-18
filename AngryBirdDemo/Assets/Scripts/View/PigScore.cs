using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigScore : MonoBehaviour
{
    public float lifetime = 0.5f;
    private void OnDisable()
    {
        CancelInvoke("Dead");
    }
    private void OnEnable()
    {
        Invoke("Dead", lifetime);
    }

    private void Dead()
    {
        Debug.Log("回收");
        ObjectPool.objectPool.PushObject(gameObject);
    }
}
