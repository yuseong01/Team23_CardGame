using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    //ScrollView�����ִ� ScrollRect������Ʈ
    [SerializeField] private ScrollRect scrollRect;
    //ScrollView�ڽ� ������Ʈ��� �������� ������Ʈ�� �� Content�� �װ� �����ְԲ� �ϴ� ViewPort 
    [SerializeField] private RectTransform contant, viewport;
    [Space(10.0f)]
    //���������� �巡�������� �߾����� �ǵ��ƿ��� �ð�
    [SerializeField] private float snapSpeed;

    //������������ RectTransform�� ����Ʈ�� �޴� ����
    private List<RectTransform> stageButtonsRectTranformList = new();
    //���������� ��ġ 
    private Vector2 stageImagePosition;
    
    //StageBtn�� ����Ʈ�� �޾Ƽ� �ʱ�ȭ�� ��Ű�� �Լ�
    public void Init(List<StageBtn> stageBtns)
    {
        stageButtonsRectTranformList.Clear();
        //�������� ����ŭ ��ó������ ����������RectTransform���۳�Ʈ�� RectTransform����Ʈ�� Add
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
        //float�� ����ū���� ���ϱ����� �ڵ�  [float.MaxValue*(�Ǽ��� ���� ū�Լ�)*]
        float minDistance = float.MaxValue;
        //�Ÿ��� ���ϱ����� ���������� ������ ��
        int selectCardButtonIndex = 0;

        //�ݺ����� ����� ��������������Ʈ�� �����۵鿡 ���� ���� �λ����� �Ÿ��� ���ϴ� �ڵ�
        for (int i = 0; i < stageButtonsRectTranformList.Count; i++)
        {
            //�� ������ �Ÿ��� ���밪���� ����ϴ� �ڵ�
            float distance = Mathf.Abs(stageButtonsRectTranformList[i].anchoredPosition.x - (-contant.anchoredPosition.x));

            if (distance < minDistance)
            {
                minDistance = distance;
                selectCardButtonIndex = i;
            }
        }

        float cardsDifference = stageButtonsRectTranformList[selectCardButtonIndex].anchoredPosition.x;
        stageImagePosition = new Vector2(-cardsDifference, contant.anchoredPosition.y);

        //���� �߾����� �̵���Ű���Լ�
        StartCoroutine(StartSnapCoroutine());
    }

    //�� ����� �̹����� ������Ʈ �߾����� �̵���Ű�� �ڷ�ƾ

    private IEnumerator StartSnapCoroutine()
    {   
        //�̹����� �����ǰ� content �������� �Ÿ��� 0.1�����϶����� ����
        while(Vector2.Distance(stageImagePosition, contant.anchoredPosition) > 0.1f)
        { 
            contant.anchoredPosition = Vector2.Lerp(contant.anchoredPosition, stageImagePosition, Time.deltaTime * snapSpeed);
                yield return null;
        }
        //0.1f���ϰ� �Ǹ� �߾����� ������ ����
        contant.anchoredPosition = stageImagePosition;
    }
}