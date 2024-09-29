using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tileMap; // 타일맵
    Transform cube;     // 큐브

    private void Awake()
    {
        cube = GetComponent<Transform>();
    }

    private void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        {
            //GetTileBase(cube.position);
        }
    }

    public TileBase GetTileBase(Vector3 cubePos)
    {
        //월드포지션

        //Vector3Int gridPos = tileMap.WorldToCell(cubePos);

        //TileBase tile = tileMap.GetTile(gridPos);

        //Debug.Log("Tile : " + gridPos + " : " +  tile);

        return null;
    }
        
}
