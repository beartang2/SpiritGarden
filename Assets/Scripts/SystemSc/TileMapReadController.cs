using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tileMap; // ≈∏¿œ∏ 
    [SerializeField] Grid grid;
    [SerializeField] List<TileData> tileData;
    Dictionary<TileBase, TileData> dataFromTile;
    [SerializeField] Transform cube;     // ≈•∫Í

    private void Awake()
    {
        dataFromTile = new Dictionary<TileBase, TileData>();
        grid = tileMap.layoutGrid;

        foreach (TileData tileData in tileData)
        {
            foreach(TileBase tile in tileData.tiles)
            {
                dataFromTile.Add(tile, tileData);
            }
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GetTileBase(GetGridPosition(cube.position, true));

        }
    }

    public Vector3Int GetGridPosition(Vector3 position, bool cubePos)
    {
        Vector3 worldPos;

        if (cubePos)
        {
            worldPos = position;
        }
        else
        {
            worldPos = position;
        }

        Vector3Int gridPos = grid.WorldToCell(worldPos);

        return gridPos;
    }

    public TileBase GetTileBase(Vector3Int gridPos)
    {
        TileBase tile = tileMap.GetTile(gridPos);

        Debug.Log("Tile : " + gridPos + " : " +  tile);

        return null;
    }
    
    public TileData GetTileData(TileBase tileBase)
    {

        return dataFromTile[tileBase];
    }
}
