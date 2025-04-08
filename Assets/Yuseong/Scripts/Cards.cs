using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public GameObject front;
    public GameObject back;

    public Animator anim;
    int idx;


    public void Setting(int idx)
    {
        //number을 매개변수로 카드의 idx초기화, 스프라이트 또한 변경
        this.idx = idx;
        //frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);
    }
}
