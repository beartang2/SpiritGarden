using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public int growTimer;
    public Crop crop;
}

public class CropManager : LightingManager
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTile;

    Dictionary<Vector3Int, CropTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector3Int, CropTile>();
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector3Int)position);
    }

    private void Update()
    {
        
    }
}
