using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Color startColor;

    public static GameManager Instance;
    public Image timeBar;
    //public GameObject endPanel;
    //public Card firstCard;
    //public Card secondCard;
    public int remainCard = 16;
    public int level;

    AudioSource audioSource;
    public AudioClip success;
    public AudioClip fail;

    float time;
    float timeLimit;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //프레임조정
        Application.targetFrameRate = 60;
        
        //카드 생성단계에서 레벨초기화 호출해줘야함 
        SetTimeLimit(remainCard, level);
        

        time = 0.0f;
        Time.timeScale = 1.0f;
        timeBar.fillAmount = 0.0f;
    }

    void Update()
    {
        //시간이 지나갈수록 타임바가 차오름
        time += Time.deltaTime;
        float progress = Mathf.Clamp01(time / timeLimit);
        timeBar.fillAmount = progress;

        //타임바의 비율에 따라 타임바 색 조정
        TimeBarColor(progress);

        if (time >= timeLimit)
        {
            Time.timeScale = 0.0f;
            //endPanel.SetActive(true);
        }
    }

    //레벨별 제한시간 설정
    public void SetTimeLimit(int remainCard,  int level)
    {
        this.remainCard = remainCard;
        timeLimit = 20f + (level * 10f);
    }

    //타임바의 비율에 따라 타임바 색 조정
    public void TimeBarColor(float progress)
    {
        if (progress < 0.5f)
        {
            // 연두색 → 노란색
            float t = progress / 0.5f;
            timeBar.color = Color.Lerp(startColor, Color.yellow, t);
        }
        else
        {
            // 노란색 → 빨간색
            float t = (progress - 0.5f) / 0.5f;
            timeBar.color = Color.Lerp(Color.yellow, Color.red, t);
        }
    }

    public void Matched()
    {
        /* 일단 전부 주석처리
        //두 카드가 같다면
        if (firstCard.idx == secondCard.idx)
        {
            //성공 사운드클립
            audioSource.PlayOneShot(success);
            //카드는 앞면으로 놔둠 / 별도의 정답이펙트?

            //남은 카드 수 감소
            remainCard -= 2;
        }
        //두 카드가 같지 않다면
        else
        {
            //실패 사운드클립
            audioSource.PlayOneShot(fail);
            //카드 다시 뒤집기
            firstCard.ReflipCard();
            secondCard.ReflipCard();
        }
        //첫번째, 두번째 카드 변수 null로 초기화
        firstCard = null;
        secondCard = null;
        */
    }
}
