using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
    public int clearedLevel = 0;

    private MemberSprite member;

    float time = 0.0f;
    float timeLimit;
    bool isPlaying = false;
    string levelkey = "LEVEL";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        audioSource = GetComponent<AudioSource>();

        //프레임조정
        Application.targetFrameRate = 60;
        //PlayerPrefs에 저장된게 있으면 clearedLevel 초기화
        if (PlayerPrefs.HasKey(levelkey)) { clearedLevel = PlayerPrefs.GetInt(levelkey); }

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


    //버튼에서 작동시킬때
    //GameManager.instance.StartGame(level);
    public void StartGame(int level)
    {
        //카드 배치 로직
        /*
        remainCard = cardPlacementController.InitStartGame(level, member);
        */
        time = 0.0f;
        Time.timeScale = 1.0f;
        timeBar.fillAmount = 0.0f;

        SetTimeLimit(level, remainCard);  // 레벨에 따라 timeLimit 설정

        isPlaying = true;
        StartCoroutine(TimeFlowCoroutine()); // 시간 흐름 시작
    }

    //레벨별 제한시간 설정
    public void SetTimeLimit(int level, int remainCard)
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
            float progress = 1- Mathf.Clamp01(time / timeLimit);
            timeBar.fillAmount = progress;
            //타임바의 비율에 따라 타임바 색 조정
            TimeBarColor(progress);

            if ((progress == 0) || (remainCard == 0))
            {
                GameOver();
                yield break; // 코루틴 종료
            }

            yield return null;
        }
    }

    private void GameOver()
    {
        if(remainCard == 0)
        {
            //성공시
            //성공UI출력******************************************************************

            //레벨 갱신, PlayerPrefs에 저장
            if (clearedLevel < level)
            {
                clearedLevel = level;
                PlayerPrefs.SetInt(levelkey, clearedLevel);
            }
        }
        else
        {
            //실패시
            //실패UI출력******************************************************************
        }
        isPlaying = false;
        Time.timeScale = 0.0f;

        Debug.Log("게임 종료");
    }

    //타임바의 비율에 따라 타임바 색 조정
    public void TimeBarColor(float progress)
    {
        if (progress > 0.5f)
        {
            // 연두색 → 노란색
            float t = (progress - 0.5f) / 0.5f;
            timeBar.color = Color.Lerp(Color.yellow, startColor, t);
        }
        else
        {
            // 노란색 → 빨간색
            float t = progress / 0.5f;
            timeBar.color = Color.Lerp(Color.red, Color.yellow, t);
        }
    }

    public void Matched()
    {
        //두 카드가 같다면
        if (isPlaying/*보호수준때문에 일단 bool 변수로 대체 firstCard.idx == secondCard.idx*/)
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
            /*
            //카드 다시 뒤집기
            firstCard.ReflipCard();
            secondCard.ReflipCard();
            */
        }
        //첫번째, 두번째 카드 변수 null로 초기화
        firstCard = null;
        secondCard = null;
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