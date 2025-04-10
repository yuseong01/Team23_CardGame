using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 게임 끝났을 때 UI
public class EndGameUI : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color clearColor; // 이겼을 때 색상
    [SerializeField] private Color failColor; // 졌을 때 색상

    [Header("UI")]
    [Space(20)]
    [SerializeField] private GameObject endUI; // 게임 종료 시 나오는 UI;
    [SerializeField] private GameObject clearTitle; // 성공 타이틀
    [SerializeField] private GameObject failTitle; // 실패 타이틀
    [SerializeField] private Animator animator; // 애니메이터
    [SerializeField] private Profile profile; // 프로필(이미지, 이름)
    [SerializeField] private Text timerTxt; // 클리어 시간 Text
    [SerializeField] private Button touchButton; // main으로
    [SerializeField] private string[] names; // 이름들..
    [SerializeField] private float profileChangeInterval; // 프로필이 변경되는 시간

    private Coroutine changeProfileCoroutine = null; // 프로필 변경 Coroutine


    public void Awake()
    {
        touchButton.onClick.AddListener(GoToMainMenu);
        endUI.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Text touchButtonText = touchButton.gameObject.GetComponent<Text>();
        Color buttonColor = touchButtonText.color;
        buttonColor.a = 0f;
        touchButtonText.color = buttonColor;
        touchButton.gameObject.SetActive(false); // UI가 켜지면 touchButton 비활성
    }

    // 성공 했을 때 UI
    public void OpenWinUI(float score)
    {

        SetTitleActive(true);
        timerTxt.text = score.ToString("N2"); // 점수 세팅
        profile.SetProfile(0, names[0], clearColor); // 처음 이미지를 하나 세팅
        endUI.SetActive(true);
        animator.SetTrigger("isEnd"); // 애니메이션 재생
        StartCoroutine(profile.ShakeImage());
        ChangeProfileLoop();
    }

    // 실패 했을 때 UI
    public void OpenFailUI()
    {
        SetTitleActive(false);
        profile.SetProfile(failColor); // 실패 이미지 세팅
        timerTxt.text = "";
        endUI.SetActive(true);
        animator.SetTrigger("isEnd"); // 애니메이션 재생
        ActiveTouchButton(false);
    }

    // Title Setting - Clear or Fail
    private void SetTitleActive(bool isClear)
    {
        clearTitle.SetActive(isClear);
        failTitle.SetActive(!isClear);
    }

    // Profile 변경 함수
    private void ChangeProfileLoop()
    {
        changeProfileCoroutine = StartCoroutine(ChangeProfile());
    }

    // Profile 변경 코루틴
    private IEnumerator ChangeProfile()
    {
        int i = 0;
        while (true)
        {
            if (i == names.Length)
            {
                i = 0;
                if (!touchButton.gameObject.activeSelf) ActiveTouchButton(true);
            }

            yield return new WaitForSeconds(profileChangeInterval); // n초마다 변경

            profile.SetProfile(i, names[i]);
            i++;
        }
    }

    private void ActiveTouchButton(bool isClear)
    {
        StartCoroutine(CheckAnimation(isClear)); // 아직 EndUI 애니메이션이 실행 중인지 확인
    }

    private IEnumerator CheckAnimation(bool isClear)
    {
        while (true)
        {
            // 체크하고자 하는 애니메이션인지 확인
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("EndUI") == true)
            {
                // 플레이 중인지 체크
                float animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                // 애니메이션 종료 시
                if (animTime >= 1.0f)
                {
                    touchButton.gameObject.SetActive(true);
                    yield return StartCoroutine(TouchButtonFadeIn(1.6f, isClear));
                    break;
                }
            }
            yield return null;
        }
    }

    // TouchButton FadeIn
    private IEnumerator TouchButtonFadeIn(float fadeSeconds, bool isClear)
    {
        float time = 0;
        Text touchButtonText = touchButton.gameObject.GetComponent<Text>();
        Color buttonColor = touchButtonText.color;
        buttonColor = isClear ? clearColor : failColor;
        touchButtonText.color = buttonColor;

        // touchButton 색상 변경
        while (time <= fadeSeconds)
        {
            time += Time.deltaTime;
            // Lerp(시작값(a), 끝값(b), a와 b 사이의 가운데 값) - 선형 보간법
            // 부드럽게 전환 시킬 때 사용하는 함수
            buttonColor.a = Mathf.Lerp(0f, 1f, time / fadeSeconds);
            touchButtonText.color = buttonColor;
            yield return null;
        }
    }


    public void GoToMainMenu()
    {
        // 실행 중인 코루틴이 있으면 정지
        if (changeProfileCoroutine != null)
        {
            StopCoroutine(changeProfileCoroutine);
        }
        //SceneManager.LoadScene("MainScene");

        GameManager.instance.SetNewStageSetting();
    }
}
