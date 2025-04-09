using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public RectTransform rectTransform;

    public Image frontImage;

    public Button backImageButton;

    public Animator anim;

    public (int, int) key;

    private void Awake()
    {
        backImageButton.onClick.AddListener(OpenCard);
    }

    public void Init((int, int) key, Sprite targetSprite)
    {
        this.key = key;

        frontImage.sprite = targetSprite;
    }

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);

        frontImage.gameObject.SetActive(true);
        backImageButton.gameObject.SetActive(false);

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
        backImageButton.gameObject.SetActive(true);
    }
}


