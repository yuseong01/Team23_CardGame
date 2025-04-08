using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlacementController : MonoBehaviour
{
    private int defalutColumnCount = 3;

    private Queue<GameObject> cardObejctQue = new();

    [Space(10f)]
    [SerializeField] Vector2 gridCellSpacing; // ī�尣�� ����


    [Space(10f)]
    [SerializeField] GridLayoutGroup cardParentGrid;
    [SerializeField] RectTransform cardParentRectTransform;

    
    //���� ���۽� ī���ġ, �ε��� ��ȯ
    public int[] InitStartGame(int levelValue, int memberCount, GameObject cardPrefab)
    {
        var leveledColumnCount = defalutColumnCount + levelValue;

        var totalCardCount = leveledColumnCount * (leveledColumnCount - 1);


        SetCellSize(leveledColumnCount, cardPrefab.GetComponent<RectTransform>());

        EnableCards(totalCardCount, cardPrefab);

        return GetCardRandCells(totalCardCount, memberCount);
    }


    //���� ����� ī���Ȱ��ȭ
    public void InitEndGame()
    {
        DisableCards();
    }



    int[] GetCardRandCells(int totalCardCount, int memberCount)
    {
        int memberIndex = 0;

        List<int> memberIndexList = new();

        List<int> memberRandCellList = new();

        for (int i = 0; i < memberCount; i++)
        {
            memberIndexList.Add(i);
        }


        for (int i = 0; i < totalCardCount; i+= 2)
        {
            if (memberIndex == memberIndexList.Count || i == 0)
            {
                memberIndex = 0;

                memberIndexList = memberIndexList.OrderBy(x => Random.value).ToList();
            }

            for (int j = 0; j < 2; j++)
            {
                memberRandCellList.Add(memberIndexList[memberIndex]);
            }

            memberIndex++;
        }

        return memberRandCellList.OrderBy(x => Random.value).ToArray();
    }


    void SetCellSize(int leveledColumnCount ,RectTransform targetRect)
    {
        float cellSizeX = (cardParentRectTransform.sizeDelta.x - gridCellSpacing.x * leveledColumnCount) / leveledColumnCount;

        float cellSizeY = cellSizeX * (targetRect.sizeDelta.y / targetRect.sizeDelta.x);

        cardParentGrid.cellSize = new Vector2(cellSizeX, cellSizeY);

        cardParentGrid.spacing = gridCellSpacing;
    }

    void EnableCards(int totalCardCount, GameObject cardPrefab)
    {
        for (int i = 0; i < totalCardCount; i++)
        {
            var targetCardObject = cardParentRectTransform.childCount <= i ?
                Instantiate(cardPrefab.gameObject, cardParentRectTransform) :
                cardParentRectTransform.GetChild(i).gameObject;

            targetCardObject.SetActive(true);

            cardObejctQue.Enqueue(targetCardObject);
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
