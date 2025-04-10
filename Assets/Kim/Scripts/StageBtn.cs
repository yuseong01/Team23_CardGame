using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{    
    
    [SerializeField] private Text stageText;
    [SerializeField] private Text bestTimeText;

    
    //[SerializeField] private Image mainIcon;
    [SerializeField] private EndGameUI endGameUI;



    [SerializeField] Button basicFirstButton;
    [SerializeField] Button blindFirstButton;

    [SerializeField] Button basicSecondButton;
    [SerializeField] Button shuffleSecondButton;

    public Button[] gameModeButtons;
    
    public GameObject lockImageGameObject;

    public Button stageStartButton;


    public (GameManager.CardGamePlaceMode, GameManager.CardGameEventMode) selectMode;

    private float bestTime;


    private void Awake()
    {
        basicFirstButton.onClick.AddListener(() => selectMode.Item1 = GameManager.CardGamePlaceMode.Blind);
        blindFirstButton.onClick.AddListener(() => selectMode.Item1 = GameManager.CardGamePlaceMode.Basic);

        basicSecondButton.onClick.AddListener(() => selectMode.Item2 = GameManager.CardGameEventMode.Shuffle);
        shuffleSecondButton.onClick.AddListener(() => selectMode.Item2 = GameManager.CardGameEventMode.Basic);

        gameModeButtons = new Button[]
        {
            basicFirstButton,
            blindFirstButton,
            basicSecondButton,
            shuffleSecondButton
        };

        foreach (var item in gameModeButtons)
        {
            item.onClick.AddListener(() => item.transform.SetAsFirstSibling());
        }
    }


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


        //mainIcon.sprite = iconSprite;
        stageStartButton.onClick.AddListener(() => StartStage(_level));
    }

    public void StartStage(int _level)
    {
        GameManager.instance.StartCardGame(_level, selectMode);
    }

    public void GetBestTime(float bestTime)
    {
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