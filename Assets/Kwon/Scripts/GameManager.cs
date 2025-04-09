using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float time = 0.0f;
    private float timeLimit;
    private bool isPlaying = false;
    private string levelkey = "LEVEL";

    private MemberSpritesContainer memberSpritesContainer;
    //private AudioSource audioSource;

    [SerializeField] private EndGameUI endCardGameUI;
    [SerializeField] private CardPlacementController cardPlacementController;
    [SerializeField] private StageController stageController;
    [SerializeField] private Cards card;
    [SerializeField] private Image timeBar;
    [SerializeField] private GameObject matchEffect;
    [SerializeField] private Color startColor;
    [SerializeField] private int level = 0;
    [SerializeField] private int remainCard = 16;
    [SerializeField] private Sprite[] stageIconSprites;
    [SerializeField] private Sprite[] members1;
    [SerializeField] private Sprite[] members2;
    [SerializeField] private Sprite[] members3;
    //[SerializeField] private AudioClip success;
    //[SerializeField] private AudioClip failure;

    [Space(10f)]
    public GameObject endPanel;
    public Cards firstCard;
    public Cards secondCard;
    public int clearedLevel = 0;
    public int maxStageLevel;
    public bool isTouchStartScreen;
  


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
        //audioSource = GetComponent<AudioSource>();

        //프레임조정
        Application.targetFrameRate = 60;
        //PlayerPrefs에 저장된게 있으면 clearedLevel 초기화
        //if (PlayerPrefs.HasKey(levelkey)) { clearedLevel = PlayerPrefs.GetInt(levelkey); }

        memberSpritesContainer = new MemberSpritesContainer();
        memberSpritesContainer.Init(5);
        memberSpritesContainer.AddSprites(members1);
        memberSpritesContainer.AddSprites(members2);
        memberSpritesContainer.AddSprites(members3);
    }

    private void Start()
    {
        stageController.StageButtonCreate(stageIconSprites);

        stageController.UpdateButtonLockImage(clearedLevel);
    }

    //버튼에서 작동시킬때
    //GameManager.instance.StartGame(level);
    public void StartCardGame(int level)
    {
        this.level = level;

        //카드 배치 로직
        List<Cards> remain = cardPlacementController.StartCardPlacement(level, memberSpritesContainer, card);
        remainCard = remain.Count;

        time = 0.0f;
        Time.timeScale = 1.0f;
        timeBar.fillAmount = 0.0f;

        SetTimeLimit(level);    // 레벨에 따라 timeLimit 설정

        isPlaying = true;
        StartCoroutine(TimeFlowCoroutine());    // 시간 흐름 시작

        stageController.OnStartCardGame();
    }

    //레벨별 제한시간 설정
    public void SetTimeLimit(int level)
    {
        timeLimit = 20f + (level * 10f);
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

            //테스트용 성공 커맨드
            if(Input.GetKeyDown(KeyCode.D))
            {
                remainCard = 0;
            }
            //테스트용 실패 커맨드
            if (Input.GetKeyDown(KeyCode.F))
            {
                time = timeLimit;
            }

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
        cardPlacementController.EndCardPalcement();

        if(remainCard == 0)
        {
            //성공시 레벨 갱신, PlayerPrefs에 저장
            if (clearedLevel < level)
            {
                clearedLevel = level;
                //PlayerPrefs.SetInt(levelkey, clearedLevel);
            }
            endCardGameUI.OpenWinUI(time);
        }
        else
        {
            //실패시
            endCardGameUI.OpenFailUI();
        }
        isPlaying = false;

        cardPlacementController.EndCardPalcement();
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
        if (firstCard.key.Item1 == secondCard.key.Item1 && firstCard.key.Item2 == secondCard.key.Item2)
        {
            //성공 사운드클립
            //audioSource.PlayOneShot(success);
            //카드는 앞면으로 놔둠, 정답이펙트
            ShowMatchEffect(firstCard);
            ShowMatchEffect(secondCard);

            //남은 카드 수 감소
            remainCard -= 2;
        }
        //두 카드가 같지 않다면
        else
        {
            //실패 사운드클립
            //audioSource.PlayOneShot(failure);

            //카드 다시 뒤집기
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        //첫번째, 두번째 카드 변수 null로 초기화
        firstCard = null;
        secondCard = null;
    }
    
    private void ShowMatchEffect(Cards card)
    {
        GameObject effect = Instantiate(matchEffect, card.transform);
        effect.transform.localPosition = Vector3.zero;
    }
    
    public void SetNewStageSetting()
    {
        endCardGameUI.gameObject.SetActive(false);

        stageController.OnEndCardGame(clearedLevel);
    }
}

