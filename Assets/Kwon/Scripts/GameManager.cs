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
    private string levelkey = "LEVEL";

    private MemberSpritesContainer memberSpritesContainer;

    [SerializeField] private EndGameUI endCardGameUI;
    [SerializeField] private CardPlacementController cardPlacementController;
    [SerializeField] private StageController stageController;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Cards card;
    [SerializeField] private Image timeBar;
    [SerializeField] private Color startColor;
    [SerializeField] private int level = 0;
    [SerializeField] private int remainCard = 16;
    [SerializeField] private Sprite[] stageIconSprites;
    [SerializeField] private Sprite[] members1;
    [SerializeField] private Sprite[] members2;
    [SerializeField] private Sprite[] members3;

    [Space(10f)]
    public GameObject endPanel;
    public int clearedLevel = 0;
    public int maxStageLevel;
    public bool isTouchStartScreen;
    public bool isPlaying = false;




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

        //프레임조정
        Application.targetFrameRate = 60;
        //PlayerPrefs에 저장된게 있으면 clearedLevel 초기화
        //if (PlayerPrefs.HasKey(levelkey)) {clearedLevel = PlayerPrefs.GetInt(levelkey);}

        memberSpritesContainer = new MemberSpritesContainer();
        memberSpritesContainer.Init(5);
        memberSpritesContainer.AddSprites(members1);
        memberSpritesContainer.AddSprites(members2);
        memberSpritesContainer.AddSprites(members3);

    }

    private void Start()
    {
        //BGM 재생방식 바뀌면 없애도 되지 않을까 싶어요
        soundManager.PlayGameBGM();

        stageController.StageButtonCreate(stageIconSprites);

        stageController.UpdateButtonLockImage(clearedLevel);

        CardGameController.instance.touchBlockPanel.enabled = false;
    }

    private void Update()
    {
        if (isPlaying)
        {
            //시간이 지나갈수록 타임바가 차오름
            time += Time.deltaTime;
            float progress = 1 - Mathf.Clamp01(time / timeLimit);
            timeBar.fillAmount = progress;
            //타임바의 비율에 따라 타임바 색 조정
            TimeBarColor(progress);

            //테스트용 성공 커맨드
            if (Input.GetKeyDown(KeyCode.D))
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
            }
        }
    }
    //레벨별 제한시간 설정
    public void SetTimeLimit(int level)
    {
        timeLimit = 20f + (level * 10f);
    }
    
    //카드 매치 실패 패널티
    public void AddMatchFailPenalty()
    {
        float penaltyAmount = timeLimit * 0.05f;
        time += penaltyAmount;
    }

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

        stageController.OnStartCardGame();

        //stageController에서 카드 배치가 끝난 뒤 isPlaying을 true로 변경해줄 예정
        //isPlaying = true;
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

    public void SetNewStageSetting()
    {
        endCardGameUI.gameObject.SetActive(false);

        stageController.OnEndCardGame(clearedLevel);
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

    //컴파일 오류때문에 임시생성
    public Image touchBlockPanel;

    public IEnumerator TimeFlowCoroutine()
    {
        yield return null;
    }
}

