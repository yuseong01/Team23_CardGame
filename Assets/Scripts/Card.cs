using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer frontImage;
    public GameObject front;
    public GameObject back;
    public int idx;

    public void Setting(int number)
    {
        //number�� �Ű������� ī���� idx�ʱ�ȭ, ��������Ʈ sprite**�� ����
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"sprite{idx}");
    }

    public void FlipCard()
    {
        //�ո� Ȱ��ȭ, �޸� ��Ȱ��ȭ
        front.SetActive(true);
        back.SetActive(false);

        //firstCard�� ����ٸ� firstCard�� ���� ����
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        //firstCard�� ������� �ʴٸ� secondCard�� ���� ���� & Matched �Լ� ȣ��
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }
    }
    //�����ϰ�� - ?


    //�����ϰ�� - ī�� �ٽõ�����
    public void ReflipCard()
    {
        //ī�� Ȯ�� ������ ���� ��������
        Invoke(nameof(ReflipCardInvoke), 0.5f);
    }

    public void ReflipCardInvoke()
    {
        //������ �ִϸ��̼�

        //�޸� Ȱ��ȭ, �ո� ��Ȱ��ȭ
        front.SetActive(false);
        back.SetActive(true);
    }
}
