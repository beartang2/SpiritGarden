using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingItem : MonoBehaviour
{
    [SerializeField] GameObject dropItem; // 드랍될 아이템
    [SerializeField] float spread = 0.7f; // 아이템이 퍼질 범위

    [SerializeField] Item item; // 아이템 종류
    [SerializeField] int itemCntDrop = 1; // 드랍될 아이템의 개수
    [SerializeField] int dropCnt = 5; // 드랍 횟수

    public void Hit()
    {
        // 드랍 횟수만큼 아이템을 드랍
        while (dropCnt > 0)
        {
            dropCnt -= 1;

            Vector3 position = transform.position;
            position.x += spread * Random.value - spread / 2;
            position.y += spread * Random.value - spread / 2;

            // 아이템 생성
            ItemSpawner.instance.SpawnItem(position, item, itemCntDrop);
        }

        // 물체를 제거
        Destroy(gameObject);
    }
}
