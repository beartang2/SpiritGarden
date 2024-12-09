using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;     // ĳ���� �̵� �ӵ�
    public Camera mainCamera;        // �� ī�޶�
    private Vector3 targetPosition;  // �̵� ��ǥ ����
    private bool isMoving;           // ĳ���Ͱ� �̵� ������ ����
    private Rigidbody rb;            // ĳ������ Rigidbody
    private Animator anim;           // ĳ���� �ִϸ�����

    private void Awake()
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

    private void Update()
    {
        // ���콺 ��Ŭ�� ����
        if (Input.GetMouseButton(1))
        {
            SetTargetPosition();  // ���콺 ��Ŭ���� ��ġ�� ��ǥ ����
        }

        // �̵� ���� �ƴ� ��� �ִϸ����� ���� ������Ʈ
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        // ��ǥ �������� ĳ���� �̵�
        if (isMoving)
        {
            //CheckFront();
            MoveToTarget();
        }
    }

    // ���콺 ��Ŭ���� ��ġ�� ��ǥ ������ ����
    private void SetTargetPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);  // ���콺 ��ġ���� ������ �߻�
        RaycastHit hit;

        // ������ �浹�� ������ ������ �� ������ ��ǥ ��ġ�� ����
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Ground" || hit.transform.tag == "Item")
        {
            targetPosition = hit.point;  // �浹�� ������ ��ǥ�� ����
            isMoving = true;             // �̵� ����
        }
    }

    // ĳ���͸� ��ǥ �������� �̵�
    private void MoveToTarget()
    {
        // ���� ��ġ�� �ʹ� �־����� �������� ����
        if(Vector3.Distance(rb.position, targetPosition) > 15f)
        {
            isMoving = false;
        }
        // ���� ��ġ���� ��ǥ �������� �ε巴�� �̵�
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);  // Rigidbody�� ����Ͽ� �̵�

        // �̵� ���� ���
        Vector3 moveDirection = (targetPosition - rb.position).normalized;

        // �ִϸ����� �Ķ���� ������Ʈ
        anim.SetFloat("MoveX", moveDirection.x);
        anim.SetFloat("MoveY", moveDirection.z); // Z���� Y������ ���

        // ĳ���Ͱ� ��ǥ ������ ���� �����ϸ� �̵��� ����
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

    // �տ� ��ü�� �ִ��� Ȯ��
    private void CheckFront()
    {
        isMoving = false; // �̵� ����
    }

    // �ִϸ����� ���� ������Ʈ
    private void UpdateAnimator()
    {
        // ĳ���Ͱ� �̵����� �ʴ´ٸ� �ִϸ��̼��� ����
        if (!isMoving)
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);
        }
    }
}
