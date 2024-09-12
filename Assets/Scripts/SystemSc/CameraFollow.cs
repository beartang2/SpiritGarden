using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;     // ���� ĳ����
    private Vector3 offset;      // ī�޶�� ĳ���� ������ ������ �Ÿ�

    void Start()
    {
        // ������ �� ĳ���Ϳ� ī�޶� ������ �ʱ� ��ġ ���̸� offset���� ����
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // ĳ������ ��ġ�� offset�� ���� ī�޶� ��ġ�� ��� ����
        transform.position = player.position + offset;
    }
}
