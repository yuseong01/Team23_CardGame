using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlacementController : MonoBehaviour
{
    private int defalutColumnCount = 3;

    public List<(int, int)> cardKeyList = new();
    public List<Cards> placedCardList = new();

    [Space(10f)]
    public float placeAnimationTime; // 카드 배치 애니메이션 시간

    [Space(10f)]
    [SerializeField] GridLayoutGroup cardParentGrid;
    [SerializeField] RectTransform cardParentRectTransform;



    CardPlaceAnimation[] cardAnim;

    BlindCardPlaceAnimation blindCardAnim;
    BasicCardPlaceAnimation basicCardAnim;

    private void Awake()
    {
        cardAnim = new CardPlaceAnimation[]
        {
            basicCardAnim = new(this),
            blindCardAnim = new(this)
        };
    }


    //게임 시작시 카드배치
    public List<Cards> StartCardPlacement(int levelValue, MemberSpritesContainer memberSpritesContainer, Cards cardPrefab, GameManager.CardGamePlaceMode placeMode)
    {
        var leveledColumnCount = defalutColumnCount + levelValue;

        var totalCardCount = leveledColumnCount * (leveledColumnCount - 1);


        SetCellSize(totalCardCount, cardPrefab);

        SetCardKeyList(totalCardCount, memberSpritesContainer);

        SetCardTable(totalCardCount, memberSpritesContainer, cardPrefab);


        int gameModeIndex = 0;

        if(placeMode == GameManager.CardGamePlaceMode.Basic)
        {
            gameModeIndex = 0;
        }
        else
        {
            gameModeIndex = 1;
        }

        StartCoroutine(cardAnim[gameModeIndex].Play());

        return placedCardList;
    }


    //게임 종료시 초기화
    public void EndCardPalcement()
    {
        foreach (var item in placedCardList)
        {
            item.CloseCard();
            item.gameObject.SetActive(false);
        }

        placedCardList.Clear();
        cardKeyList.Clear();
    }



    void SetCardKeyList(int totalCardCount, MemberSpritesContainer memberSpritesContainer)
    {
        int item2Index = 0;

        int pairCount = 2;

        for (int i = 0; i < totalCardCount; i += pairCount)
        {
            if (item2Index % memberSpritesContainer.totalMemberCount == 0)
            {
                item2Index = 0;
            }

            int randCategoryNum = Random.Range(0, memberSpritesContainer.CategoryCount);

            for (int j = 0; j < pairCount; j++)
            {
                cardKeyList.Add(new(randCategoryNum, item2Index));
            }

            item2Index++;
        }

        cardKeyList = cardKeyList.OrderBy(x=> Random.value).ToList();
    }

    void SetCardTable(int totalCardCount, MemberSpritesContainer memberSpritesContainer, Cards cardPrefab)
    {
        for (int i = 0; i < totalCardCount; i ++)
        {
            var targetCard = cardParentRectTransform.childCount <= i ?
               Instantiate(cardPrefab, cardParentRectTransform) :
               cardParentRectTransform.GetChild(i).GetComponent<Cards>();

            placedCardList.Add(targetCard);

            targetCard.gameObject.SetActive(false);

            targetCard.Init(cardKeyList[i], memberSpritesContainer);
        }
    }



    void SetCellSize(int totalCardCount, Cards targetCard)
    {
        float cardRatio = targetCard.size.y / targetCard.size.x;

        Vector2 cellSize = Vector2.zero;

        float screenWidth = Screen.width - cardParentGrid.padding.horizontal;
        float screenHeight = Screen.height - cardParentGrid.padding.vertical;


        for (int row = 1; row < totalCardCount; row++)
        {
            if(totalCardCount % row == 0)
            {
                int col = totalCardCount / row;

                float currentScreenWidth = screenWidth - (cardParentGrid.spacing.x * (col - 1));
                float currentScreenHeight = screenHeight - (cardParentGrid.spacing.y * (row - 1));


                float cardMaxWidth = currentScreenWidth / col;
                float cardMaxHeight = currentScreenHeight / row;


                float ratioCardWidth = cardMaxHeight / cardRatio;

                float bestCardWidth = Mathf.Min(cardMaxWidth, ratioCardWidth);
                float bestCardHeight = bestCardWidth * cardRatio;

                if (bestCardWidth > cellSize.x)
                {
                    cellSize = new(bestCardWidth, bestCardHeight);
                }
            }
        }

        cardParentGrid.cellSize = cellSize;
    }
}
