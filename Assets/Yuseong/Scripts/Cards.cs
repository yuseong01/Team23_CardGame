using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public (int, int) key;

    public Vector2 size;

    public RectTransform rectTransform;

    public Image frontImage;
    public Image backImage;

    public Button backObjectButton;

    public Animator anim;


    private void Awake()
    {
        backObjectButton.onClick.AddListener(OpenCard);
    }

    public void Init((int, int) key, Sprite memberSprite)
    {
        this.key = key;

        frontImage.sprite = memberSprite;
    }

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);

        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);

        if(GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
        }
        else
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }
    }

    public void CloseCard()
    {
        Invoke(nameof(CloseCardInvoke), 0.5f);
    }

    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);

        rectTransform.localScale = Vector2.one;


        frontImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(true);
    }
}


