using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private CardPlacementController cardPlacementController;
    [SerializeField] private GameObject card;
    [SerializeField] private Color startColor;
    [SerializeField] private Image timeBar;
    [SerializeField] private int level = 0;
    [SerializeField] private int remainCard = 16;
    [SerializeField] private Sprite[] members1;
    [SerializeField] private Sprite[] members2;
    [SerializeField] private Sprite[] members3;

    AudioSource audioSource;
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip failure;

    public GameObject endPanel;
    public Cards firstCard;
    public Cards secondCard;

    private MemberSprite member;

    float time = 0.0f;
    float timeLimit;
    bool isPlaying = false;

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
        //프레임조정
        Application.targetFrameRate = 60;
        member = new MemberSprite();
        member.Init(members1, members2, members3);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void StartGame(int level)
    {
        //카드 배치 로직
        /*
        cardPlacementController.InitStartGame(level, member);
        */
        time = 0.0f;
        Time.timeScale = 1.0f;
        timeBar.fillAmount = 0.0f;

        SetTimeLimit(level);  // 레벨에 따라 timeLimit 설정

        isPlaying = true;
        StartCoroutine(TimeFlowCoroutine()); // 시간 흐름 시작
    }

    //레벨별 제한시간 설정
    public void SetTimeLimit(int level)
    {
        this.remainCard = remainCard;
        timeLimit = 30f + (level * 10f);
    }

    private IEnumerator TimeFlowCoroutine()
    {
        while (isPlaying)
        {
            //시간이 지나갈수록 타임바가 차오름
            time += Time.deltaTime;
            float progress = Mathf.Clamp01(time / timeLimit);
            timeBar.fillAmount = progress;
            //타임바의 비율에 따라 타임바 색 조정
            TimeBarColor(progress);

            if (time >= timeLimit)
            {
                GameOver();
                yield break; // 코루틴 종료
            }

            yield return null;
        }
    }

    private void GameOver()
    {
        isPlaying = false;
        Time.timeScale = 0.0f;

        Debug.Log("게임 종료");
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
            audioSource.PlayOneShot(failure);
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

public class MemberSprite
{
    public Sprite[] cardset1;
    public Sprite[] cardset2;
    public Sprite[] cardset3;

    public void Init(Sprite[] cardset1, Sprite[] cardset2, Sprite[] cardset3)
    {
        this.cardset1 = cardset1;
        this.cardset2 = cardset2;
        this.cardset3 = cardset3;
    }
}