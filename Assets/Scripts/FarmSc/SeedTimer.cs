using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeedTimer : MonoBehaviour
{
    [SerializeField] LightingManager timeManager;
    [SerializeField] TileMapReadController tileReadCont;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap seedTile;

    private void Update()
    {
        SetSprite();
        timeManager.CheckDay();

        if(timeManager.dayCount / 3 == 0) //3�� ����϶� ��, 3�ϸ���
        {

        }
    }

    private void SetSprite()
    {
        Vector3Int gridPos = seedTile.WorldToCell(gameObject.transform.position);
    }
}
