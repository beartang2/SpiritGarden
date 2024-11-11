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

    [SerializeField] GameObject dropItem; // ����� ������
    [SerializeField] float spread = 0.7f; // �������� ���� ����

    [SerializeField] Item item; // ������ ����
    [SerializeField] int itemCntDrop = 1; // ����� �������� ����
    [SerializeField] int dropCnt = 5; // ��� Ƚ��

    public float yOffset = 0f; // ���� ����

    // �Ĺ��� ��Ȯ�ϰų� �����ϴ� �޼���
    public void HarvestPlant(Vector3Int position)
    {
        if(targetTile == null)
        {
            return;
        }
        if (cropTiles.ContainsKey(position) && cropTiles[position].hasGrown)
        {
            // �Ĺ� ���� ó�� (�ʿ��ϴٸ� �Ĺ� �����յ� ����)
            // �ش� Ÿ���� �������� �ִٸ�, ���� ������Ʈ ����

            // Ÿ�� ���� �ʱ�ȭ
            cropTiles[position].hasSeed = false; // ���� ���� �ʱ�ȭ
            cropTiles[position].isWatered = false; // �� �� ���� �ʱ�ȭ
            cropTiles[position].hasGrown = false; // �ڶ� ���� �ʱ�ȭ
            cropTiles[position].growTimer = 0f; // ���� Ÿ�̸� �ʱ�ȭ

            // Ÿ�ϸʿ��� �ش� Ÿ���� �� ���·� ���� (��: �� Ÿ��)
            seedTile.SetTile(position, null); // ���� Ÿ�� ����
            targetTile.SetTile(position, dried); // �ٽ� ���� �ִ� ������ ����

            Debug.Log("�Ĺ� ��Ȯ �Ϸ�: " + position);
        }
    }

    public void Hit()
    {
        // ��� Ƚ����ŭ �������� ���
        while (dropCnt > 0)
        {
            dropCnt -= 1;

            Vector3 position = transform.position;
            position.x += spread * Random.value - spread / 2;
            position.z += spread * Random.value - spread / 2;
            position.y = yOffset;

            // ������ ����
            ItemSpawner.instance.SpawnItem(position, item, itemCntDrop);
        }

        // ��ü�� ����
        Destroy(gameObject);
    }

    public void KillEnemy()
    {
        // ��� Ƚ����ŭ �������� ���
        while (dropCnt > 0)
        {
            dropCnt -= 1;

            Vector3 position = transform.position;
            position.x += spread * Random.value - spread / 2;
            position.z += spread * Random.value - spread / 2;
            position.y = yOffset;

            // ������ ����
            ItemSpawner.instance.SpawnItem(position, item, itemCntDrop);
        }

        // ���ʹ� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
