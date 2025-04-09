using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleEffect : MonoBehaviour
{
    public float lifetime = 1.0f;

    private void OnEnable()
    {
        // 매번 활성화될 때 비활성화를 지연동작시킴
        Invoke(nameof(ReleaseSparkle), lifetime);
    }

    private void OnDisable()
    {
        // 비활성화될 때 Invoke 예약 제거
        CancelInvoke(nameof(ReleaseSparkle));
    }

    private void ReleaseSparkle()
    {
        SparkleObjectPoolManager.instance.ReleaseObject(gameObject);
    }
}