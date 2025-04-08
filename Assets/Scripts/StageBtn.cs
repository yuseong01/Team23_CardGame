using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBtn : MonoBehaviour
{
    public int id;

    public void Stage()
    {
        switch (id)
        {
            case 0:
                SceneManager.LoadScene("CardGame");
                break;
            case 1:
                SceneManager.LoadScene("Stage");
                break;
            case 2:
                SceneManager.LoadScene("");
                break;
        }
    }

}
