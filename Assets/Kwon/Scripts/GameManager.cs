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

        //����������
        Application.targetFrameRate = 60;
        //PlayerPrefs�� ����Ȱ� ������ clearedLevel �ʱ�ȭ
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

    //��ư���� �۵���ų��
    //GameManager.instance.StartGame(level);
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

        isPlaying = true;
        StartCoroutine(TimeFlowCoroutine());    // �ð� �帧 ����

        stageController.OnStartCardGame();
    }

    //������ ���ѽð� ����
    public void SetTimeLimit(int level)
    {
        timeLimit = 20f + (level * 10f);
    }

    private IEnumerator TimeFlowCoroutine()
    {
        while (isPlaying)
        {
            //�ð��� ���������� Ÿ�ӹٰ� ������
            time += Time.deltaTime;
            float progress = 1- Mathf.Clamp01(time / timeLimit);
            timeBar.fillAmount = progress;
            //Ÿ�ӹ��� ������ ���� Ÿ�ӹ� �� ����
            TimeBarColor(progress);

            //�׽�Ʈ�� ���� Ŀ�ǵ�
            if(Input.GetKeyDown(KeyCode.D))
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
                yield break; // �ڷ�ƾ ����
            }
            yield return null;
        }
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

    public void Matched()
    {
        //�� ī�尡 ���ٸ�
        if (firstCard.key.Item1 == secondCard.key.Item1 && firstCard.key.Item2 == secondCard.key.Item2)
        {
            //���� ����Ŭ��
            //audioSource.PlayOneShot(success);
            //ī��� �ո����� ����, ��������Ʈ
            ShowMatchEffect(firstCard);
            ShowMatchEffect(secondCard);

            //���� ī�� �� ����
            remainCard -= 2;
        }
        //�� ī�尡 ���� �ʴٸ�
        else
        {
            //���� ����Ŭ��
            //audioSource.PlayOneShot(failure);

            //ī�� �ٽ� ������
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        //ù��°, �ι�° ī�� ���� null�� �ʱ�ȭ
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

