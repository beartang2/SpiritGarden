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
    public string description; // Ʃ�丮�� ����
    public string objective; // ��ǥ
    public bool isCompleted; // �Ϸ� �÷���
    public GameObject highlightObj; // ���� ǥ�� ������Ʈ
}
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<Tutorial_Info> tutoInfo; // Ʃ�丮�� ���� ����Ʈ
    private int currentStep = 0; // ���� Ʃ�丮�� �ܰ�

    [SerializeField] private GameObject tuto_Panel; // Ʃ�丮�� UI �г�
    [SerializeField] private Text tuto_Text; // Ʃ�丮�� �ؽ�Ʈ
    //[SerializeField] private TMPro.TextMeshPro tuto_Text; // Ʃ�丮�� �ؽ�Ʈ (TMP)
    [SerializeField] private Transform player; // �÷��̾� �̵� Ȯ�� ����

    private bool arrived = false; // �̵����� ���� Ȯ�� ����
    private int cnt = 0; // ī��Ʈ ����

    private void Start()
    {
        // Ʃ�丮�� Ȱ��ȭ
        tuto_Panel.SetActive(true);

        // Ʃ�丮�� ���� �Լ�
        StartTutorial();
    }

    private void Update()
    {
        // Ʃ�丮�� ��ŵ ���� Ȯ�� �Լ�
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
        else if(currentStep == 9) // ������ Ʃ��
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                EndTutorial();
            }
        }

        // �̵� üũ
        Moving_Tuto();
        // ���� ��� üũ
        Tool_Tuto();
        // �κ��丮 ��� üũ
        Inventory_Tuto();
    }

    private void StartTutorial()
    {
        // �ܰ� �ʱ�ȭ
        currentStep = 0;

        // ���� �ܰ��� Ʃ�丮�� ǥ�� �Լ�
        ShowCurrentTuto();
    }

    private void ShowCurrentTuto()
    {
        Tutorial_Info info = tutoInfo[currentStep]; // Ʃ�� ������ ���� �ܰ� ����
        tuto_Text.text = info.description; // ������ �ؽ�Ʈ�� ��������

        // ���� ������Ʈ�� �ִ� ����
        if(info.highlightObj != null)
        {
            HighlightObject(info.highlightObj);
        }
    }

    private void HighlightObject(GameObject highlightObj)
    {
        // ���� ������Ʈ Ȱ��ȭ
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
    private void Moving_Tuto() // ù��° �÷��̾� �̵� Ʃ�丮��
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
    private void Tool_Tuto() // �ι�° �÷��̾� ���� ��� Ʃ�丮��
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
