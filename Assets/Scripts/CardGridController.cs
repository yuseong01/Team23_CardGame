using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class CardGridController : MonoBehaviour
{
    [SerializeField] GameObject testCardPrefab;
    [SerializeField] Transform cardParent;
    [SerializeField] GridLayoutGroup gridParent;

    [SerializeField] Vector2 gridSpacing;

    [SerializeField] int gridCellSize;

    [SerializeField] int testLevel;

    public int totalCardCount;

    private List<GameObject> cardObejctList;

    private void Awake()
    {
        cardObejctList = new();

        gridParent.spacing = gridSpacing;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DisableCards();

            EnableCards(testLevel);
        }
    }

    public void EnableCards(int levelValue)
    {
        if(levelValue < 0)
        {
            return;
        }

        totalCardCount = (gridCellSize + levelValue) * (gridCellSize + levelValue);

        for (int i = 0; i < totalCardCount; i++)
        {
            var targetCardObject = cardParent.childCount <= i ?
                Instantiate(testCardPrefab, cardParent) :
                cardParent.GetChild(i).gameObject;

            targetCardObject.SetActive(true);

            cardObejctList.Add(targetCardObject);
        }
    }

    private void DisableCards()
    {
        foreach (var item in cardObejctList)
        {
            item.SetActive(false);
        }

        cardObejctList.Clear();
    }

}
