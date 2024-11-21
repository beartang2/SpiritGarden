using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // �̵��� ���� �� �ѹ� ���� (����:1, ����:2, �����:3)
    [SerializeField] private int portalID = 0;

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(portalID);
    }
}
