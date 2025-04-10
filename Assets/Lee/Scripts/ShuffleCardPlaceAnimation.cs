using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShuffleCardPlaceAnimation : CardPlaceAnimation
{
    public ShuffleCardPlaceAnimation(CardPlacementController controller) : base(controller) { }


    public override IEnumerator Play()
    {
        var gameManager = GameManager.instance;
        var cardList = controller.placedCardList;

        List<Cards> closedCardList = new();
        List<(int, int)> shuffleCardKeyList = new();

        gameManager.touchBlockPanel.enabled = true;

        float animSpeed = controller.placeAnimationTime / cardList.Count;


        foreach (var item in cardList)
        {
            if(!item.isOpen)
            {
                closedCardList.Add(item);
                shuffleCardKeyList.Add(item.key);
            }
        }

        shuffleCardKeyList = shuffleCardKeyList.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < closedCardList.Count; i++)
        {
            closedCardList[i].OpenCard();
            closedCardList[i].Init(shuffleCardKeyList[i], gameManager.memberSpritesContainer);

            yield return new WaitForSeconds(animSpeed);
        };

        yield return new WaitForSeconds(1f);


        for (int i = 0; i < closedCardList.Count; i++)
        {
            closedCardList[i].CloseCard();

            yield return new WaitForSeconds(animSpeed);
        };


        gameManager.touchBlockPanel.enabled = false;
    }
}
