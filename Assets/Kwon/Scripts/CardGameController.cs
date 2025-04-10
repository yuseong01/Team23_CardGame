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

        //�� ī�尡 ���ٸ�
        if (firstCard.key.Item1 == secondCard.key.Item1 && firstCard.key.Item2 == secondCard.key.Item2)
        {
            //���� ����Ŭ��
            soundManager.PlayFlipSuccessSound();

            //��������Ʈ
            ShowMatchEffect(firstCard);
            ShowMatchEffect(secondCard);
            firstCard.OnSuccess();
            secondCard.OnSuccess();

            //���� ī�� �� ����
            remainCard -= 2;
        }
        //�� ī�尡 ���� �ʴٸ�
        else
        {
            //���� ����Ŭ��
            soundManager.PlayFlipFailSound();

            //�г�Ƽ
            GameManager.instance.AddMatchFailPenalty();

            //ī�� �ٽ� ������
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        //ù��°, �ι�° ī�� ���� null�� �ʱ�ȭ
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
