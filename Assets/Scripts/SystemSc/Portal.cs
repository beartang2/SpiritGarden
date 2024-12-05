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
    [SerializeField] private Text timeText;
    [SerializeField] CameraManager camSc;

    public Color[] textColors;

    private bool isWarped = false;

    private void Start()
    {
        camSc.mapCnt = 1;
        hp_Canvas.SetActive(false); // 시작은 사냥터가 아니므로 비활성화
    }

    private void OnTriggerEnter(Collider col)
    {
        if(!isWarped && portalID == 1) // 동굴->농장
        {
            // 카메라 포스트프로세싱 프로파일 설정
            mapManager.ChangeProfile(1, camSc.mapCnt);
            // 시간 텍스트 색상 변경
            timeText.color = textColors[1];

            // 포탈 타는 효과음 한번만 재생

            // hp UI 비활성화
            hp_Canvas.SetActive(false);

            player.position = new Vector3 (portals[1].transform.position.x,
                0.79f,
                portals[1].transform.position.z - 3); // 농장포탈 좌표

            isWarped = true;
            //SceneManager.LoadScene(portalID);
        }
        else if (!isWarped && portalID == 2) // 농장->동굴
        {
            // 카메라 포스트프로세싱 프로파일 설정
            mapManager.ChangeProfile(0, 0);
            // 시간 텍스트 색상 변경
            timeText.color = textColors[0];

            // 포탈 타는 효과음 한번만 재생

            // hp UI 비활성화
            hp_Canvas.SetActive(false);

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

            // 시간 텍스트 색상 변경
            timeText.color = textColors[2];

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

            // 시간 텍스트 색상 변경
            timeText.color = textColors[1];

            // 포탈 타는 효과음 한번만 재생

            player.position = new Vector3(
                portals[2].transform.position.x - 3,
                0.79f,
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
