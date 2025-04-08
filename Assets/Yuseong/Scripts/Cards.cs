using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public Image frontImage;
    public Image backImage;

    public Animator anim;

    public (int, int) key;

    public int idx;

    public void Init((int, int) key, Sprite targetSprite)
    {
        this.key = key;
        backImage.sprite = targetSprite;
    }

    public void Setting(int idx)
    {
        //number을 매개변수로 카드의 idx초기화, 스프라이트 또한 변경
        this.idx = idx;
        //frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);
    }
}
