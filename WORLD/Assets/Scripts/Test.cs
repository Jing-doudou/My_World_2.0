using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        Block block = collision.gameObject.GetComponentInParent<Block>();
        block.Hp = 0;
    }
}
