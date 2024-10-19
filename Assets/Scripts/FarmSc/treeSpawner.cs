using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnRange;
    BoxCollider range;
    [SerializeField] GameObject[] farmPrefab;
    public float delay = 10f;
    private int maxCount = 10;
    private BoxCollider boxCollider;
    private int randIndex;
    private float yOffset;

    public List<GameObject> objectList = new List<GameObject>();

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

    private void Update()
    {
        randIndex = Random.Range(0, 2);
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
                objectList.Add(collider.gameObject); // 리스트에 추가
            }
            if (collider.CompareTag("Tree")) // 태그가 "tree"인 오브젝트인지 확인
            {
                objectList.Add(collider.gameObject); // 리스트에 추가
            }
        }
    }

    Vector3 randomPosition(float y)
    {
        Vector3 originPos = spawnRange.transform.position;

        float rangeX = range.bounds.size.x;
        float rangeZ = range.bounds.size.z;

        rangeX = Random.Range((rangeX / 2) * -1, rangeX / 2);
        rangeZ = Random.Range((rangeZ / 2) * -1, rangeZ / 2);
        Vector3 randPos = new Vector3(rangeX, 0f, rangeZ);

        Vector3 respawnPos = originPos + randPos;
        respawnPos.y = y;

        return respawnPos;
    }

    IEnumerator randomSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            if (objectList.Count < maxCount)
            {
                if (randIndex == 0) // 돌이면 높이가 낮게 설정
                {
                    yOffset = 0.5f;
                }
                else if(randIndex == 1) // 나무면 높이가 높게 설정
                {
                    yOffset = 2f;
                }
                GameObject spawnObj = Instantiate(farmPrefab[randIndex], randomPosition(yOffset), Quaternion.Euler(0f, 45f, 0f));
                objectList.Add(spawnObj);
            }
        }
    }
}
