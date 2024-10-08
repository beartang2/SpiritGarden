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
    [SerializeField] TileMapReadController tileReadCont;
    [SerializeField] TileBase watered;
    [SerializeField] TileBase seeded;
    [SerializeField] TileBase dried;
    [SerializeField] Tilemap targetTile;
    [SerializeField] Tilemap seedTile;
    public bool isSeeded = false;
    [SerializeField] private float seedGrowthTime = 480f;
    [SerializeField] private float landDryTime = 240f;
    [SerializeField] GameObject growingPlantPrefab;

    Dictionary<Vector2Int, Crop> crops;
    private Vector3Int tilePos;
    private Vector3 seedTilePos;

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

        StartCoroutine(StartLandTimer(position));
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

        seedTilePos = seedTile.GetCellCenterWorld(position);

        StartCoroutine(StarPlantTimer(position ,seedTilePos));
    }

    //땅이 마르는 타이머 시작
    IEnumerator StartLandTimer(Vector3Int tilePos)
    {
        yield return new WaitForSeconds(landDryTime); // 하루 동안 대기

        //땅이 마름
        BackToLandTile(tilePos);
    }

    //씨앗 자라는 타이머 시작
    IEnumerator StarPlantTimer(Vector3Int delTilePos ,Vector3 tilePos)
    {
        yield return new WaitForSeconds(seedGrowthTime); // 이틀 동안 대기

        Debug.Log("plant");
        //식물 생성
        Instantiate(growingPlantPrefab, tilePos, Quaternion.Euler(0f, 45f, 0f)); // GrowingPlant 스크립트를 가진 프리팹 생성
        //씨앗 타일 제거
        seedTile.SetTile(delTilePos, watered);
    }
}
