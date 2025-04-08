using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBtnTest : MonoBehaviour
{
    [SerializeField] StageBtnController stageBtnController;
    [SerializeField] int stageCount;
    void Start()
    {
        stageBtnController.StageButtonCreate(stageCount);
    }
}
