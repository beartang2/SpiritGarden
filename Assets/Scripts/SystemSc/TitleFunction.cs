using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFunction : MonoBehaviour
{
    [SerializeField] private Animator title_Ani;
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
        if (Input.anyKeyDown) //아무 키 입력 시
        {
            title_Ani.SetBool("isStart", true); //애니메이션 실행
            anyKey_UI.SetActive(false); //UI 비활성화

            foreach (GameObject button in buttons)
            {
                button.SetActive(true);
            }
        }
    }
}
