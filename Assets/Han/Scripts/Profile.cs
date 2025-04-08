using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    public Image image;
    public Text nameTxt;

    public void SetProfile()
    {
        image.sprite = Resources.Load<Sprite>($"fail");
        nameTxt.text = "";
    }

    public void SetProfile(int idx, string name)
    {
        image.sprite = Resources.Load<Sprite>($"profile{idx}");
        nameTxt.text = name;
    }
}
