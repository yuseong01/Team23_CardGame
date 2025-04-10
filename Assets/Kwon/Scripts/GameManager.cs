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

        //����������
        Application.targetFrameRate = 60;
        //PlayerPrefs�� ����Ȱ� ������ clearedLevel �ʱ�ȭ
        //if (PlayerPrefs.HasKey(levelkey)) {clearedLevel = PlayerPrefs.GetInt(levelkey);}

        memberSpritesContainer = new MemberSpritesContainer();
        memberSpritesContainer.Init(5);
        memberSpritesContainer.AddSprites(members1);
        memberSpritesContainer.AddSprites(members2);
        memberSpritesContainer.AddSprites(members3);

    }

    private void Start()
    {
        //BGM ������ �ٲ�� ���ֵ� ���� ������ �;��
        soundManager.PlayGameBGM();

        stageController.StageButtonCreate(stageIconSprites);

        stageController.UpdateButtonLockImage(clearedLevel);

        CardGameController.instance.touchBlockPanel.enabled = false;
    }

    private void Update()
    {
        if (isPlaying)
        {
            //�ð��� ���������� Ÿ�ӹٰ� ������
            time += Time.deltaTime;
            float progress = 1 - Mathf.Clamp01(time / timeLimit);
            timeBar.fillAmount = progress;
            //Ÿ�ӹ��� ������ ���� Ÿ�ӹ� �� ����
            TimeBarColor(progress);

            //�׽�Ʈ�� ���� Ŀ�ǵ�
            if (Input.GetKeyDown(KeyCode.D))
            {
                remainCard = 0;
            }
            //�׽�Ʈ�� ���� Ŀ�ǵ�
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
    //������ ���ѽð� ����
    public void SetTimeLimit(int level)
    {
        timeLimit = 20f + (level * 10f);
    }
    
    //ī�� ��ġ ���� �г�Ƽ
    public void AddMatchFailPenalty()
    {
        float penaltyAmount = timeLimit * 0.05f;
        time += penaltyAmount;
    }

    public void StartCardGame(int level)
    {
        this.level = level;

        //ī�� ��ġ ����
        List<Cards> remain = cardPlacementController.StartCardPlacement(level, memberSpritesContainer, card);
        remainCard = remain.Count;

        time = 0.0f;
        Time.timeScale = 1.0f;
        timeBar.fillAmount = 0.0f;

        SetTimeLimit(level);    // ������ ���� timeLimit ����

        stageController.OnStartCardGame();

        //stageController���� ī�� ��ġ�� ���� �� isPlaying�� true�� �������� ����
        //isPlaying = true;
    }

    private void GameOver()
    {
        cardPlacementController.EndCardPalcement();

        if(remainCard == 0)
        {
            //������ ���� ����, PlayerPrefs�� ����
            if (clearedLevel < level)
            {
                clearedLevel = level;
                //PlayerPrefs.SetInt(levelkey, clearedLevel);
            }
            endCardGameUI.OpenWinUI(time);
        }
        else
        {
            //���н�
            endCardGameUI.OpenFailUI();
        }
        isPlaying = false;

        cardPlacementController.EndCardPalcement();
        Debug.Log("���� ����");
    }

    public void SetNewStageSetting()
    {
        endCardGameUI.gameObject.SetActive(false);

        stageController.OnEndCardGame(clearedLevel);
    }

    //Ÿ�ӹ��� ������ ���� Ÿ�ӹ� �� ����
    public void TimeBarColor(float progress)
    {
        if (progress > 0.5f)
        {
            // ���λ� �� �����
            float t = (progress - 0.5f) / 0.5f;
            timeBar.color = Color.Lerp(Color.yellow, startColor, t);
        }
        else
        {
            // ����� �� ������
            float t = progress / 0.5f;
            timeBar.color = Color.Lerp(Color.red, Color.yellow, t);
        }
    }

    //������ ���������� �ӽû���
    public Image touchBlockPanel;

    public IEnumerator TimeFlowCoroutine()
    {
        yield return null;
    }
}

