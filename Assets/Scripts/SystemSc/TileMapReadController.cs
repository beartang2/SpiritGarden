using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class TileMapReadController : MonoBehaviour
{
    public Tilemap tileMap; // Å¸ÀÏ¸Ê
    public Tilemap seedTileMap; // ¾¾¾ÑÅ¸ÀÏ¸Ê
    [SerializeField] Grid grid;
    [SerializeField] List<TileData> tileData;
    Dictionary<TileBase, TileData> dataFromTile;
    [SerializeField] Transform cube;     // Å¥ºê

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
        /*
        if(Input.GetMouseButtonDown(0))
        {
            GetTileBase(GetGridPosition(cube.position));
        }
        */
    }

    public Vector3Int GetGridPosition(Vector3 position)
    {
        Vector3 worldPos;


        worldPos = position;


        Vector3Int gridPos = grid.WorldToCell(worldPos);

        return gridPos;
    }

    public TileBase GetTileBase(Vector3Int gridPos, Tilemap map)
    {
        TileBase tile = map.GetTile(gridPos);

        Debug.Log(map.name + " : " + gridPos + " : " +  tile);

        return tile;
    }
    
    public TileData GetTileData(TileBase tileBase)
    {
        return dataFromTile[tileBase];
    }
}
