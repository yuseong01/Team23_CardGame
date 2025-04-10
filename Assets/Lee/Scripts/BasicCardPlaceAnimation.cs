using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCardPlaceAnimation : CardPlaceAnimation
{
    public BasicCardPlaceAnimation(CardPlacementController controller) : base(controller) { }


    public override IEnumerator Play()
    {
        var gameManager = GameManager.instance;
        var cardList = controller.placedCardList;

        gameManager.touchBlockPanel.enabled = true;

        float animSpeed = controller.placeAnimationTime / cardList.Count;


        foreach (var item in cardList)
        {
            item.gameObject.SetActive(true);
            item.CloseCard();

            yield return new WaitForSeconds(animSpeed);
        }

        gameManager.touchBlockPanel.enabled = false;

        gameManager.isPlaying = true;
    }
}
