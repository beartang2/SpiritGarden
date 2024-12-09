using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractBoxController : MonoBehaviour
{
    public Transform player;   // ������ �Ǵ� ĳ����
    public Transform cube;     // ť��
    [SerializeField] float distance = 2f;   // ĳ���Ϳ� ť�� ������ ���� �Ÿ�
    [SerializeField] LayerMask groundLayer; // ���콺�� Ŭ���� �� �ִ� ���̾� (��: ��)
    [SerializeField] Collider cubeCollider; // ť���� �ݶ��̴�

    private Vector3 lastDirection; // ť���� ������ ������ ����
    private float fixedY;          // ������ Y�� ��
    [SerializeField] float colliderActiveTime = 0.1f; // �ݶ��̴��� ��� Ȱ��ȭ�ϴ� �ð�

    ToolBarController toolbarCont; // �� �� ��Ʈ�ѷ�
    [SerializeField] SpriteRenderer toolSprite; // ���� �̹���

    private void Start()
    {
        // ĳ������ Y ���� ����
        fixedY = player.position.y;

        // �ʱ� ť�� ���� ���� (�÷��̾���� �ʱ� �Ÿ�)
        lastDirection = (cube.position - player.position).normalized;

        // ť���� �ݶ��̴��� ó������ ��Ȱ��ȭ
        cubeCollider.enabled = false;

        toolbarCont = gameObject.GetComponent<ToolBarController>();
    }

    void Update()
    {
        UpdateToolIcon();

        // ���콺 �����Ϳ��� ������ ���̸� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ���콺 ��Ŭ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            // �ݶ��̴��� ��� �״ٰ� ���� �ڷ�ƾ ����
            StartCoroutine(ActivateColliderTemporarily());
        }
        else
        {
            // groundLayer���� �浹 ���� Ȯ��
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // ������ ������ ������Ʈ
                lastDirection = (hit.point - player.position).normalized;
            }
            else
            {
                // �浹 ����: ī�޶� ����� �������� ���
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // y=0 ���

                if (groundPlane.Raycast(ray, out float distanceToPlane))
                {
                    Vector3 hitPoint = ray.GetPoint(distanceToPlane);
                    lastDirection = (hitPoint - player.position).normalized;
                }
            }


            // ť�갡 ������ �������� �÷��̾ ����ٴ�
            cube.position = player.position + lastDirection * distance;

            // ť���� Y�� ����
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

        // ���⿡ ���� x,y �ø� ���
        Vector3 flipIcon = cube.position - player.position;

        if(flipIcon.x - flipIcon.z > 0)
        {
            toolSprite.flipX = true;
        }
        else
        {
            toolSprite.flipX = false;
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
