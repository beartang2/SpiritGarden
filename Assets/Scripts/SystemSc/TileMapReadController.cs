using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tileMap; // Ÿ�ϸ�
    Transform cube;     // ť��

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
        //����������

        //Vector3Int gridPos = tileMap.WorldToCell(cubePos);

        //TileBase tile = tileMap.GetTile(gridPos);

        //Debug.Log("Tile : " + gridPos + " : " +  tile);

        return null;
    }
        
}
