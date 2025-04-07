using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGridController : MonoBehaviour
{
    private List<GameObject> cardObejctList;


    [SerializeField] int testLevel;


    [SerializeField] int defalutGridSize;

    [SerializeField] GameObject testCardPrefab;
    [SerializeField] RectTransform cardParent;
    [SerializeField] GridLayoutGroup cardParentGrid;

    [SerializeField] Vector2 gridCellSpacing;

    private void Awake()
    {
        cardObejctList = new();

        cardParentGrid.spacing = gridCellSpacing;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DisableCards();

            InitNewGame(testLevel);
        }
    }

    public void InitNewGame(int levelValue)
    {
        var totalGridSize = defalutGridSize + levelValue;

        cardParentGrid.constraintCount = totalGridSize;

        EnableCards(totalGridSize);
    }

    public void InitEndGame()
    {
        DisableCards();
    }



    void EnableCards(int totalGridSize)
    {
        for (int i = 0; i < totalGridSize * totalGridSize; i++)
        {
            var targetCardObject = cardParent.childCount <= i ?
                Instantiate(testCardPrefab, cardParent) :
                cardParent.GetChild(i).gameObject;

            targetCardObject.SetActive(true);

            cardObejctList.Add(targetCardObject);
        }
    }


    void DisableCards()
    {
        foreach (var item in cardObejctList)
        {
            item.SetActive(false);
        }

        cardObejctList.Clear();
    }

}
