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
    [SerializeField] Vector2 gridCellSpacing; // 카드간의 간격


    [Space(10f)]
    [SerializeField] GridLayoutGroup cardParentGrid;
    [SerializeField] RectTransform cardParentRectTransform;

    
    public List<Cards> StartCardPlacement(int levelValue, MemberSpritesContainer memberSpritesContainer, Cards cardPrefab)
    {
        var leveledColumnCount = defalutColumnCount + levelValue;

        var totalCardCount = leveledColumnCount * (leveledColumnCount - 1);


        SetCellSize(leveledColumnCount, cardPrefab.frontImage.rectTransform);



        List<(int, int)> cardKeyList = GetCardKeyList(totalCardCount, memberSpritesContainer);

        return InitCardTable(totalCardCount, cardKeyList, memberSpritesContainer, cardPrefab);
    }

    List<(int, int)> GetCardKeyList(int totalCardCount, MemberSpritesContainer memberSpritesContainer)
    {
        int memberIndex = 0;

        int pairCount = 2;

        List<(int, int)> cardKeyList = new();

        for (int i = 0; i < totalCardCount; i += pairCount)
        {
            if (memberIndex % memberSpritesContainer.totalMemberCount == 0)
            {
                memberIndex = 0;
            }

            int randCategoryNum = Random.Range(0, memberSpritesContainer.CategoryCount);

            for (int j = 0; j < pairCount; j++)
            {
                cardKeyList.Add(new(randCategoryNum, memberIndex));
            }

            memberIndex++;
        }

        return cardKeyList.OrderBy(x=> Random.value).ToList();
    }

    List<Cards> InitCardTable(int totalCardCount, List<(int,int)> cardKeyList, MemberSpritesContainer memberSpritesContainer, Cards cardPrefab)
    {
        var memberSpriteList = memberSpritesContainer.spritesList;

        List<Cards> totalCardsList = new();

        for (int i = 0; i < totalCardCount; i ++)
        {
            var targetCard = cardParentRectTransform.childCount <= i ?
               Instantiate(cardPrefab, cardParentRectTransform) :
               cardParentRectTransform.GetChild(i).GetComponent<Cards>();

            cardObejctQue.Enqueue(targetCard.gameObject);

            targetCard.gameObject.SetActive(true);

            targetCard.Init(cardKeyList[i], memberSpriteList[cardKeyList[i].Item1][cardKeyList[i].Item2]);

            totalCardsList.Add(targetCard);
        }

        return totalCardsList;
    }



    //게임 종료시 카드비활성화
    public void InitEndGame()
    {
        DisableCards();
    }



    void SetCellSize(int leveledColumnCount ,RectTransform targetRect)
    {
        float cellSizeX = (cardParentRectTransform.sizeDelta.x - gridCellSpacing.x * leveledColumnCount) / leveledColumnCount;

        float cellSizeY = cellSizeX * (targetRect.sizeDelta.y / targetRect.sizeDelta.x);

        cardParentGrid.cellSize = new Vector2(cellSizeX, cellSizeY);

        cardParentGrid.spacing = gridCellSpacing;
    }

    void DisableCards()
    {
        while (cardObejctQue.Count > 0)
        {
            cardObejctQue.Dequeue().SetActive(false);
        }
    }

}
