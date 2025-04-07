using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public RectTransform timeBar;
    public Image barImage;
    public GameObject endPanel;
    public Card firstCard;
    public Card secondCard;
    public int remainCard;

    AudioSource audioSource;
    public AudioClip success;
    public AudioClip fail;

    float time;
    float timeLimit;
    int level;

    private void Awake()
    {
        if(Instance == null)
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

        //������ ���ѽð� ����
        switch (level)
        {
            case 1:
                timeLimit = 30.0f;
                break;
            case 2:
                timeLimit = 40.0f;
                break;
            case 3:
                timeLimit = 50.0f;
                break;
            default:
                timeLimit = 30.0f;
                break;
        }

        time = 0.0f;
        Time.timeScale = 1.0f;
        timeBar.localScale = new Vector3(0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //�ð��� ���������� Ÿ�ӹٰ� ������
        time += Time.deltaTime;
        float progress = Mathf.Clamp01(time / timeLimit);
        timeBar.localScale = new Vector3(progress, 1, 1);

        //Ÿ�ӹ��� ������ ���� Ÿ�ӹ� �� ����
        if (progress < 0.5f)
        {
            // ���λ� �� �����
            float t = progress / 0.5f;
            barImage.color = Color.Lerp(new Color(0.6f, 1f, 0.2f), Color.yellow, t);
        }
        else
        {
            // ����� �� ������
            float t = (progress - 0.5f) / 0.5f;
            barImage.color = Color.Lerp(Color.yellow, Color.red, t);
        }

        if (time >= timeLimit)
        {
            Time.timeScale = 0.0f;
            endPanel.SetActive(true);
        }
    }

    public void Matched()
    {
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
    }
}
