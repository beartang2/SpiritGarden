using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFunction : MonoBehaviour
{
    [SerializeField] private GameObject anyKey_UI;
    [SerializeField] private GameObject[] buttons;

    private void Start()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.anyKeyDown) //�ƹ� Ű �Է� ��
        {
            anyKey_UI.SetActive(false); //UI ��Ȱ��ȭ

            foreach (GameObject button in buttons)
            {
                button.SetActive(true);
            }
        }
    }
}
