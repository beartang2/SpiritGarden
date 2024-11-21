using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OreType
{
    public GameObject prefab; // 광석 프리팹
    public float spawnChance; // 스폰 확률 (0 ~ 1 사이)
}

public class stoneSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnRange;
    BoxCollider range;
    [SerializeField] public List<OreType> oreTypes; // 여러 광석의 종류와 확률
    //[SerializeField] GameObject stonePrefab;
    public float delay = 10f;
    private int maxCount = 10;
    private BoxCollider boxCollider;

    public List<GameObject> stoneList = new List<GameObject> ();

    private void Start()
    {
        StartCoroutine(randomSpawn());
        DetectStones();
    }

    void Awake()
    {
        range = spawnRange.GetComponent<BoxCollider>();
        boxCollider = spawnRange.GetComponent<BoxCollider>();
    }

    void DetectStones()
    {
        Vector3 boxCenter = boxCollider.bounds.center; // 박스 콜라이더의 중심 위치
        Vector3 boxSize = boxCollider.bounds.size;     // 박스 콜라이더의 실제 크기

        // 박스 콜라이더 범위 내 모든 콜라이더 탐색
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.identity);

        //stoneList.Clear(); // 리스트 초기화

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Stone")) // 태그가 "stone"인 오브젝트인지 확인
            {
                stoneList.Add(collider.gameObject); // 리스트에 추가
            }
        }
    }

    Vector3 randomPosition()
    {
        Vector3 originPos = spawnRange.transform.position;

        float rangeX = range.bounds.size.x;
        float rangeZ = range.bounds.size.z;

        rangeX = Random.Range((rangeX / 2) * -1, rangeX / 2);
        rangeZ = Random.Range((rangeZ / 2) * -1, rangeZ / 2);
        Vector3 randPos = new Vector3(rangeX, 0f, rangeZ);

        Vector3 respawnPos = originPos + randPos;

        return respawnPos;
    }

    GameObject GetRandomOre()
    {
        float totalChance = 0f;

        // 확률의 총합 계산
        foreach (var ore in oreTypes)
        {
            totalChance += ore.spawnChance;
        }

        // 0 ~ totalChance 사이의 랜덤 값 생성
        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        // 랜덤 값에 해당하는 광석 선택
        foreach (var ore in oreTypes)
        {
            cumulativeChance += ore.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return ore.prefab;
            }
        }

        // 기본값 반환 (예외 상황 방지)
        return oreTypes[0].prefab;
    }


    IEnumerator randomSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            if(stoneList.Count < maxCount)
            {
                GameObject selectOre = GetRandomOre();
                GameObject spawnStone = Instantiate(selectOre, randomPosition(), Quaternion.Euler(0f, 45f, 0f));
                stoneList.Add(spawnStone);
            }
        }
    }
}
