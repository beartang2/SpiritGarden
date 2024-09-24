using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    // �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� CuttingTree ��ũ��Ʈ�� ���� ���
        CuttingTree cuttingTree = other.GetComponent<CuttingTree>();

        if (cuttingTree != null)
        {
            Debug.Log("ť�갡 ������ �浹��!");
            cuttingTree.Hit(); // CuttingTree Ŭ������ Hit() �޼��带 ȣ��
        }
    }

    // �ݶ��̴��� �ٸ� ��ü�� �浹�� ������ �� ȣ���
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�浹�� ����!");
    }
}
