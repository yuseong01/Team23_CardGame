using System.Collections;
using System.Collections.Generic;
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

    AudioSource audioSource;
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip failure;

    //public GameObject endPanel;
    //public Card firstCard;
    //public Card secondCard;

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
        //����������
        Application.targetFrameRate = 60;
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
        //ī�� ��ġ ����
        cardPlacementController.InitStartGame(level, 5, card);

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
        this.remainCard = remainCard;
        timeLimit = 30f + (level * 10f);
    }

    private IEnumerator TimeFlowCoroutine()
    {
        while (isPlaying)
        {
            //�ð��� ���������� Ÿ�ӹٰ� ������
            time += Time.deltaTime;
            float progress = Mathf.Clamp01(time / timeLimit);
            timeBar.fillAmount = progress;
            //Ÿ�ӹ��� ������ ���� Ÿ�ӹ� �� ����
            TimeBarColor(progress);

            if (time >= timeLimit)
            {
                GameOver();
                yield break; // �ڷ�ƾ ����
            }

            yield return null;
        }
    }

    private void GameOver()
    {
        isPlaying = false;
        Time.timeScale = 0.0f;

        Debug.Log("���� ����");
    }

    //Ÿ�ӹ��� ������ ���� Ÿ�ӹ� �� ����
    public void TimeBarColor(float progress)
    {
        if (progress < 0.5f)
        {
            // ���λ� �� �����
            float t = progress / 0.5f;
            timeBar.color = Color.Lerp(startColor, Color.yellow, t);
        }
        else
        {
            // ����� �� ������
            float t = (progress - 0.5f) / 0.5f;
            timeBar.color = Color.Lerp(Color.yellow, Color.red, t);
        }
    }

    public void Matched()
    {
        /* �ϴ� ���� �ּ�ó��
        //�� ī�尡 ���ٸ�
        if (firstCard.idx == secondCard.idx)
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
            firstCard.ReflipCard();
            secondCard.ReflipCard();
        }
        //ù��°, �ι�° ī�� ���� null�� �ʱ�ȭ
        firstCard = null;
        secondCard = null;
        */
    }
}
