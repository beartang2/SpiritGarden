using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractBoxController : MonoBehaviour
{
    public Transform player;   // 기준이 되는 캐릭터
    public Transform cube;     // 큐브
    [SerializeField] float distance = 2f;   // 캐릭터와 큐브 사이의 고정 거리
    [SerializeField] LayerMask groundLayer; // 마우스가 클릭할 수 있는 레이어 (예: 땅)
    [SerializeField] Collider cubeCollider; // 큐브의 콜라이더

    private Vector3 lastDirection; // 큐브의 마지막 방향을 저장
    private float fixedY;          // 고정된 Y축 값
    [SerializeField] float colliderActiveTime = 0.1f; // 콜라이더를 잠시 활성화하는 시간

    ToolBarController toolbarCont; // 툴 바 컨트롤러
    [SerializeField] SpriteRenderer toolSprite; // 도구 이미지

    private void Start()
    {
        // 캐릭터의 Y 축을 고정
        fixedY = player.position.y;

        // 초기 큐브 방향 설정 (플레이어와의 초기 거리)
        lastDirection = (cube.position - player.position).normalized;

        // 큐브의 콜라이더를 처음에는 비활성화
        cubeCollider.enabled = false;

        toolbarCont = gameObject.GetComponent<ToolBarController>();
    }

    void Update()
    {
        UpdateToolIcon();

        // 마우스 포인터에서 나가는 레이를 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 마우스 좌클릭을 감지
        if (Input.GetMouseButtonDown(0))
        {
            // 레이가 땅에 맞았는지 확인
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // 클릭한 위치와 캐릭터 사이의 방향 계산 (Y축 고정)
                Vector3 targetDirection = (hit.point - player.position).normalized;
                targetDirection.y = 0;  // Y축 무시하여 수평 방향만 계산

                // 클릭한 위치와 캐릭터 사이의 거리를 계산
                float clickedDistance = Vector3.Distance(player.position, hit.point);

                // 클릭 위치가 고정 거리보다 가까운 경우
                if (clickedDistance < distance)
                {
                    // 큐브가 플레이어와 적절한 거리만큼 떨어지도록 설정
                    cube.position = player.position + targetDirection * distance;
                }
                else
                {
                    // 큐브가 플레이어와 적절한 거리만큼 떨어지도록 설정
                    cube.position = player.position + targetDirection * distance;
                }

                // 큐브의 Y축 고정
                cube.position = new Vector3(cube.position.x, fixedY, cube.position.z);

                // 콜라이더를 잠시 켰다가 끄는 코루틴 실행
                StartCoroutine(ActivateColliderTemporarily());
            }
        }
        else
        {
            Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer);

            // 마지막 방향을 업데이트
            lastDirection = (hit.point - player.position).normalized;

            // 큐브가 마지막 방향으로 플레이어를 따라다님
            cube.position = player.position + lastDirection * distance;

            // 큐브의 Y축 고정
            cube.position = new Vector3(cube.position.x, fixedY, cube.position.z);
        }
    }

    private void UpdateToolIcon()
    {
        Item item = toolbarCont.GetItem;

        if(item == null)
        {
            return;
        }

        toolSprite.sprite = item.icon;

        // 방향에 따른 x,y 플립 기능
    }

    // 큐브의 콜라이더를 잠깐 활성화했다가 비활성화하는 코루틴
    IEnumerator ActivateColliderTemporarily()
    {
        cubeCollider.enabled = true;              // 콜라이더 활성화
        yield return new WaitForSeconds(colliderActiveTime); // 일정 시간 대기
        cubeCollider.enabled = false;             // 콜라이더 비활성화
    }
}
