using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlacementController : MonoBehaviour
{
    private int defalutColumnCount = 3;

    private Queue<Cards> cardObejctQue = new();

    [Space(10f)]
    [SerializeField] Vector2 gridCellSpacing; // 카드간의 간격


    [Space(10f)]
    [SerializeField] GridLayoutGroup cardParentGrid;
    [SerializeField] RectTransform cardParentRectTransform;

    Vector2 cacheScreenSize;
    private void Awake()
    {
        cacheScreenSize = new(Screen.width, Screen.height);
    }


    //게임 시작시 카드배치
    public List<Cards> StartCardPlacement(int levelValue, MemberSpritesContainer memberSpritesContainer, Cards cardPrefab)
    {
        var leveledColumnCount = defalutColumnCount + levelValue;

        var totalCardCount = leveledColumnCount * (leveledColumnCount - 1);


        SetCellSize(leveledColumnCount, cardPrefab.GetComponent<RectTransform>());



        List<(int, int)> cardKeyList = GetCardKeyList(totalCardCount, memberSpritesContainer);

        return InitCardTable(totalCardCount, cardKeyList, memberSpritesContainer, cardPrefab);
    }


    //게임 종료시 카드비활성화
    public void EndCardPalcement()
    {
        while (cardObejctQue.Count > 0)
        {
            var targetCard = cardObejctQue.Dequeue();

            targetCard.CloseCardInvoke(); 

            targetCard.gameObject.SetActive(false);
        }
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

            cardObejctQue.Enqueue(targetCard);

            targetCard.gameObject.SetActive(true);

            targetCard.Init(cardKeyList[i], memberSpriteList[cardKeyList[i].Item1][cardKeyList[i].Item2]);

            totalCardsList.Add(targetCard);
        }

        return totalCardsList;
    }





    void SetCellSize(int leveledColumnCount ,RectTransform targetRect)
    {
        float cellSizeX = (cacheScreenSize.x - gridCellSpacing.x * leveledColumnCount) / leveledColumnCount;

        float cellSizeY = cellSizeX * (targetRect.sizeDelta.y / targetRect.sizeDelta.x);

        cardParentGrid.cellSize = new Vector2(cellSizeX, cellSizeY);

        cardParentGrid.spacing = gridCellSpacing;
    }

   
}
