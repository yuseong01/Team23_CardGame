using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBtn : MonoBehaviour
{
    public int diffcult = 0; //�������� ���̵�
    public GameObject StagImages;

    public void Stage(int diffcult)
    {
        diffcult = this.diffcult;
        switch (diffcult)
        {
            case 1:
                StagImages.SetActive(false);
                Debug.Log("TestStage1");
                //�������� 1
                break;
            case 2:
                StagImages.SetActive(false);
                Debug.Log("TestStage2");
                //�������� 2
                break;
            case 3:
                StagImages.SetActive(false);
                Debug.Log("TestStage3");
                //�������� 3
                break;
        }

    }

}
