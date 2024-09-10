using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;     // 따라갈 캐릭터
    private Vector3 offset;      // 카메라와 캐릭터 사이의 고정된 거리

    void Start()
    {
        // 시작할 때 캐릭터와 카메라 사이의 초기 위치 차이를 offset으로 설정
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // 캐릭터의 위치에 offset을 더해 카메라 위치를 즉시 고정
        transform.position = player.position + offset;
    }
}
