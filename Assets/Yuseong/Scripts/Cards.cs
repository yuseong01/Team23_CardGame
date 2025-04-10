using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public (int, int) key;

    public bool isOpen;

    public Vector2 size;

    public RectTransform rectTransform;

    public Image frontImage;
    public Image backImage;
    public Image selectImage;

    public Button backObjectButton;

    public Animator anim;


    private void Awake()
    {
        backObjectButton.onClick.AddListener(OnSelectCard);
    }

    public void Init((int, int) key, MemberSpritesContainer memberSpriteContainer)
    {
        this.key = key;

        frontImage.sprite = memberSpriteContainer.spritesList[key.Item1][key.Item2];
    }

    public void OnSelectCard()
    {
        OpenCard();

        selectImage.gameObject.SetActive(true);


        if (CardGameController.instance.firstCard == null)
        {
            CardGameController.instance.firstCard = this;
        }
        else
        {
            CardGameController.instance.secondCard = this;

            StartCoroutine(CardGameController.instance.CardMatched());
        }
    }

    public void OpenCard()
    {
        anim.SetTrigger("PopTrigger");

        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);

        isOpen = true;
    }

    public void CloseCard()
    {
        anim.SetTrigger("PopTrigger");

        rectTransform.localScale = Vector2.one;

        frontImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(true);

        selectImage.gameObject.SetActive(false);

        isOpen = false;
    }


    public void OnSuccess()
    {
        anim.SetTrigger("PopTrigger");

        selectImage.gameObject.SetActive(false);
    }

}


