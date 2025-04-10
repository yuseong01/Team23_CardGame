using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlacementController : MonoBehaviour
{
    private int defalutColumnCount = 3;

    private List<(int, int)> cardKeyList = new();
    private List<Cards> placedCardList = new();

    [Space(10f)]
    [SerializeField] float placeAnimationTime; // 카드 배치 애니메이션 시간

    [Space(10f)]
    [SerializeField] GridLayoutGroup cardParentGrid;
    [SerializeField] RectTransform cardParentRectTransform;



    //게임 시작시 카드배치
    public List<Cards> StartCardPlacement(int levelValue, MemberSpritesContainer memberSpritesContainer, Cards cardPrefab)
    {
        var leveledColumnCount = defalutColumnCount + levelValue;

        var totalCardCount = leveledColumnCount * (leveledColumnCount - 1);


        SetCellSize(leveledColumnCount, cardPrefab.size);

        SetCardKeyList(totalCardCount, memberSpritesContainer);

        SetCardTable(totalCardCount, memberSpritesContainer, cardPrefab);


        StartCoroutine(AnimationPlaceCards(placeAnimationTime));

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
        var memberSpriteList = memberSpritesContainer.spritesList;


        for (int i = 0; i < totalCardCount; i ++)
        {
            var targetCard = cardParentRectTransform.childCount <= i ?
               Instantiate(cardPrefab, cardParentRectTransform) :
               cardParentRectTransform.GetChild(i).GetComponent<Cards>();

            placedCardList.Add(targetCard);

            targetCard.gameObject.SetActive(true);

            targetCard.Init(cardKeyList[i], memberSpriteList[cardKeyList[i].Item1][cardKeyList[i].Item2]);
        }
    }



    void SetCellSize(int leveledColumnCount ,Vector2 cardSize)
    {
        float placeAreaHorizontal = 1080 - cardParentGrid.padding.horizontal;

        float cellSizeX = (placeAreaHorizontal - cardParentGrid.spacing.x * leveledColumnCount) / leveledColumnCount;

        float cellSizeY = cellSizeX * (cardSize.y / cardSize.x);

        cardParentGrid.cellSize = new(cellSizeX, cellSizeY);
    }


    IEnumerator AnimationPlaceCards(float placeAnimTime)
    {
        var gameManager = GameManager.instance;

        gameManager.touchBlockPanel.enabled = true;

        float animSpeed = placeAnimTime / placedCardList.Count;

        foreach (var item in placedCardList)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in placedCardList)
        {
            item.gameObject.SetActive(true);

            yield return new WaitForSeconds(animSpeed);
        }

        gameManager.touchBlockPanel.enabled = false;

        StartCoroutine(gameManager.TimeFlowCoroutine());
    }
}
