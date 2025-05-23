using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;     // 캐릭터 이동 속도
    public Camera mainCamera;        // 주 카메라
    private Vector3 targetPosition;  // 이동 목표 지점
    private bool isMoving;           // 캐릭터가 이동 중인지 여부
    private Rigidbody rb;            // 캐릭터의 Rigidbody
    private Animator anim;           // 캐릭터 애니메이터

    private void Awake()
    {
        // Rigidbody 컴포넌트 가져오기
        rb = GetComponent<Rigidbody>();
        // Animator 컴포넌트 가져오기
        anim = GetComponent<Animator>();

        // 카메라가 할당되지 않았으면 자동으로 메인 카메라를 찾음
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        targetPosition = transform.position;  // 시작할 때 캐릭터의 위치를 목표 지점으로 설정
        isMoving = false;                     // 처음에는 이동 중이 아님
    }

    private void Update()
    {
        // 마우스 우클릭 감지
        if (Input.GetMouseButton(1))
        {
            SetTargetPosition();  // 마우스 우클릭한 위치로 목표 설정
        }

        // 이동 중이 아닌 경우 애니메이터 상태 업데이트
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        // 목표 지점으로 캐릭터 이동
        if (isMoving)
        {
            //CheckFront();
            MoveToTarget();
        }
        else
        {
            BlockOutOfWorld();  // 플레이어가 Ground 밖으로 나갔는지 확인
        }
    }

    // 플레이어가 Ground 밖으로 나갔는지 아래 레이로 확인
    private void BlockOutOfWorld()
    {
        Ray ray = new Ray(transform.position, Vector3.down);  // 아래 방향으로 레이 발사
        if(Physics.Raycast(ray, out RaycastHit hit, 1f))
        {
            // 레이가 충돌한 물체가 Ground가 아니면
            if (hit.transform.tag != "Ground")
            {
                // 캐릭터 움직임 멈추기
                rb.velocity = Vector3.zero;  // Rigidbody 속도 초기화
                isMoving = false;  // 이동 멈춤
            }
        }
        else
        {
            // 캐릭터 움직임 멈추기
            rb.velocity = Vector3.zero;  // Rigidbody 속도 초기화
            isMoving = false;  // 이동 멈춤
        }
    }

    // 마우스 우클릭한 위치로 목표 지점을 설정
    private void SetTargetPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);  // 마우스 위치에서 광선을 발사
        RaycastHit hit;

        // 광선이 충돌한 지점이 있으면 그 지점을 목표 위치로 설정
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Ground")
        {
            targetPosition = hit.point;  // 충돌한 지점을 목표로 설정
            isMoving = true;             // 이동 시작
        }
    }

    // 캐릭터를 목표 지점으로 이동
    private void MoveToTarget()
    {
        // 지정 위치가 너무 멀어지면 움직이지 않음
        if(Vector3.Distance(rb.position, targetPosition) > 15f)
        {
            isMoving = false;
        }
        // 현재 위치에서 목표 지점으로 부드럽게 이동
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);  // Rigidbody를 사용하여 이동

        // 이동 방향 계산
        Vector3 moveDirection = (targetPosition - rb.position).normalized;

        // 애니메이터 파라미터 업데이트
        anim.SetFloat("MoveX", moveDirection.x);
        anim.SetFloat("MoveY", moveDirection.z); // Z축을 Y축으로 사용

        // 캐릭터가 목표 지점에 거의 도달하면 이동을 멈춤
        if (Vector3.Distance(rb.position, targetPosition) < 0.2f)
        {
            isMoving = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Ground")
        {
            CheckFront();
        }
    }

    // 앞에 물체가 있는지 확인
    private void CheckFront()
    {
        isMoving = false; // 이동 멈춤
    }

    // 애니메이터 상태 업데이트
    private void UpdateAnimator()
    {
        // 캐릭터가 이동하지 않는다면 애니메이션을 중지
        if (!isMoving)
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);
        }
    }
}
