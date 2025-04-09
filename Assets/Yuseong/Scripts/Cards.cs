using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public Image frontImage;

    public Button backImageButton;

    public Animator anim;

    public (int, int) key;

    public void Init((int, int) key, Sprite targetSprite)
    {
        this.key = key;

        frontImage.sprite = targetSprite;

        backImageButton.onClick.AddListener(OpenCard);
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
        frontImage.gameObject.SetActive(false);
        backImageButton.gameObject.SetActive(true);
    }
}
