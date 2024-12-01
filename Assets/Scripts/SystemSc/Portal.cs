using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Portal : MonoBehaviour
{
    // �̵��� ���� �ѹ� ���� (����:1, ����:2, �����:3)
    [SerializeField] private int portalID = 0;
    // 0:����-����, 1:����-����, 2:����-�����, 3:�����-����
    [SerializeField] private GameObject[] portals;
    [SerializeField] private Transform player;
    [SerializeField] private CameraManager mapManager;
    [SerializeField] private GameObject hp_Canvas;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private bool isWarped = false;

    private void OnTriggerEnter(Collider col)
    {
        if(!isWarped && portalID == 1) // ����->����
        {
            // ī�޶� ����Ʈ���μ��� �������� ����
            mapManager.ChangeProfile(1);

            // ��Ż Ÿ�� ȿ���� �ѹ��� ���

            // hp UI ��Ȱ��ȭ
            hp_Canvas.SetActive(false);

            // ���� agent �̵� ��Ȱ��ȭ
            navMeshAgent.isStopped = true;

            player.position = new Vector3 (portals[1].transform.position.x,
                0.79f,
                portals[1].transform.position.z - 3); // ������Ż ��ǥ

            isWarped = true;
            //SceneManager.LoadScene(portalID);
        }
        else if (!isWarped && portalID == 2) // ����->����
        {
            // ī�޶� ����Ʈ���μ��� �������� ����
            mapManager.ChangeProfile(0);

            // ��Ż Ÿ�� ȿ���� �ѹ��� ���

            // hp UI ��Ȱ��ȭ
            hp_Canvas.SetActive(false);

            // ���� agent �̵� ��Ȱ��ȭ
            navMeshAgent.isStopped = true;

            player.position = new Vector3(
                portals[0].transform.position.x - 3,
                0.79f,
                portals[0].transform.position.z); // ������Ż ��ǥ

            isWarped = true;
            //SceneManager.LoadScene(portalID);
        }
        else if (!isWarped && portalID == 3) // ����->�����
        {
            // hp UI Ȱ��ȭ
            hp_Canvas.SetActive(true);

            // ���� �̵� Ȱ��ȭ
            navMeshAgent.isStopped = false;

            // ��Ż Ÿ�� ȿ���� �ѹ��� ���

            player.position = new Vector3(
                portals[3].transform.position.x,
                0.79f,
                portals[3].transform.position.z - 3); // �������Ż ��ǥ

            isWarped = true;
            //SceneManager.LoadScene(portalID);
        }
        else if (!isWarped && portalID == 4) // �����->����
        {
            // hp UI ��Ȱ��ȭ
            hp_Canvas.SetActive(false);

            // ���� agent �̵� ��Ȱ��ȭ
            navMeshAgent.isStopped = true;

            // ��Ż Ÿ�� ȿ���� �ѹ��� ���

            player.position = new Vector3(
                portals[2].transform.position.x - 3,
                1.5f,
                portals[2].transform.position.z); // ������Ż2 ��ǥ

            isWarped = true;
            //SceneManager.LoadScene(portalID);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isWarped = false;
    }
}
