using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameController : MonoBehaviour
{
    public static CardGameController instance;

    int remainCard;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private CardPlacementController cardPlacementController;
    
    public Cards firstCard;
    public Cards secondCard;

    public Image touchBlockPanel;

    public ShuffleCardPlaceAnimation shuffleCardPlaceAnimation;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        shuffleCardPlaceAnimation = new(cardPlacementController);
    }

    public IEnumerator PlayCardShuffle()
    {
        while(GameManager.instance.isPlaying)
        {
            yield return new WaitForSeconds(6f);

            StartCoroutine(shuffleCardPlaceAnimation.Play());
        }
    }


    public IEnumerator CardMatched()
    {
        touchBlockPanel.enabled = true;

        yield return new WaitForSeconds(0.5f);

        //두 카드가 같다면
        if (firstCard.key.Item1 == secondCard.key.Item1 && firstCard.key.Item2 == secondCard.key.Item2)
        {
            //성공 사운드클립
            soundManager.PlayFlipSuccessSound();

            //정답이펙트
            ShowMatchEffect(firstCard);
            ShowMatchEffect(secondCard);
            firstCard.OnSuccess();
            secondCard.OnSuccess();

            //남은 카드 수 감소
            remainCard -= 2;
        }
        //두 카드가 같지 않다면
        else
        {
            //실패 사운드클립
            soundManager.PlayFlipFailSound();

            //패널티
            GameManager.instance.AddMatchFailPenalty();

            //카드 다시 뒤집기
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        //첫번째, 두번째 카드 변수 null로 초기화
        firstCard = null;
        secondCard = null;

        touchBlockPanel.enabled = false;
    }

    private void ShowMatchEffect(Cards card)
    {
        if (SparkleObjectPoolManager.instance != null)
        {
            GameObject sparkle = SparkleObjectPoolManager.instance.GetObject(card.transform.position);
        }
    }
}
