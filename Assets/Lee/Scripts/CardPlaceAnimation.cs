using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardPlaceAnimation
{
    protected CardPlacementController controller;

    public CardPlaceAnimation(CardPlacementController controller)
    {
        this.controller = controller;
    }
    public abstract IEnumerator Play();
}
