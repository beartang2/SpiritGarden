using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Tutorial_Info
{
    [TextArea(1, 10)]
    public string description; // 튜토리얼 설명
    public string objective; // 목표
    public bool isCompleted; // 완료 플래그
    public GameObject highlightObj; // 강조 표시 오브젝트
}
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<Tutorial_Info> tutoInfo; // 튜토리얼 정보 리스트
    private int currentStep = 0; // 현재 튜토리얼 단계

    [SerializeField] private GameObject tuto_Panel; // 튜토리얼 UI 패널
    [SerializeField] private Text tuto_Text; // 튜토리얼 텍스트
    //[SerializeField] private TMPro.TextMeshPro tuto_Text; // 튜토리얼 텍스트 (TMP)
    [SerializeField] private Transform player; // 플레이어 이동 확인 변수

    private bool arrived = false; // 이동에서 도착 확인 변수
    private int cnt = 0; // 카운트 변수

    private void Start()
    {
        // 튜토리얼 활성화
        tuto_Panel.SetActive(true);

        // 튜토리얼 시작 함수
        StartTutorial();
    }

    private void Update()
    {
        // 튜토리얼 스킵 유무 확인 함수
        Check_Skip_Tuto();

        if (tutoInfo[0].isCompleted)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                EndTutorial();
            }
        }
        else if(currentStep % 2 == 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                CompleteTuto();
            }
        }
        else if(currentStep == 9) // 마지막 튜토
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                EndTutorial();
            }
        }

        // 이동 체크
        Moving_Tuto();
        // 도구 사용 체크
        Tool_Tuto();
        // 인벤토리 사용 체크
        Inventory_Tuto();
    }

    private void StartTutorial()
    {
        // 단계 초기화
        currentStep = 0;

        // 현재 단계의 튜토리얼 표시 함수
        ShowCurrentTuto();
    }

    private void ShowCurrentTuto()
    {
        Tutorial_Info info = tutoInfo[currentStep]; // 튜토 정보에 현재 단계 적용
        tuto_Text.text = info.description; // 설명을 텍스트에 가져오기

        // 강조 오브젝트가 있는 상태
        if(info.highlightObj != null)
        {
            HighlightObject(info.highlightObj);
        }
    }

    private void HighlightObject(GameObject highlightObj)
    {
        // 강조 오브젝트 활성화
        highlightObj.SetActive(true);
    }

    private void Check_Skip_Tuto()
    {
        if(Input.GetKeyDown(KeyCode.Return) && currentStep == 0)
        {
            //currentStep++;
            CompleteTuto();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && currentStep == 0)
        {
            currentStep+=2;
            ShowCurrentTuto();
        }
    }
    private void Moving_Tuto() // 첫번째 플레이어 이동 튜토리얼
    {
        if (currentStep == 3)
        {
            if(Input.GetMouseButtonDown(1))
            {
                cnt++;
                Debug.Log(cnt);
            }
            if(!arrived && cnt > 2)
            {
                arrived = true;
                currentStep++;
                ShowCurrentTuto();
                cnt = 0;
            }
        }
    }
    private void Tool_Tuto() // 두번째 플레이어 도구 사용 튜토리얼
    {
        if(currentStep == 5)
        {
            if(Input.GetMouseButtonDown(0))
            {
                cnt++;
            }
            if(cnt > 5)
            {
                CompleteTuto();
            }
        }
    }
    private void Inventory_Tuto()
    {
        if (currentStep == 7)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                CompleteTuto();
            }
        }
    }

    private void CompleteTuto()
    {
        tutoInfo[currentStep].isCompleted = true;
        currentStep++;

        if(currentStep < tutoInfo.Count)
        {
            ShowCurrentTuto();
        }
        else
        {
            EndTutorial();
        }
    }

    private void EndTutorial()
    {
        tuto_Panel.SetActive(false);
    }
}
