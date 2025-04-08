using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBtnController : MonoBehaviour
{
    [SerializeField] private Transform StageBtnPrefabParents;
    [SerializeField] private  StageBtn StageBtnPrefab;

    int levelText = 1;

    public void StageButtonCreate(int stageCount)
    {
        for(int i = 0; i < stageCount; i++)
        {
            Instantiate(StageBtnPrefab, StageBtnPrefabParents).Init(levelText);
            levelText++;
        }
    }
}
