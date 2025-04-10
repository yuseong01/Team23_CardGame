using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{    
    private float bestTime;
    
    [SerializeField] private Text stageText;
    [SerializeField] private Text bestTimeText;

    [SerializeField] private Button stageStartButton;
    [SerializeField] private Image mainIcon;
    [SerializeField] private EndGameUI endGameUI;
    
    public GameObject lockImageGameObject;
    private void Update()
    {
        ResetBestTimePlayerPrefs();
    }
    public void Init(int _level, Sprite iconSprite)
    {
        stageText.text = "Stage " + _level.ToString();


        string timeKey = "BestTime_" + _level;
        float saveBestTime = PlayerPrefs.GetFloat(timeKey, -1);
        if (saveBestTime != -1)
        {
            bestTimeText.text = "Best Time: " + saveBestTime.ToString("N2");
        }
        else
        {
            bestTimeText.text = "";
        }


        mainIcon.sprite = iconSprite;
        stageStartButton.onClick.AddListener(() => StartStage(_level));
    }

    public void StartStage(int _level)
    {
        GameManager.instance.StartCardGame(_level);
    }

    public void GetBestTime(float bestTime)
    {
        Debug.Log("GetTimeTest");

        this.bestTime = bestTime;

        SetBestTime();
    }

    public void ResetBestTimePlayerPrefs()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            bestTimeText.text = "Best Time: ";
        }
    }

    public void SetBestTime()
    {
        Debug.Log("SetTimeTest");
        Debug.Log(bestTime);

        bestTimeText.text= "Best Time: "+ bestTime.ToString("N2");
    }
}