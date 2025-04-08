using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBtnController : MonoBehaviour
{
    [SerializeField] private Transform StageBtnPrefabParents;
    [SerializeField] private  StageBtn StageBtnPrefab;

    [SerializeField] int level;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            StageButtonCreate(level);
            level++;
        }
    }

    public void StageButtonCreate(int level)
    {
        level = this.level;
        Instantiate(StageBtnPrefab, StageBtnPrefabParents).Init(level);
    }
}
