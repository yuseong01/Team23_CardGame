using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private GameObject stageSelectUIGameObject;
    [SerializeField] private Transform StageBtnPrefabParents;
    [SerializeField] private StageBtn StageBtnPrefab;

    List<StageBtn> stageBtnList;

    public void StageButtonCreate(Sprite[] memberIconsArray)
    {
        stageBtnList = new();

        int totalStageCount = memberIconsArray.Length;

        for (int i = 0; i < totalStageCount; i++)
        {
            StageBtn newBtn = Instantiate(StageBtnPrefab, StageBtnPrefabParents);

            newBtn.Init(i + 1, memberIconsArray[i]);

            stageBtnList.Add(newBtn);
        }
    }

    public void UpdateButtonLockImage(int clearedLevel)
    {
        for (int i = 0; i < stageBtnList.Count; i++)
        {
            if(i <= clearedLevel)
            {
                stageBtnList[i].lockImageGameObject.SetActive(false);
            }
        }
    }

    public void OnStartCardGame()
    {
        stageSelectUIGameObject.SetActive(false);
    }

    public void OnEndCardGame(int clearedLevel)
    {
        stageSelectUIGameObject.SetActive(true);

        UpdateButtonLockImage(clearedLevel);
    }
}
