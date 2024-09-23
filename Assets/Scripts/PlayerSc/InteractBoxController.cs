using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBoxController : MonoBehaviour
{
    public Transform player;      // ������ �Ǵ� ĳ����
    public Transform cube;        // ť��
    public float distance = 2f;   // ĳ���Ϳ� ť�� ������ ���� �Ÿ�
    public LayerMask groundLayer; // ���콺�� Ŭ���� �� �ִ� ���̾� (��: ��)
    public Collider cubeCollider;   // ť���� �ݶ��̴�

    private Vector3 lastDirection; // ť���� ������ ������ ����
    private float fixedY;          // ������ Y�� ��
    public float colliderActiveTime = 0.1f; // �ݶ��̴��� ��� Ȱ��ȭ�ϴ� �ð�

    void Start()
    {
        // ĳ������ Y ���� ����
        fixedY = player.position.y;
        // �ʱ� ť�� ���� ���� (�÷��̾���� �ʱ� �Ÿ�)
        lastDirection = (cube.position - player.position).normalized;

        // ť���� �ݶ��̴��� ó������ ��Ȱ��ȭ
        cubeCollider.enabled = false;
    }

    void Update()
    {
        // ���콺 ��Ŭ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 �����Ϳ��� ������ ���̸� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ���̰� ���� �¾Ҵ��� Ȯ��
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // Ŭ���� ��ġ�� ĳ���� ������ ���� ��� (Y�� ����)
                Vector3 targetDirection = (hit.point - player.position).normalized;
                targetDirection.y = 0;  // Y�� �����Ͽ� ���� ���⸸ ���

                // Ŭ���� ��ġ�� ĳ���� ������ �Ÿ��� ���
                float clickedDistance = Vector3.Distance(player.position, hit.point);

                // Ŭ�� ��ġ�� ���� �Ÿ����� ����� ���
                if (clickedDistance < distance)
                {
                    // ť�갡 �÷��̾�� ������ �Ÿ���ŭ ���������� ����
                    cube.position = player.position + targetDirection * distance;
                }
                else
                {
                    // ť�갡 �÷��̾�� ������ �Ÿ���ŭ ���������� ����
                    cube.position = player.position + targetDirection * distance;
                }

                // ť���� Y�� ����
                cube.position = new Vector3(cube.position.x, fixedY, cube.position.z);

                // ������ ������ ������Ʈ
                lastDirection = (cube.position - player.position).normalized;

                // �ݶ��̴��� ��� �״ٰ� ���� �ڷ�ƾ ����
                StartCoroutine(ActivateColliderTemporarily());
            }
        }
        else
        {
            // ť�갡 ������ �������� �÷��̾ ����ٴ�
            cube.position = player.position + lastDirection * distance;

            // ť���� Y�� ����
            cube.position = new Vector3(cube.position.x, fixedY, cube.position.z);
        }
    }

    // ť���� �ݶ��̴��� ��� Ȱ��ȭ�ߴٰ� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
    IEnumerator ActivateColliderTemporarily()
    {
        cubeCollider.enabled = true;              // �ݶ��̴� Ȱ��ȭ
        yield return new WaitForSeconds(colliderActiveTime); // ���� �ð� ���
        cubeCollider.enabled = false;             // �ݶ��̴� ��Ȱ��ȭ
    }
}
