using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlacementController : MonoBehaviour
{
    private int defalutColumnCount = 3;

    private Queue<GameObject> cardObejctQue = new();


    int[] testMemberArr = { 0, 1, 2, 3, 4 };

    [SerializeField] int testLevel;
    [SerializeField] List<int> testCardCellList = new();

    [SerializeField] Vector2 gridCellSpacing;

    [Space(10f)]
    [SerializeField] RectTransform testCardPrefab;
    [SerializeField] RectTransform cardParent;
    [SerializeField] GridLayoutGroup cardParentGrid;

   


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
        var leveledColumnCount = defalutColumnCount + levelValue;

        var totalCardCount = leveledColumnCount * (leveledColumnCount - 1);


        InitGrid(leveledColumnCount);

        InitCardCellList(totalCardCount);


        EnableCards(totalCardCount);
    }

    public void InitEndGame()
    {
        DisableCards();
    }

    void InitCardCellList(int totalCardCount)
    {
        int memberIndex = 0;

        var newMemberArr = testMemberArr;

        for (int i = 0; i < totalCardCount; i += 2)
        {
            if (memberIndex == newMemberArr.Length || i == 0)
            {
                memberIndex = 0;
                newMemberArr = newMemberArr.OrderBy(x => Random.value).ToArray();
            }

            testCardCellList.Add(newMemberArr[memberIndex]);
            testCardCellList.Add(newMemberArr[memberIndex]);

            memberIndex++;
        }

        testCardCellList = testCardCellList.OrderBy(x => Random.value).ToList();
    }


    void InitGrid(int leveledColumnCount)
    {
        float cellSizeX = (cardParent.sizeDelta.x - gridCellSpacing.x * leveledColumnCount) / leveledColumnCount;
        
        float cellSizeY = cellSizeX * (testCardPrefab.sizeDelta.y / testCardPrefab.sizeDelta.x);

        cardParentGrid.cellSize = new Vector2(cellSizeX, cellSizeY);

        cardParentGrid.spacing = gridCellSpacing;
    }



    void EnableCards(int totalCardCount)
    {
        for (int i = 0; i < totalCardCount; i++)
        {
            var targetCardObject = cardParent.childCount <= i ?
                Instantiate(testCardPrefab.gameObject, cardParent) :
                cardParent.GetChild(i).gameObject;

            targetCardObject.SetActive(true);

            cardObejctQue.Enqueue(targetCardObject);

            /*
             * targetCardObject < testCardCellList วาด็
             */
        }
    }


    void DisableCards()
    {
        while (cardObejctQue.Count > 0)
        {
            cardObejctQue.Dequeue().SetActive(false);
        }
    }
}
