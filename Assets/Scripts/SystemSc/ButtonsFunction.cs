using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsFunction : MonoBehaviour
{
    public void SingleMode()
    {
        // 스토리 씬
        SceneManager.LoadScene("FarmScene");
    }

    public void MultiMode()
    {
        // 멀티 기능 추가
    }

    public void Option()
    {
        // 옵션 패널 활성화
    }

    public void Exit()
    {
        // 게임 종료
#if UNITY_EDITOR //전처리기로 유니티 에디터가 실행중일때 플레이를 멈추도록함.
        UnityEditor.EditorApplication.isPlaying = false; //어플리케이션 플레이를 false로 함.
#else //유니티에디터가 실행중이 아닐때 작동
                Application.Quit(); //어플리케이션을 종료
#endif
    }
}
