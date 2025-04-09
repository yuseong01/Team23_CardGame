using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    [SerializeField] Text stageText;
    [SerializeField] Button button;
    [SerializeField] Image mainIcon;

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

}

