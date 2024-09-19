using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject pickItemPrefab;

    public void SpawnItem(Vector3 position, Item item, int count)
    {
        GameObject p = Instantiate(pickItemPrefab, position, Quaternion.identity);

    }
}