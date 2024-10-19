using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnRange;
    BoxCollider range;
    [SerializeField] GameObject stonePrefab;
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

    IEnumerator randomSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            if(stoneList.Count < maxCount)
            {
                GameObject spawnStone = Instantiate(stonePrefab, randomPosition(), Quaternion.Euler(0f, 45f, 0f));
                stoneList.Add(spawnStone);
            }
        }
    }
}
