using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsFunction : MonoBehaviour
{
    public void SingleMode()
    {
        // ���丮 ��
        SceneManager.LoadScene("FarmScene");
    }

    public void MultiMode()
    {
        // ��Ƽ ��� �߰�
    }

    public void Option()
    {
        // �ɼ� �г� Ȱ��ȭ
    }

    public void Exit()
    {
        // ���� ����
#if UNITY_EDITOR //��ó����� ����Ƽ �����Ͱ� �������϶� �÷��̸� ���ߵ�����.
        UnityEditor.EditorApplication.isPlaying = false; //���ø����̼� �÷��̸� false�� ��.
#else //����Ƽ�����Ͱ� �������� �ƴҶ� �۵�
                Application.Quit(); //���ø����̼��� ����
#endif
    }
}
