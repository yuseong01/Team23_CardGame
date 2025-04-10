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
    [SerializeField] private Transform ModChangedBtnPrefabParents;

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

            for (int j = 0; j < 3; j++)
            {
                GameObject modButton = Instantiate(ModChangedBtnPrefab, newBtn.transform);

                modButton.GetComponentInChildren<Text>().text = "Mod" + j;

                GameObject capturedButon = modButton;

                capturedButon.GetComponent<Button>().onClick.AddListener(() =>
                {
                    modButton.transform.SetSiblingIndex(0);
                });
            }
        }


        scrollController.Init(stageBtnList);
    }

    public void UpdateButtonLockImage(int clearedLevel)
    {
        for (int i = 0; i < stageBtnList.Count; i++)
        {
            if (i <= clearedLevel)
            {
                stageBtnList[i].lockImageGameObject.SetActive(false);
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
