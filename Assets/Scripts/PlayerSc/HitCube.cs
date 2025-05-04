using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class HitCube : MonoBehaviour
{
    [SerializeField] stoneSpawner stspawner; // �� ������ ��ũ��Ʈ
    [SerializeField] treeSpawner trstspawner; // ����&�� ������ ��ũ��Ʈ
    [SerializeField] DroppingItem dropItemSc; // ������ ��� ��ũ��Ʈ
    [SerializeField] TileMapReadController tileReadCont; // Ÿ���� �д� ��ũ��Ʈ
    [SerializeField] Rebuilding building; //���� ��ũ��Ʈ
    [SerializeField] RecipeList recipeList; // ������ ����Ʈ
    [SerializeField] LightingManager lightManager; // ������ �Ŵ��� ��ũ��Ʈ
    [SerializeField] CameraManager camSc; // ī�޶� ��ũ��Ʈ
    [SerializeField] ToolBarController toolbarCont;
    [SerializeField] AudioClip[] sfxs;
    LightingPreset preset;
    Item item;
    AudioSource audioSource;

    private BuildingSprites buildingSprites;
    private SpriteRenderer buildingSpriteRenderer;
    private Sprite sprites; // sprite �迭

    private Vector3Int tilePos;

    private float delay = 0f;
    private bool delaying = true;

    //private int id = -1; // ���๰ ��ȣ �ʱ�ȭ

    private void Start()
    {
        // builded bool ���� �ʱ�ȭ
        InitializeRecipes();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (delaying)
        {
            delay += Time.deltaTime;
            if (delay > 1f)
            {
                delaying = false; // ������ ���� ����
                delay = 0f; // ������ �ʱ�ȭ
            }
        }
    }

    // �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        // ���� ������ ���� ��������
        item = toolbarCont.GetItem;

        // �浹�� ��ü�� ��ũ��Ʈ�� �����´�
        DroppingItem droppingItem = other.GetComponent<DroppingItem>();
        EnemyHp enemyHp = other.GetComponent<EnemyHp>();

        if (!delaying && enemyHp != null) // ���ʹ� ü�� ��ũ��Ʈ�� �����ϸ� -> ���ʹ�
        {
            if (other.CompareTag("Enemy") && item.Name == "Sword")
            {
                delaying = true;
                audioSource.PlayOneShot(sfxs[0]);
                enemyHp.TakeDamage(20);
                Debug.Log("'��'���� ���� Ÿ���Ͽ���! 10 ������");
            }
            if (other.CompareTag("Enemy") && item.Name == "Pickaxe")
            {
                delaying = true;
                audioSource.PlayOneShot(sfxs[0]);
                enemyHp.TakeDamage(14);
                Debug.Log("'���'�� ���� Ÿ���Ͽ���! 8 ������");
            }
            if (other.CompareTag("Enemy") && item.Name == "Sickle")
            {
                delaying = true;
                audioSource.PlayOneShot(sfxs[0]);
                enemyHp.TakeDamage(8);
                Debug.Log("'��'���� ���� Ÿ���Ͽ���! 4 ������");
            }
            if (other.CompareTag("Enemy") && item.Name == null)
            {
                delaying = true;
                audioSource.PlayOneShot(sfxs[1]);
                enemyHp.TakeDamage(4);
                Debug.Log("'��'���� ���� Ÿ���Ͽ���! 2 ������");
            }
        }
        else if (droppingItem != null) // ��������� ��ũ��Ʈ�� �����ϸ� -> ��ü
        {
            if (other.CompareTag("Plant") && tileReadCont != null)
            {
                if (item.Name == "Sickle")
                {
                    Debug.Log("Ǯ");
                    tilePos = tileReadCont.GetGridPosition(gameObject.transform.position);
                    dropItemSc.HarvestPlant(tilePos);
                    droppingItem.Hit();
                }
            }

            DistructionManager distructObj = other.GetComponent<DistructionManager>();
            if (distructObj != null)
            {
                if (other.CompareTag("Stone") && item.Name == "Pickaxe")
                {
                    if (!delaying)
                    {
                        delaying = true;
                        if (distructObj.currentHits > 0 && distructObj.TakeHit())
                        {
                            distructObj.PlaySFX();
                            StartCoroutine(DestroyObject(other));
                        }
                    }
                }
                if (other.CompareTag("Tree") && item.Name == "Pickaxe")
                {
                    if (!delaying)
                    {
                        delaying = true;
                        if (distructObj.currentHits > 0 && distructObj.TakeHit())
                        {
                            distructObj.PlaySFX();
                            StartCoroutine(DestroyObject(other));
                        }
                    }
                }
                Debug.Log("ť�갡 �浹��!");
            }
        }
        else // ��������� ��ũ��Ʈ�� �������� ������ -> �ǹ� ������Ʈ
        {
            // �ǹ� ó��
            ProcessBuilding(other);
        }
        //else // ��������� ��ũ��Ʈ�� �������� ������ -> �ǹ� ������Ʈ
        //{
        //    // �ǹ� ������ ���̵� �ο�
        //    if (other.CompareTag("Building1"))
        //    {
        //        buildingSpriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
        //        buildingSprites = other.GetComponentInChildren<BuildingSprites>();
        //        sprites = buildingSprites.buildSprites; // sprite ����

        //        id = 0;
        //        if (!recipeList.recipes[id].builded)
        //        {
        //            recipeList.recipes[id].builded = true;
        //            Debug.Log("it's Building1");
        //            building.Rebuild(recipeList.recipes[id]);

        //            // �ǹ��� �������ٸ� sprite ����
        //            if (recipeList.recipes[id].builded == true)
        //            {
        //                buildingSpriteRenderer.sprite = sprites;

        //            }
        //        }
        //    }
        //    else if (other.CompareTag("Building2"))
        //    {
        //        buildingSpriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
        //        buildingSprites = other.GetComponentInChildren<BuildingSprites>();
        //        sprites = buildingSprites.buildSprites; // sprite ����

        //        id = 1;
        //        if (!recipeList.recipes[id].builded)
        //        {
        //            recipeList.recipes[id].builded = true;
        //            Debug.Log("it's Building2");
        //            building.Rebuild(recipeList.recipes[id]);

        //            // �ǹ��� �������ٸ� sprite ����
        //            if (recipeList.recipes[id].builded == true)
        //            {
        //                buildingSpriteRenderer.sprite = sprites;

        //            }
        //        }
        //    }
        //    else if (other.CompareTag("Building3"))
        //    {
        //        buildingSpriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
        //        buildingSprites = other.GetComponentInChildren<BuildingSprites>();
        //        sprites = buildingSprites.buildSprites; // sprite ����

        //        id = 2;
        //        // 1���� 2���� ������ �����̰�
        //        if (recipeList.recipes[id - 1].builded && recipeList.recipes[id - 2].builded)
        //        {
        //            // 3���� �������� ���� ���¶��
        //            if (!recipeList.recipes[id].builded)
        //            {
        //                recipeList.recipes[id].builded = true;
        //                Debug.Log("it's Building3");
        //                building.Rebuild(recipeList.recipes[id]);

        //                // �ǹ��� �������ٸ� sprite ����
        //                if (recipeList.recipes[id].builded == true)
        //                {
        //                    buildingSpriteRenderer.sprite = sprites;

        //                }
        //            }
        //        }
        //    }

    }

    private void ProcessBuilding(Collider other)
    {
        // �ǹ� �±׷� ID ����
        Dictionary<string, int> buildingIds = new Dictionary<string, int>
    {
        { "Building1", 0 },
        { "Building2", 1 },
        { "Building3", 2 },
        { "Building4", 3 },
        { "Building5", 4 }
    };

        if (buildingIds.TryGetValue(other.tag, out int id))
        {
            buildingSpriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
            buildingSprites = other.GetComponentInChildren<BuildingSprites>();

            if (buildingSprites == null || buildingSpriteRenderer == null)
            {
                Debug.LogError("BuildingSprites or SpriteRenderer is missing on: " + other.name);
                return;
            }

            sprites = buildingSprites.buildSprites; // sprite ����

            // ���� ó��
            if (id == 2)
            {
                // 3�� �ǹ��� ���� 1, 2���� ������ ���¿��� ��
                if (!recipeList.recipes[id - 1].builded || !recipeList.recipes[id - 2].builded)
                    return;
            }

            if (!recipeList.recipes[id].builded)
            {
                Debug.Log($"Attempting to build {other.tag}");
                bool built = building.Rebuild(recipeList.recipes[id]); // ���� ���� Ȯ��
                if (built) // ��� ���� ��
                {
                    recipeList.recipes[id].builded = true;
                    buildingSpriteRenderer.sprite = sprites; // ��������Ʈ ����
                    if (id > 1)
                    {
                        //lightManager.vignette.intensity.value -= 0.15f;
                        camSc.mapCnt++;
                        if (id != 3)
                        {
                            camSc.UpdateProfile();
                        }
                        Debug.Log("����� ���� ���������" + lightManager.vignette.intensity.value);
                    }
                    Debug.Log($"{other.tag} successfully built!");
                }
                else
                {
                    Debug.Log($"{other.tag} cannot be built due to insufficient materials.");
                }
            }
        }
    }

    private void InitializeRecipes()
    {
        if (recipeList == null || recipeList.recipes == null)
        {
            Debug.LogError("RecipeList or recipes is not assigned.");
            return;
        }

        foreach (var recipe in recipeList.recipes)
        {
            recipe.builded = false; // ��� �ǹ� ���� �ʱ�ȭ
        }

        Debug.Log("RecipeList initialized.");
    }

    IEnumerator DestroyObject(Collider other)
    {
        yield return new WaitForSeconds(1.3f);

        if(trstspawner != null)
        {
            trstspawner.objectList.Remove(other.gameObject);
            //Destroy();
        }
        if(stspawner != null)
        {
            stspawner.stoneList.Remove(other.gameObject);

        }
        other.GetComponent<DroppingItem>().Hit(); // DroppingItem Ŭ������ Hit() �޼��带 ȣ��
    }
}
