using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    [Header("UI")]
    public Image frameImage;
    public Image image;
    public Text nameTxt;

    [Space(10)]
    [Header("ClearImage")]
    [SerializeField] private Sprite[] profileImages;

    [Space(10)]
    [Header("FailImage")]
    [SerializeField] private Sprite[] failImages;

    [Space(10)]
    [Header("Shaking")]
    [SerializeField] private float shakeAngle = 1f; // 흔들림 각도 범위
    [SerializeField] private float shakeSpeed = 2f; // 흔들림 속도


    private Quaternion frameImageOriginalRotation; // 기존 Rotation

    public void Start()
    {
        frameImageOriginalRotation = frameImage.rectTransform.localRotation;
    }

    // Fail
    public void SetProfile(Color color)
    {
        frameImage.color = color;
        int randomIdx = Random.Range(0, failImages.Length);
        image.sprite = failImages[randomIdx];
        nameTxt.text = "";
    }

    // Clear - 2번 이상 호출됐을 때 -> 배경 색상 변경 x
    public void SetProfile(int idx, string name)
    {
        image.sprite = profileImages[idx];
        nameTxt.text = name;
    }

    // Clear 처음 호출될 때 -> 배경 색상 변경
    public void SetProfile(int idx, string name, Color color)
    {
        frameImage.color = color;
        image.sprite = profileImages[idx];
        nameTxt.text = name;
        //StartCoroutine(ShakeImage());
    }

    public IEnumerator ShakeImage()
    {
        while (true)
        {
            float angle = Mathf.Sin(Time.time * shakeSpeed) * shakeAngle; // 시간에 따라 
            frameImage.rectTransform.localRotation = frameImageOriginalRotation * Quaternion.Euler(0f, 0f, angle); ;
            yield return null;
        }
    }
}
