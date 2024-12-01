using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Portal : MonoBehaviour
{
    // 이동할 맵의 넘버 설정 (농장:1, 동굴:2, 사냥터:3)
    [SerializeField] private int portalID = 0;
    // 0:동굴-농장, 1:농장-동굴, 2:농장-사냥터, 3:사냥터-농장
    [SerializeField] private GameObject[] portals;
    [SerializeField] private Transform player;
    [SerializeField] private CameraManager mapManager;
    [SerializeField] private GameObject hp_Canvas;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private bool isWarped = false;

    private void OnTriggerEnter(Collider col)
    {
        if(!isWarped && portalID == 1) // 동굴->농장
        {
            // 카메라 포스트프로세싱 프로파일 설정
            mapManager.ChangeProfile(1);

            // 포탈 타는 효과음 한번만 재생

            // hp UI 비활성화
            hp_Canvas.SetActive(false);

            // 몬스터 agent 이동 비활성화
            navMeshAgent.isStopped = true;

            player.position = new Vector3 (portals[1].transform.position.x,
                0.79f,
                portals[1].transform.position.z - 3); // 농장포탈 좌표

            isWarped = true;
            //SceneManager.LoadScene(portalID);
        }
        else if (!isWarped && portalID == 2) // 농장->동굴
        {
            // 카메라 포스트프로세싱 프로파일 설정
            mapManager.ChangeProfile(0);

            // 포탈 타는 효과음 한번만 재생

            // hp UI 비활성화
            hp_Canvas.SetActive(false);

            // 몬스터 agent 이동 비활성화
            navMeshAgent.isStopped = true;

            player.position = new Vector3(
                portals[0].transform.position.x - 3,
                0.79f,
                portals[0].transform.position.z); // 동굴포탈 좌표

            isWarped = true;
            //SceneManager.LoadScene(portalID);
        }
        else if (!isWarped && portalID == 3) // 농장->사냥터
        {
            // hp UI 활성화
            hp_Canvas.SetActive(true);

            // 몬스터 이동 활성화
            navMeshAgent.isStopped = false;

            // 포탈 타는 효과음 한번만 재생

            player.position = new Vector3(
                portals[3].transform.position.x,
                0.79f,
                portals[3].transform.position.z - 3); // 사냥터포탈 좌표

            isWarped = true;
            //SceneManager.LoadScene(portalID);
        }
        else if (!isWarped && portalID == 4) // 사냥터->농장
        {
            // hp UI 비활성화
            hp_Canvas.SetActive(false);

            // 몬스터 agent 이동 비활성화
            navMeshAgent.isStopped = true;

            // 포탈 타는 효과음 한번만 재생

            player.position = new Vector3(
                portals[2].transform.position.x - 3,
                1.5f,
                portals[2].transform.position.z); // 농장포탈2 좌표

            isWarped = true;
            //SceneManager.LoadScene(portalID);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isWarped = false;
    }
}
