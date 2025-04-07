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
        //프레임조정
        Application.targetFrameRate = 60;

        //레벨별 제한시간 설정
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
        //시간이 지나갈수록 타임바가 차오름
        time += Time.deltaTime;
        float progress = Mathf.Clamp01(time / timeLimit);
        timeBar.localScale = new Vector3(progress, 1, 1);

        //타임바의 비율에 따라 타임바 색 조정
        if (progress < 0.5f)
        {
            // 연두색 → 노란색
            float t = progress / 0.5f;
            barImage.color = Color.Lerp(new Color(0.6f, 1f, 0.2f), Color.yellow, t);
        }
        else
        {
            // 노란색 → 빨간색
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
            audioSource.PlayOneShot(fail);
            //카드 다시 뒤집기
            firstCard.ReflipCard();
            secondCard.ReflipCard();
        }
        //첫번째, 두번째 카드 변수 null로 초기화
        firstCard = null;
        secondCard = null;
    }
}
