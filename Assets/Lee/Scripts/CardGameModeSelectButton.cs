using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameModeSelectButton : MonoBehaviour
{
    Button button;

    public StageBtn parentBtn;

    public GameManager.CardGameEventMode targetMode;

    private void Awake()
    {
        button.onClick.AddListener(
            () =>
            {

            }
            );
    }
}
