using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    public int diffcult = 0; //스테이지 난이도
    public GameObject StagImages;

    [SerializeField] StageBtnController btnController;

    public Text stageText;
    public Button button;


    public void Init(int level)
    {
        stageText.text= "Stage "+level.ToString();

        button.onClick.AddListener(() => Stage(level));
    }
    public void Stage(int diffcult)
    {
        diffcult = this.diffcult;
        //if(diffcult) { }

    }

}
