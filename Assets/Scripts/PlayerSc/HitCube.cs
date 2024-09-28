using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    // �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� DroppingItem ��ũ��Ʈ�� ���� ���
        DroppingItem droppingItem = other.GetComponent<DroppingItem>();

        if (droppingItem != null)
        {
            Debug.Log("ť�갡 ������ �浹��!");
            droppingItem.Hit(); // DroppingItem Ŭ������ Hit() �޼��带 ȣ��
        }
    }

    // �ݶ��̴��� �ٸ� ��ü�� �浹�� ������ �� ȣ���
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�浹�� ����!");
    }
}
