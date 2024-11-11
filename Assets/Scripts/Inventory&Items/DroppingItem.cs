using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DroppingItem : MonoBehaviour
{
    [SerializeField] Dictionary<Vector3Int, CropTile> cropTiles;
    [SerializeField] TileBase dried;
    [SerializeField] Tilemap targetTile;
    [SerializeField] Tilemap seedTile;

    [SerializeField] GameObject dropItem; // 드랍될 아이템
    [SerializeField] float spread = 0.7f; // 아이템이 퍼질 범위

    [SerializeField] Item item; // 아이템 종류
    [SerializeField] int itemCntDrop = 1; // 드랍될 아이템의 개수
    [SerializeField] int dropCnt = 5; // 드랍 횟수

    public float yOffset = 0f; // 높이 설정

    // 식물을 수확하거나 제거하는 메서드
    public void HarvestPlant(Vector3Int position)
    {
        if(targetTile == null)
        {
            return;
        }
        if (cropTiles.ContainsKey(position) && cropTiles[position].hasGrown)
        {
            // 식물 제거 처리 (필요하다면 식물 프리팹도 삭제)
            // 해당 타일의 프리팹이 있다면, 관련 오브젝트 삭제

            // 타일 상태 초기화
            cropTiles[position].hasSeed = false; // 씨앗 상태 초기화
            cropTiles[position].isWatered = false; // 물 준 상태 초기화
            cropTiles[position].hasGrown = false; // 자란 상태 초기화
            cropTiles[position].growTimer = 0f; // 성장 타이머 초기화

            // 타일맵에서 해당 타일을 빈 상태로 변경 (예: 땅 타일)
            seedTile.SetTile(position, null); // 씨앗 타일 제거
            targetTile.SetTile(position, dried); // 다시 말라 있는 땅으로 변경

            Debug.Log("식물 수확 완료: " + position);
        }
    }

    public void Hit()
    {
        // 드랍 횟수만큼 아이템을 드랍
        while (dropCnt > 0)
        {
            dropCnt -= 1;

            Vector3 position = transform.position;
            position.x += spread * Random.value - spread / 2;
            position.z += spread * Random.value - spread / 2;
            position.y = yOffset;

            // 아이템 생성
            ItemSpawner.instance.SpawnItem(position, item, itemCntDrop);
        }

        // 물체를 제거
        Destroy(gameObject);
    }

    public void KillEnemy()
    {
        // 드랍 횟수만큼 아이템을 드랍
        while (dropCnt > 0)
        {
            dropCnt -= 1;

            Vector3 position = transform.position;
            position.x += spread * Random.value - spread / 2;
            position.z += spread * Random.value - spread / 2;
            position.y = yOffset;

            // 아이템 생성
            ItemSpawner.instance.SpawnItem(position, item, itemCntDrop);
        }

        // 에너미 비활성화
        gameObject.SetActive(false);
    }
}
