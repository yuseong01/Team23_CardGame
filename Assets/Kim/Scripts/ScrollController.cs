using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    //ScrollView내에있는 ScrollRect컴포넌트
    [SerializeField] private ScrollRect scrollRect;
    //ScrollView자식 오브젝트들로 스테이지 오브젝트가 들어갈 Content와 그걸 볼수있게끔 하는 ViewPort 
    [SerializeField] private RectTransform contant, viewport;
    [Space(10.0f)]
    //스테이지를 드래그했을때 중앙으로 되돌아오는 시간
    [SerializeField] private float snapSpeed;

    //스테이지들의 RectTransform을 리스트로 받는 변수
    private List<RectTransform> stageButtonsRectTranformList = new();
    //스테이지의 위치 
    private Vector2 stageImagePosition;
    
    //StageBtn을 리스트로 받아서 초기화를 시키는 함수
    public void Init(List<StageBtn> stageBtns)
    {
        stageButtonsRectTranformList.Clear();
        //스테이지 수만큼 순처적으로 스테이지의RectTransform컴퍼넌트를 RectTransform리스트에 Add
        for (int i = 0; i < stageBtns.Count; i++)
        {
            stageButtonsRectTranformList.Add(stageBtns[i].GetComponent<RectTransform>());
        }
    }

    public void AnimStopCoroutine()
    {
        StopCoroutine(StartSnapCoroutine());
        //StartCoroutine(StartSnapCoroutine());
    }
    public void OnPointDownEndDrag()
    {
        //float의 가장큰수를 비교하기위한 코드  [float.MaxValue*(실수중 가장 큰함수)*]
        float minDistance = float.MaxValue;
        //거리를 비교하기위한 스테이지의 아이템 수
        int selectCardButtonIndex = 0;

        //반복문을 사용해 스테이지오브젝트를 아이템들에 넣은 다음 두사이의 거리를 비교하는 코드
        for (int i = 0; i < stageButtonsRectTranformList.Count; i++)
        {
            //두 사이의 거리를 절대값으로 계산하는 코드
            float distance = Mathf.Abs(stageButtonsRectTranformList[i].anchoredPosition.x - (-contant.anchoredPosition.x));

            if (distance < minDistance)
            {
                minDistance = distance;
                selectCardButtonIndex = i;
            }
        }

        float cardsDifference = stageButtonsRectTranformList[selectCardButtonIndex].anchoredPosition.x;
        stageImagePosition = new Vector2(-cardsDifference, contant.anchoredPosition.y);

        //비교후 중앙으로 이동시키는함수
        StartCoroutine(StartSnapCoroutine());
    }

    //더 가까운 이미지를 오브젝트 중앙으로 이동시키는 코루틴

    private IEnumerator StartSnapCoroutine()
    {   
        //이미지의 포지션과 content 포지션의 거리가 0.1이하일때까지 실행
        while(Vector2.Distance(stageImagePosition, contant.anchoredPosition) > 0.1f)
        { 
            contant.anchoredPosition = Vector2.Lerp(contant.anchoredPosition, stageImagePosition, Time.deltaTime * snapSpeed);
                yield return null;
        }
        //0.1f이하가 되면 중앙으로 포지션 고정
        contant.anchoredPosition = stageImagePosition;
    }
}