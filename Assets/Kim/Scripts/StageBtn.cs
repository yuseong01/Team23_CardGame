using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    [SerializeField] Text stageText;
    [SerializeField] Button button;
    [SerializeField] int level;

    public void Init(int _level)
    {
        this.level = _level;
        stageText.text = "Stage " + _level.ToString();

        //���ӸŴ������� ó��

        //if (GameManager.instance.clearedLevel <= _level)
        //{
        //    button.onClick.AddListener(() => StartStage(_level));
        //}

    }
    public void StartStage(int _level)
    {
        //���ӸŴ���
        //GameManager.instance.StartGame(_level);
    }

}

