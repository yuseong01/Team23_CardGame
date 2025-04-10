using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    [SerializeField] private GameObject stageSelectUIGameObject;
    [SerializeField] private Transform StageBtnPrefabParents;
    [SerializeField] private StageBtn StageBtnPrefab;

    [SerializeField] private GameObject ModChangedBtnPrefab;

    [SerializeField] private ScrollController scrollController;
    [SerializeField] int modcount;

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

        scrollController.Init(stageBtnList);
    }

    public void UpdateButtonLockImage(int clearedLevel)
    {
        for (int i = 0; i < stageBtnList.Count; i++)
        {
            var targetButton = stageBtnList[i];

            targetButton.stageStartButton.enabled = false;


            for (int j = 0; j < targetButton.gameModeButtons.Length; j++)
            {
                targetButton.gameModeButtons[j].gameObject.SetActive(false);
            }

            if (i <= clearedLevel)
            {
                targetButton.lockImageGameObject.SetActive(false);

                targetButton.stageStartButton.enabled = true;
                for (int j = 0; j < targetButton.gameModeButtons.Length; j++)
                {
                    targetButton.gameModeButtons[j].gameObject.SetActive(true);
                }
            }
        }
    }

    public void UpdateBestTime(int _level, float time)
    {
        if (stageBtnList != null && _level - 1 < stageBtnList.Count)
        {
            stageBtnList[_level - 1].GetBestTime(time);
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
