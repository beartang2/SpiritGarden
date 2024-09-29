using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingItem : MonoBehaviour
{
    [SerializeField] GameObject dropItem; // ����� ������
    [SerializeField] float spread = 0.7f; // �������� ���� ����

    [SerializeField] Item item; // ������ ����
    [SerializeField] int itemCntDrop = 1; // ����� �������� ����
    [SerializeField] int dropCnt = 5; // ��� Ƚ��

    public void Hit()
    {
        // ��� Ƚ����ŭ �������� ���
        while (dropCnt > 0)
        {
            dropCnt -= 1;

            Vector3 position = transform.position;
            position.x += spread * Random.value - spread / 2;
            position.y += spread * Random.value - spread / 2;

            // ������ ����
            ItemSpawner.instance.SpawnItem(position, item, itemCntDrop);
        }

        // ��ü�� ����
        Destroy(gameObject);
    }
}
