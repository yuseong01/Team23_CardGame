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
        //����������
        Application.targetFrameRate = 60;
        
        //ī�� �����ܰ迡�� �����ʱ�ȭ ȣ��������� 
        SetTimeLimit(remainCard, level);
        

        time = 0.0f;
        Time.timeScale = 1.0f;
        timeBar.fillAmount = 0.0f;
    }

    void Update()
    {
        //�ð��� ���������� Ÿ�ӹٰ� ������
        time += Time.deltaTime;
        float progress = Mathf.Clamp01(time / timeLimit);
        timeBar.fillAmount = progress;

        //Ÿ�ӹ��� ������ ���� Ÿ�ӹ� �� ����
        TimeBarColor(progress);

        if (time >= timeLimit)
        {
            Time.timeScale = 0.0f;
            //endPanel.SetActive(true);
        }
    }

    //������ ���ѽð� ����
    public void SetTimeLimit(int remainCard,  int level)
    {
        this.remainCard = remainCard;
        timeLimit = 20f + (level * 10f);
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
            audioSource.PlayOneShot(fail);
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
