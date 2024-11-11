using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    [SerializeField] stoneSpawner stspawner; // 돌 스포너 스크립트
    [SerializeField] treeSpawner trspawner; // 나무 스포너 스크립트
    [SerializeField] DroppingItem dropItemSc; // 아이템 드랍 스크립트
    [SerializeField] TileMapReadController tileReadCont; // 타일을 읽는 스크립트
    [SerializeField] Rebuilding building; //건축 스크립트
    [SerializeField] RecipeList recipeList; // 레시피 리스트

    private Vector3Int tilePos;

    [SerializeField] ToolBarController toolbarCont;

    private int id = -1; // 건축물 번호
    private bool builded1 = false; // 1번건축물
    private bool builded2 = false; // 2번 건축물
    private bool builded3 = false; // 정령

    // 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        // 도구 아이템 정보 가져오기
        Item item = toolbarCont.GetItem;

        // 충돌한 물체의 스크립트를 가져온다
        DroppingItem droppingItem = other.GetComponent<DroppingItem>();
        EnemyHp enemyHp = other.GetComponent<EnemyHp>();
        SpriteRenderer buildingSprite = other.GetComponent<SpriteRenderer>();

        if (enemyHp != null) // 에너미 체력 스크립트가 존재하면 -> 에너미
        {
            if (other.CompareTag("Enemy") && item.Name == "Sword")
            {
                enemyHp.TakeDamage(10);
                Debug.Log("'검'으로 적을 타격하였다! 10 데미지");
            }
            if (other.CompareTag("Enemy") && item.Name == "Pickaxe")
            {
                enemyHp.TakeDamage(8);
                Debug.Log("'곡괭이'로 적을 타격하였다! 8 데미지");
            }
            if (other.CompareTag("Enemy") && item.Name == "Sickle")
            {
                enemyHp.TakeDamage(4);
                Debug.Log("'낫'으로 적을 타격하였다! 4 데미지");
            }
        }
        else if (droppingItem != null) // 드랍아이템 스크립트가 존재하면 -> 물체
        {
            if(other.CompareTag("Plant") && tileReadCont != null)
            {
                tilePos = tileReadCont.GetGridPosition(gameObject.transform.position);
                dropItemSc.HarvestPlant(tilePos);
            }
            if(other.CompareTag("Stone") && item.Name == "Pickaxe" && stspawner != null)
            {
                stspawner.stoneList.Remove(other.gameObject);
                droppingItem.Hit(); // DroppingItem 클래스의 Hit() 메서드를 호출
            }
            if(other.CompareTag("Tree") && item.Name == "Pickaxe" && trspawner != null)
            {
                trspawner.objectList.Remove(other.gameObject);
                droppingItem.Hit(); // DroppingItem 클래스의 Hit() 메서드를 호출
            }
            Debug.Log("큐브가 충돌함!");
        }
        else // 드랍아이템 스크립트가 존재하지 않으면 -> 건물 오브젝트
        {
            // 건물 레시피 아이디 부여
            if (other.CompareTag("Building1") && !builded1)
            {
                id = 0;
                builded1 = true;
                Debug.Log("it's Building1");
                building.Rebuild(recipeList.recipes[id]);
            }

            if (other.CompareTag("Building2") && !builded2)
            {
                id = 1;
                builded2 = true;
                Debug.Log("it's Building2");
                building.Rebuild(recipeList.recipes[id]);
            }

            if (other.CompareTag("Building3") && !builded3)
            {
                id = 2;
                builded3 = true;
                Debug.Log("it's Building3");
                building.Rebuild(recipeList.recipes[id]);
            }
        }
    }
}
