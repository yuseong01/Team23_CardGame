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

    public void Init(int _level, Sprite iconSprite)
    {
        stageText.text = "Stage " + _level.ToString();
       // bestTimeText.text = "";
        
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
        //EndUI���� Ŭ������������ ���ھ ������
        if (this.bestTime > bestTime)
        {
            this.bestTime = bestTime;
        }

        SetBestTime();
    }

    public void SetBestTime()
    {
        Debug.Log("SetTimeTest");
        Debug.Log(bestTime);

        bestTimeText.text= "Best Time: "+ bestTime.ToString("N2");
    }
}