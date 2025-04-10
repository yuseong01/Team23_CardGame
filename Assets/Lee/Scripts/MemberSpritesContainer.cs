using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberSpritesContainer 
{
    public List<Sprite[]> spritesList;


    public int totalMemberCount;
    public int CategoryCount => spritesList.Count;

    public void Init(int totalMemberCount)
    {
        spritesList = new();

        this.totalMemberCount = totalMemberCount;
    }

    public void AddSprites(Sprite[] addSprites)
    {
        if(addSprites.Length == totalMemberCount)
        {
            spritesList.Add(addSprites);
        }
    }
}