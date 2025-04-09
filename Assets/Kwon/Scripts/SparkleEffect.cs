using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleEffect : MonoBehaviour
{
    public float lifetime = 1.0f;

    private void OnEnable()
    {
        // �Ź� Ȱ��ȭ�� �� ��Ȱ��ȭ�� �������۽�Ŵ
        Invoke(nameof(ReleaseSparkle), lifetime);
    }

    private void OnDisable()
    {
        // ��Ȱ��ȭ�� �� Invoke ���� ����
        CancelInvoke(nameof(ReleaseSparkle));
    }

    private void ReleaseSparkle()
    {
        SparkleObjectPoolManager.instance.ReleaseObject(gameObject);
    }
}