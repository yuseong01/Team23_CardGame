using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 끝났을 때 UI
public class EndGameUI : MonoBehaviour
{
    [Header("UI")]
    [Space(20)]
    [SerializeField] private GameObject endUI; // 게임 종료 시 나오는 UI;
    [SerializeField] private Profile profile; // 프로필(이미지, 이름)
    [SerializeField] private Text titleTxt; // CLEAR , FAIL
    [SerializeField] private Text timerTxt; // 클리어 시간 Text
    [SerializeField] private Button goToMainButton; // main으로
    [SerializeField] private string[] names; // 이름들..
    [SerializeField] private float profileChangeInterval; // 프로필이 변경되는 시간

    private Coroutine changeProfileCoroutine = null; // 프로필 변경 Coroutine

    public void Awake()
    {
        goToMainButton.onClick.AddListener(RetryGame);

        endUI.gameObject.SetActive(false);
    }

    // 성공 했을 때 UI
    public void OpenWinUI(float score)
    {
        titleTxt.text = "CLEAR";
        timerTxt.text = score.ToString("N2");
        profile.SetProfile(0, names[0]); // 처음 이미지를 하나 세팅
        endUI.SetActive(true);
        ChangeProfileLoop();
    }

    // 실패 했을 때 UI
    public void OpenFailUI()
    {
        titleTxt.text = "FAIL";
        profile.SetProfile();
        timerTxt.text = "";
        endUI.SetActive(true);
    }

    // Profile 변경 함수
    void ChangeProfileLoop()
    {
        changeProfileCoroutine = StartCoroutine(ChangeProfile());
    }

    // Profile 변경 코루틴
    IEnumerator ChangeProfile()
    {
        int i = 0;
        while (true)
        {
            if (i == names.Length) i = 0;

            yield return new WaitForSeconds(profileChangeInterval); // n초마다 변경

            profile.SetProfile(i, names[i]);

            i++;
        }
    }

    public void RetryGame()
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
