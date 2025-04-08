using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private CardPlacementController cardPlacementController;
    [SerializeField] private Cards card;
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

    private MemberSpritesContainer memberSpritesContainer;

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

        //����������
        Application.targetFrameRate = 60;
        //PlayerPrefs�� ����Ȱ� ������ clearedLevel �ʱ�ȭ
        if (PlayerPrefs.HasKey(levelkey)) { clearedLevel = PlayerPrefs.GetInt(levelkey); }

        memberSpritesContainer = new MemberSpritesContainer();
        memberSpritesContainer.Init(5);
        memberSpritesContainer.AddSprites(members1);
        memberSpritesContainer.AddSprites(members2);
        memberSpritesContainer.AddSprites(members3);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        
    }


    //��ư���� �۵���ų��
    //GameManager.instance.StartGame(level);
    public void StartGame(int level)
    {
        //ī�� ��ġ ����
        List<Cards> remain = cardPlacementController.StartCardPlacement(level, memberSpritesContainer, card);
        remainCard = remain.Count;

        time = 0.0f;
        Time.timeScale = 1.0f;
        timeBar.fillAmount = 0.0f;

        SetTimeLimit(level);  // ������ ���� timeLimit ����

        isPlaying = true;
        StartCoroutine(TimeFlowCoroutine()); // �ð� �帧 ����

        
    }

    //������ ���ѽð� ����
    public void SetTimeLimit(int level)
    {
        timeLimit = 30f + (level * 10f);
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
        if(remainCard == 0)
        {
            //������
            //����UI���******************************************************************

            //���� ����, PlayerPrefs�� ����
            if (clearedLevel < level)
            {
                clearedLevel = level;
                PlayerPrefs.SetInt(levelkey, clearedLevel);
            }
        }
        else
        {
            //���н�
            //����UI���******************************************************************
        }
        isPlaying = false;
        Time.timeScale = 0.0f;

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
            audioSource.PlayOneShot(success);
            //ī��� �ո����� ���� / ������ ��������Ʈ?

            //���� ī�� �� ����
            remainCard -= 2;
        }
        //�� ī�尡 ���� �ʴٸ�
        else
        {
            //���� ����Ŭ��
            audioSource.PlayOneShot(failure);
            //ī�� �ٽ� ������
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        //ù��°, �ι�° ī�� ���� null�� �ʱ�ȭ
        firstCard = null;
        secondCard = null;
    }
}

