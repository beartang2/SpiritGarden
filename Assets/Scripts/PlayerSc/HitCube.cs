using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    [SerializeField] stoneSpawner stspawner; // �� ������ ��ũ��Ʈ
    [SerializeField] treeSpawner trspawner; // ���� ������ ��ũ��Ʈ
    [SerializeField] DroppingItem dropItemSc; // ������ ��� ��ũ��Ʈ
    [SerializeField] TileMapReadController tileReadCont; // Ÿ���� �д� ��ũ��Ʈ
    [SerializeField] Rebuilding building; //���� ��ũ��Ʈ
    [SerializeField] RecipeList recipeList; // ������ ����Ʈ

    private Vector3Int tilePos;

    [SerializeField] ToolBarController toolbarCont;

    private BuildingSprites buildingSprites;
    private SpriteRenderer spriteRenderer;
    private Sprite[] sprites; // sprite �迭

    private int id = -1; // ���๰ ��ȣ �ʱ�ȭ

    // �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        // ���� ������ ���� ��������
        Item item = toolbarCont.GetItem;

        // �浹�� ��ü�� ��ũ��Ʈ�� �����´�
        DroppingItem droppingItem = other.GetComponent<DroppingItem>();
        EnemyHp enemyHp = other.GetComponent<EnemyHp>();
        SpriteRenderer buildingSprite = other.GetComponentInChildren<SpriteRenderer>();
        BuildingSprites buildingSprites = other.GetComponentInChildren<BuildingSprites>();
        sprites = buildingSprites.buildSprites; // sprite ����

        if (enemyHp != null) // ���ʹ� ü�� ��ũ��Ʈ�� �����ϸ� -> ���ʹ�
        {
            if (other.CompareTag("Enemy") && item.Name == "Sword")
            {
                enemyHp.TakeDamage(10);
                Debug.Log("'��'���� ���� Ÿ���Ͽ���! 10 ������");
            }
            if (other.CompareTag("Enemy") && item.Name == "Pickaxe")
            {
                enemyHp.TakeDamage(8);
                Debug.Log("'���'�� ���� Ÿ���Ͽ���! 8 ������");
            }
            if (other.CompareTag("Enemy") && item.Name == "Sickle")
            {
                enemyHp.TakeDamage(4);
                Debug.Log("'��'���� ���� Ÿ���Ͽ���! 4 ������");
            }
        }
        else if (droppingItem != null) // ��������� ��ũ��Ʈ�� �����ϸ� -> ��ü
        {
            if(other.CompareTag("Plant") && tileReadCont != null)
            {
                tilePos = tileReadCont.GetGridPosition(gameObject.transform.position);
                dropItemSc.HarvestPlant(tilePos);
            }
            if(other.CompareTag("Stone") && item.Name == "Pickaxe" && stspawner != null)
            {
                stspawner.stoneList.Remove(other.gameObject);
                droppingItem.Hit(); // DroppingItem Ŭ������ Hit() �޼��带 ȣ��
            }
            if(other.CompareTag("Tree") && item.Name == "Pickaxe" && trspawner != null)
            {
                trspawner.objectList.Remove(other.gameObject);
                droppingItem.Hit(); // DroppingItem Ŭ������ Hit() �޼��带 ȣ��
            }
            Debug.Log("ť�갡 �浹��!");
        }
        else // ��������� ��ũ��Ʈ�� �������� ������ -> �ǹ� ������Ʈ
        {
            // �ǹ� ������ ���̵� �ο�
            if (other.CompareTag("Building1"))
            {
                id = 0;
                if(!recipeList.recipes[id].builded)
                {
                    recipeList.recipes[id].builded = true;
                    Debug.Log("it's Building1");
                    building.Rebuild(recipeList.recipes[id]);

                    // �ǹ��� �������ٸ� sprite ����
                    if (recipeList.recipes[id].builded == true)
                    {
                        buildingSprite.sprite = sprites[1];
                    }
                }
            }
            else if (other.CompareTag("Building2"))
            {
                id = 1;
                if (!recipeList.recipes[id].builded)
                {
                    recipeList.recipes[id].builded = true;
                    Debug.Log("it's Building2");
                    building.Rebuild(recipeList.recipes[id]);

                    // �ǹ��� �������ٸ� sprite ����
                    if (recipeList.recipes[id].builded == true)
                    {
                        buildingSprite.sprite = sprites[1];
                    }
                }
            }
            else if (other.CompareTag("Building3"))
            {
                id = 2;
                // 1���� 2���� ������ �����̰�
                if(recipeList.recipes[id - 1].builded && recipeList.recipes[id - 2].builded)
                {
                    // 3���� �������� ���� ���¶��
                    if (!recipeList.recipes[id].builded)
                    {
                        recipeList.recipes[id].builded = true;
                        Debug.Log("it's Building3");
                        building.Rebuild(recipeList.recipes[id]);
                    }
                }
            }
            sprites = null; // �迭 �ʱ�ȭ
        }
    }
}
