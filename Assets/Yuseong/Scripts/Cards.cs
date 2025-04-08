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
        //number�� �Ű������� ī���� idx�ʱ�ȭ, ��������Ʈ ���� ����
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
