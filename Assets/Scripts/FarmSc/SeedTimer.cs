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

    private float timer = 0f;

    private void Update()
    {
        SetSprite();
    }

    private void SetSprite()
    {
        Vector3Int gridPos = seedTile.WorldToCell(gameObject.transform.position);

        this.timer += Time.deltaTime;
        if (this.timer == 5f)
        {
            this.seedTile.SetTile(gridPos, null);
            this.timer = 0f;
        }
    }
}
