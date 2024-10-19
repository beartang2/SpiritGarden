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
        Vector3 boxCenter = boxCollider.bounds.center; // �ڽ� �ݶ��̴��� �߽� ��ġ
        Vector3 boxSize = boxCollider.bounds.size;     // �ڽ� �ݶ��̴��� ���� ũ��

        // �ڽ� �ݶ��̴� ���� �� ��� �ݶ��̴� Ž��
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.identity);

        //stoneList.Clear(); // ����Ʈ �ʱ�ȭ

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Stone")) // �±װ� "stone"�� ������Ʈ���� Ȯ��
            {
                objectList.Add(collider.gameObject); // ����Ʈ�� �߰�
            }
            if (collider.CompareTag("Tree")) // �±װ� "tree"�� ������Ʈ���� Ȯ��
            {
                objectList.Add(collider.gameObject); // ����Ʈ�� �߰�
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
                if (randIndex == 0) // ���̸� ���̰� ���� ����
                {
                    yOffset = 0.5f;
                }
                else if(randIndex == 1) // ������ ���̰� ���� ����
                {
                    yOffset = 2f;
                }
                GameObject spawnObj = Instantiate(farmPrefab[randIndex], randomPosition(yOffset), Quaternion.Euler(0f, 45f, 0f));
                objectList.Add(spawnObj);
            }
        }
    }
}
