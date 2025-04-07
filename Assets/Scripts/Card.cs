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
        //number을 매개변수로 카드의 idx초기화, 스프라이트 sprite**로 변경
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"sprite{idx}");
    }

    public void FlipCard()
    {
        //앞면 활성화, 뒷면 비활성화
        front.SetActive(true);
        back.SetActive(false);

        //firstCard가 비었다면 firstCard에 정보 저장
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        //firstCard가 비어있지 않다면 secondCard에 정보 저장 & Matched 함수 호출
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }
    }
    //정답일경우 - ?


    //오답일경우 - 카드 다시뒤집기
    public void ReflipCard()
    {
        //카드 확인 여유를 위해 지연동작
        Invoke(nameof(ReflipCardInvoke), 0.5f);
    }

    public void ReflipCardInvoke()
    {
        //뒤집는 애니메이션

        //뒷면 활성화, 앞면 비활성화
        front.SetActive(false);
        back.SetActive(true);
    }
}
