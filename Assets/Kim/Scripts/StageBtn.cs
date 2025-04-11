using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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


    public (GameManager.CardGameMode, GameManager.CardGameMode) currentGameMode;

    private float bestTime;

    Dictionary<(GameManager.CardGameMode, GameManager.CardGameMode), float> bestTimeDict = new();


    private void Awake()
    {
        currentGameMode = new(GameManager.CardGameMode.Basic, GameManager.CardGameMode.Basic);

        bestTimeDict.Add((GameManager.CardGameMode.Basic, GameManager.CardGameMode.Basic), float.MaxValue);
        bestTimeDict.Add((GameManager.CardGameMode.Blind, GameManager.CardGameMode.Basic), float.MaxValue);
        bestTimeDict.Add((GameManager.CardGameMode.Basic, GameManager.CardGameMode.Shuffle), float.MaxValue);
        bestTimeDict.Add((GameManager.CardGameMode.Blind, GameManager.CardGameMode.Shuffle), float.MaxValue);

        basicFirstButton.onClick.AddListener(() => currentGameMode.Item1 = GameManager.CardGameMode.Blind);
        blindFirstButton.onClick.AddListener(() => currentGameMode.Item1 = GameManager.CardGameMode.Basic);
        basicSecondButton.onClick.AddListener(() => currentGameMode.Item2 = GameManager.CardGameMode.Shuffle);
        shuffleSecondButton.onClick.AddListener(() => currentGameMode.Item2 = GameManager.CardGameMode.Basic);

        gameModeButtons = new Button[]
        {
            basicFirstButton,
            blindFirstButton,
            basicSecondButton,
            shuffleSecondButton
        };

        foreach (var item in gameModeButtons)
        {
            item.onClick.AddListener(() =>
            {
                item.transform.SetAsFirstSibling();
                SetBestTime();
            });

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
        /*float saveBestTime = PlayerPrefs.GetFloat(timeKey, -1);
        if (saveBestTime != -1)
        {
            bestTimeText.text = "Best Time: " + saveBestTime.ToString("N2");
        }
        else
        {
            bestTimeText.text = "";
        }*/

        SetBestTime();


        //mainIcon.sprite = iconSprite;
        stageStartButton.onClick.AddListener(() => StartStage(_level));
    }

    public void StartStage(int _level)
    {
        GameManager.instance.StartCardGame(_level, currentGameMode);
    }

    public void GetBestTime((GameManager.CardGameMode, GameManager.CardGameMode) gameMode, float currentTime)
    {
        this.bestTime = currentTime;


        if (bestTimeDict[gameMode] > currentTime)
        {
            bestTimeDict[gameMode] = currentTime;
        }

        bestTimeText.text = "Best Time: " + bestTimeDict[gameMode].ToString("N2");

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
        bestTimeText.text= "Best Time: " + bestTimeDict[currentGameMode].ToString("N2");
    }
}