using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public int growTimer;
    public Crop crop;
}

public class CropManager : MonoBehaviour
{
    [SerializeField] TileBase watered;
    [SerializeField] TileBase seeded;
    [SerializeField] TileBase dried;
    [SerializeField] Tilemap targetTile;
    [SerializeField] Tilemap seedTile;
    public bool isSeeded = false;

    Dictionary<Vector2Int, Crop> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, Crop>();
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    public void BackToLandTile(Vector3Int position)
    {
        targetTile.SetTile((Vector3Int)position, dried);

        Debug.Log("dried");
    }

    public void Watering(Vector3Int position)
    {
        //if(crops.ContainsKey((Vector2Int)position))
        //{
        //    return;
        //}

        CreateWateredTile(position);
    }

    private void CreateWateredTile(Vector3Int position)
    {
        //Crop crop = new Crop();
        //crops.Add((Vector2Int)position, crop);

        targetTile.SetTile((Vector3Int)position, watered);

        Debug.Log("watered");
    }

    public void Seed(Vector3Int position)
    {
        //if (crops.ContainsKey((Vector2Int)position))
        //{
        //    return;
        //}

        CreateSeededTile(position);
    }

    private void CreateSeededTile(Vector3Int position)
    {
        //Crop crop = new Crop();
        //crops.Add((Vector2Int)position, crop);

        seedTile.SetTile((Vector3Int)position, seeded);

        Debug.Log("seeded");
        isSeeded = true;
    }
}
