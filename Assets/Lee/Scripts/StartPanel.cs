using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Text touchGuideText;

    private void Awake()
    {
        button.onClick.AddListener(TouchScreen);
    }

    private void Start()
    {
        StartCoroutine(TouchGuideAnimation());
    }

    void TouchScreen()
    {
        gameObject.SetActive(false);

        GameManager.instance.isTouchStartScreen = true;
    }

    IEnumerator TouchGuideAnimation()
    {
        touchGuideText.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.33f);


        touchGuideText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        StartCoroutine(TouchGuideAnimation());
    }
}
