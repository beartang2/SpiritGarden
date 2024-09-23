using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] Transform player;   // �÷��̾� ĳ������ Transform
    [SerializeField] ItemSlot itemSlot;  // ������ ���� (�巡�� ���� ������ ����)
    [SerializeField] GameObject itemIcon;// �巡�� ���� �������� ǥ���� ������
    RectTransform iconTrans;             // �������� RectTransform
    Image itemImage;                     // �����ܿ� ǥ���� �̹���

    private void Start()
    {
        itemSlot = new ItemSlot();                         // ���ο� ������ ���� ����
        iconTrans = itemIcon.GetComponent<RectTransform>(); // �������� RectTransform ������Ʈ ��������
        itemImage = itemIcon.GetComponent<Image>();        // �������� �̹��� ������Ʈ ��������
    }

    private void Update()
    {
        if (itemIcon.activeInHierarchy == true) // ������ �������� Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
        {
            iconTrans.position = Input.mousePosition; // �������� ���콺 ��ġ�� �̵���Ŵ

            if (Input.GetMouseButtonDown(0)) // ���콺 ��Ŭ���� ����
            {
                if (EventSystem.current.IsPointerOverGameObject() == false) // UI ��� ���� �ƴ��� Ȯ��
                {
                    // ���콺 Ŭ�� ��ġ�� ���̷� ��ȯ�Ͽ� ���� ��ǥ�� ���
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity)) // Ŭ���� ������ ������ ���
                    {
                        // �÷��̾�� Ŭ���� ���� ������ ������ ����ϰ�, ���� �Ÿ��� �������� ����
                        Vector3 direction = (hit.point - player.position).normalized;
                        Vector3 spawnPosition = player.position + direction * 2f; // ĳ���ͷκ��� 2���� ������ ����
                        spawnPosition.y = 0.5f; // y�� ����

                        // ������ ����
                        ItemSpawner.instance.SpawnItem(
                            spawnPosition,   // �������� ������ ��ġ
                            itemSlot.item,   // �巡�� ���� ������ ����
                            itemSlot.count   // ������ ����
                        );

                        // �巡�� ���� ������ ���� �ʱ�ȭ
                        itemSlot.Clear();
                        itemIcon.SetActive(false); // ������ ��Ȱ��ȭ
                    }
                }
            }
        }
    }

    // ������ ���� Ŭ�� �� ó��
    internal void OnClick(ItemSlot itemSlot)
    {
        // ���� �巡�� ���� �������� ������ Ŭ���� ������ ���� ������ ������
        if (this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);  // Ŭ���� ������ ������ ������ ����
            itemSlot.Clear();              // Ŭ���� ���� �ʱ�ȭ
        }
        else // �巡�� ���� �������� �ִ� ��� ���� ��ü
        {
            Item item = itemSlot.item;     // Ŭ���� ������ ������ ������ ����
            int count = itemSlot.count;    // Ŭ���� ������ ������ ���� ����

            itemSlot.Copy(this.itemSlot);  // �巡�� ���� ������ ������ Ŭ���� ���Կ� ����
            this.itemSlot.Set(item, count);// Ŭ���� ������ ������ ������ �巡�� ���� ���Կ� ����
        }
        UpdateIcon();  // ������ ������Ʈ
    }

    // �������� ������Ʈ�Ͽ� �巡�� ���� �����ۿ� �°� ����
    private void UpdateIcon()
    {
        if (itemSlot.item == null)  // �巡�� ���� �������� ���� ���
        {
            itemIcon.SetActive(false); // �������� ��Ȱ��ȭ
        }
        else // �巡�� ���� �������� ���� ���
        {
            itemIcon.SetActive(true);      // �������� Ȱ��ȭ
            itemImage.sprite = itemSlot.item.icon; // �������� �������� ����
        }
    }
}
