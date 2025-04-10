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
    }
}
