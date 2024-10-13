using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CropTile
{
    public float growTimer; // 성장 시간
    public bool isWatered; // 물을 줬는지 여부
    public bool hasSeed; // 씨앗이 심어졌는지 여부
    public bool hasGrown; // 식물이 자랐는지 여부
    public Crop crop;
}

public class CropManager : MonoBehaviour
{
    [SerializeField] PlayerToolController playerCont;
    [SerializeField] TileMapReadController tileReadCont;
    [SerializeField] TileBase watered;
    [SerializeField] TileBase seeded;
    [SerializeField] TileBase dried;
    [SerializeField] Tilemap targetTile;
    [SerializeField] Tilemap seedTile;
    //public bool isSeeded = false;
    [SerializeField] private float seedGrowthTime = 480f;
    [SerializeField] private float landDryTime = 240f;
    [SerializeField] GameObject growingPlantPrefab; // 식물 오브젝트

    Dictionary<Vector2Int, Crop> crops;
    private Vector3Int tilePos; // 타일 위치
    private Vector3 seedTilePos; // 씨앗 타일 위치

    //private float timer = 0f;

    private Dictionary<Vector3Int, CropTile> cropTiles = new Dictionary<Vector3Int, CropTile>();


    private void Start()
    {
        crops = new Dictionary<Vector2Int, Crop>();
        //timer = 0f;
    }

    private void Update()
    {
        UpdateCrop();
    }

    private void UpdateCrop()
    {
        List<Vector3Int> tilesToGrow = new List<Vector3Int>();

        foreach (var kvp in cropTiles)
        {
            Vector3Int position = kvp.Key;
            CropTile cropTile = kvp.Value;

            // 물을 준 상태라면 시간이 흐름
            if (cropTile.isWatered && cropTile.hasSeed)
            {
                cropTile.growTimer += Time.deltaTime;

                // 씨앗이 자랄 시간이 되었으면
                if (cropTile.growTimer >= seedGrowthTime)
                {
                    tilesToGrow.Add(position); // 자랄 타일을 기록
                }
            }
        }
        // 자랄 시간이 된 타일에 식물 생성
        foreach (var tilePos in tilesToGrow)
        {
            GrowPlant(tilePos); // 식물 성장 처리
        }
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

        if (cropTiles.ContainsKey(position))
        {
            cropTiles[position].isWatered = true; // 물을 준 상태로 변경
            targetTile.SetTile(position, watered); // 물을 준 타일로 변경
            Debug.Log("물 준 타일: " + position);
        }

        //targetTile.SetTile((Vector3Int)position, watered);

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

        if(!cropTiles.ContainsKey(position))
        {
            CropTile newCropTile = new CropTile { growTimer = 0f, isWatered = false, hasSeed = true };
            cropTiles.Add(position, newCropTile);
            seedTile.SetTile((Vector3Int)position, seeded);

            Debug.Log("seeded");
        }

        //StartCoroutine(StarPlantTimer(position ,seedTilePos));
    }

    //땅이 마르는 타이머 시작
    IEnumerator StartLandTimer(Vector3Int tilePos)
    {
        yield return new WaitForSeconds(landDryTime); // 하루 동안 대기

        //땅이 마름
        targetTile.SetTile(tilePos, dried);

        Debug.Log("dried");
    }

    /*
    //씨앗 자라는 타이머 시작
    IEnumerator StarPlantTimer(Vector3Int delTilePos ,Vector3 tilePos)
    {
        yield return new WaitForSeconds(seedGrowthTime); // 이틀 동안 대기

        Debug.Log("plant");
        //식물 생성
        Instantiate(growingPlantPrefab, tilePos, Quaternion.Euler(0f, 45f, 0f)); // GrowingPlant 스크립트를 가진 프리팹 생성
        //씨앗 타일 제거
        seedTile.SetTile(delTilePos, null);
    }
    */

    // 식물이 자라는 처리
    private void GrowPlant(Vector3Int position)
    {
        Vector3 plantPosition = seedTile.GetCellCenterWorld(position);
        //식물 생성
        Instantiate(growingPlantPrefab, plantPosition, Quaternion.Euler(0f, 45f, 0f));

        DeleteSeedTile(position);
    }

    private void DeleteSeedTile(Vector3Int delTilePos)
    {
        //씨앗 타일 제거
        seedTile.SetTile(delTilePos, null);
    }
}
