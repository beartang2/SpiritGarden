using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CropTile
{
    public float growTimer; // ���� �ð�
    public bool isWatered; // ���� ����� ����
    public bool hasSeed; // ������ �ɾ������� ����
    public bool hasGrown; // �Ĺ��� �ڶ����� ����
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
    [SerializeField] GameObject growingPlantPrefab; // �Ĺ� ������Ʈ

    Dictionary<Vector2Int, Crop> crops;
    private Vector3Int tilePos; // Ÿ�� ��ġ
    private Vector3 seedTilePos; // ���� Ÿ�� ��ġ

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

            // ���� �� ���¶�� �ð��� �帧
            if (cropTile.isWatered && cropTile.hasSeed)
            {
                cropTile.growTimer += Time.deltaTime;

                // ������ �ڶ� �ð��� �Ǿ�����
                if (cropTile.growTimer >= seedGrowthTime)
                {
                    tilesToGrow.Add(position); // �ڶ� Ÿ���� ���
                }
            }
        }
        // �ڶ� �ð��� �� Ÿ�Ͽ� �Ĺ� ����
        foreach (var tilePos in tilesToGrow)
        {
            GrowPlant(tilePos); // �Ĺ� ���� ó��
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
            cropTiles[position].isWatered = true; // ���� �� ���·� ����
            targetTile.SetTile(position, watered); // ���� �� Ÿ�Ϸ� ����
            Debug.Log("�� �� Ÿ��: " + position);
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

    //���� ������ Ÿ�̸� ����
    IEnumerator StartLandTimer(Vector3Int tilePos)
    {
        yield return new WaitForSeconds(landDryTime); // �Ϸ� ���� ���

        //���� ����
        targetTile.SetTile(tilePos, dried);

        Debug.Log("dried");
    }

    /*
    //���� �ڶ�� Ÿ�̸� ����
    IEnumerator StarPlantTimer(Vector3Int delTilePos ,Vector3 tilePos)
    {
        yield return new WaitForSeconds(seedGrowthTime); // ��Ʋ ���� ���

        Debug.Log("plant");
        //�Ĺ� ����
        Instantiate(growingPlantPrefab, tilePos, Quaternion.Euler(0f, 45f, 0f)); // GrowingPlant ��ũ��Ʈ�� ���� ������ ����
        //���� Ÿ�� ����
        seedTile.SetTile(delTilePos, null);
    }
    */

    // �Ĺ��� �ڶ�� ó��
    private void GrowPlant(Vector3Int position)
    {
        Vector3 plantPosition = seedTile.GetCellCenterWorld(position);
        //�Ĺ� ����
        Instantiate(growingPlantPrefab, plantPosition, Quaternion.Euler(0f, 45f, 0f));

        DeleteSeedTile(position);
    }

    private void DeleteSeedTile(Vector3Int delTilePos)
    {
        //���� Ÿ�� ����
        seedTile.SetTile(delTilePos, null);
    }
}
