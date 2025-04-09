using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleEffect : MonoBehaviour
{
    public float lifetime = 1.0f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
