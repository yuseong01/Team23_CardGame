using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    float bestTime;

    [SerializeField] private Text stageText;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private Button button;
    [SerializeField] private Image mainIcon;

    [SerializeField] private EndGameUI endGameUI;

    public GameObject lockImageGameObject;

    public void Init(int _level, Sprite iconSprite)
    {
        stageText.text = "Stage " + _level.ToString();

        mainIcon.sprite = iconSprite;
        button.onClick.AddListener(() => StartStage(_level));
    }

    public void StartStage(int _level)
    {
        GameManager.instance.StartCardGame(_level);
    }

    public void GetBestTime(float bestTime)
    {

    }
}