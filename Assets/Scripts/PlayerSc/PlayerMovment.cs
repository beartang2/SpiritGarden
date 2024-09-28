using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;     // ĳ���� �̵� �ӵ�
    public Camera mainCamera;        // �� ī�޶�
    private Vector3 targetPosition;  // �̵� ��ǥ ����
    private bool isMoving;           // ĳ���Ͱ� �̵� ������ ����
    private Rigidbody rb;            // ĳ������ Rigidbody
    private Animator anim;           // ĳ���� �ִϸ�����

    void Awake()
    {
        // Rigidbody ������Ʈ ��������
        rb = GetComponent<Rigidbody>();
        // Animator ������Ʈ ��������
        anim = GetComponent<Animator>();

        // ī�޶� �Ҵ���� �ʾ����� �ڵ����� ���� ī�޶� ã��
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        targetPosition = transform.position;  // ������ �� ĳ������ ��ġ�� ��ǥ �������� ����
        isMoving = false;                     // ó������ �̵� ���� �ƴ�
    }

    void Update()
    {
        // ���콺 ��Ŭ�� ����
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition();  // ���콺 ��Ŭ���� ��ġ�� ��ǥ ����
        }

        // ��ǥ �������� ĳ���� �̵�
        if (isMoving)
        {
            MoveToTarget();
        }

        // �̵� ���� �ƴ� ��� �ִϸ����� ���� ������Ʈ
        UpdateAnimator();
    }

    // ���콺 ��Ŭ���� ��ġ�� ��ǥ ������ ����
    void SetTargetPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);  // ���콺 ��ġ���� ������ �߻�
        RaycastHit hit;

        // ������ �浹�� ������ ������ �� ������ ��ǥ ��ġ�� ����
        if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Ground")
        {
            targetPosition = hit.point;  // �浹�� ������ ��ǥ�� ����
            isMoving = true;             // �̵� ����
        }
    }

    // ĳ���͸� ��ǥ �������� �̵�
    void MoveToTarget()
    {
        // ���� ��ġ���� ��ǥ �������� �ε巴�� �̵�
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);  // Rigidbody�� ����Ͽ� �̵�

        // �̵� ���� ���
        Vector3 moveDirection = (targetPosition - rb.position).normalized;

        // �ִϸ����� �Ķ���� ������Ʈ
        anim.SetFloat("MoveX", moveDirection.x);
        anim.SetFloat("MoveY", moveDirection.z); // Z���� Y������ ��� (2D�� ��� Y�� ���)

        // ĳ���Ͱ� ��ǥ ������ ���� �����ϸ� �̵��� ����
        if (Vector3.Distance(rb.position, targetPosition) < 0.2f)
        {
            isMoving = false;
        }
    }

    // �ִϸ����� ���� ������Ʈ
    void UpdateAnimator()
    {
        // ĳ���Ͱ� �̵����� �ʴ´ٸ� �ִϸ��̼��� ����
        if (!isMoving)
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);
        }
    }
}
