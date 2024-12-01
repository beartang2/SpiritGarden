using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    [SerializeField] stoneSpawner stspawner; // 돌 스포너 스크립트
    [SerializeField] treeSpawner trstspawner; // 나무&돌 스포너 스크립트
    [SerializeField] DroppingItem dropItemSc; // 아이템 드랍 스크립트
    [SerializeField] TileMapReadController tileReadCont; // 타일을 읽는 스크립트
    [SerializeField] Rebuilding building; //건축 스크립트
    [SerializeField] RecipeList recipeList; // 레시피 리스트
    [SerializeField] LightingManager lightManager; // 라이팅 매니저 스크립트
    LightingPreset preset;

    private Vector3Int tilePos;

    [SerializeField] ToolBarController toolbarCont;

    private BuildingSprites buildingSprites;
    private SpriteRenderer buildingSpriteRenderer;
    private Sprite sprites; // sprite 배열

    //private int id = -1; // 건축물 번호 초기화

    private void Start()
    {
        // builded bool 변수 초기화
        InitializeRecipes();
    }

    // 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        // 도구 아이템 정보 가져오기
        Item item = toolbarCont.GetItem;

        // 충돌한 물체의 스크립트를 가져온다
        DroppingItem droppingItem = other.GetComponent<DroppingItem>();
        EnemyHp enemyHp = other.GetComponent<EnemyHp>();

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
            if (other.CompareTag("Enemy") && item.Name == null)
            {
                enemyHp.TakeDamage(2);
                Debug.Log("'손'으로 적을 타격하였다! 2 데미지");
            }
        }
        else if (droppingItem != null) // 드랍아이템 스크립트가 존재하면 -> 물체
        {
            if (other.CompareTag("Plant") && tileReadCont != null)
            {
                if(item.Name == "Sickle")
                {
                Debug.Log("풀");
                    tilePos = tileReadCont.GetGridPosition(gameObject.transform.position);
                    dropItemSc.HarvestPlant(tilePos);
                    droppingItem.Hit();
                }
            }
            if (other.CompareTag("Stone") && item.Name == "Pickaxe")
            {
                if (trstspawner != null)
                {
                    trstspawner.objectList.Remove(other.gameObject);
                }
                if(stspawner != null)
                {
                    stspawner.stoneList.Remove(other.gameObject);
                }
                droppingItem.Hit(); // DroppingItem 클래스의 Hit() 메서드를 호출
            }
            if (other.CompareTag("Tree") && item.Name == "Pickaxe" && trstspawner != null)
            {
                trstspawner.objectList.Remove(other.gameObject);
                droppingItem.Hit(); // DroppingItem 클래스의 Hit() 메서드를 호출
            }
            Debug.Log("큐브가 충돌함!");
        }
        else // 드랍아이템 스크립트가 존재하지 않으면 -> 건물 오브젝트
        {
            // 건물 처리
            ProcessBuilding(other);
        }
        //else // 드랍아이템 스크립트가 존재하지 않으면 -> 건물 오브젝트
        //{
        //    // 건물 레시피 아이디 부여
        //    if (other.CompareTag("Building1"))
        //    {
        //        buildingSpriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
        //        buildingSprites = other.GetComponentInChildren<BuildingSprites>();
        //        sprites = buildingSprites.buildSprites; // sprite 참조

        //        id = 0;
        //        if (!recipeList.recipes[id].builded)
        //        {
        //            recipeList.recipes[id].builded = true;
        //            Debug.Log("it's Building1");
        //            building.Rebuild(recipeList.recipes[id]);

        //            // 건물이 지어졌다면 sprite 변경
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
        //        sprites = buildingSprites.buildSprites; // sprite 참조

        //        id = 1;
        //        if (!recipeList.recipes[id].builded)
        //        {
        //            recipeList.recipes[id].builded = true;
        //            Debug.Log("it's Building2");
        //            building.Rebuild(recipeList.recipes[id]);

        //            // 건물이 지어졌다면 sprite 변경
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
        //        sprites = buildingSprites.buildSprites; // sprite 참조

        //        id = 2;
        //        // 1번과 2번이 지어진 상태이고
        //        if (recipeList.recipes[id - 1].builded && recipeList.recipes[id - 2].builded)
        //        {
        //            // 3번이 지어지지 않은 상태라면
        //            if (!recipeList.recipes[id].builded)
        //            {
        //                recipeList.recipes[id].builded = true;
        //                Debug.Log("it's Building3");
        //                building.Rebuild(recipeList.recipes[id]);

        //                // 건물이 지어졌다면 sprite 변경
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
        // 건물 태그로 ID 매핑
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

            sprites = buildingSprites.buildSprites; // sprite 참조

            // 조건 처리
            if (id == 2)
            {
                // 3번 건물은 이전 1, 2번이 지어진 상태여야 함
                if (!recipeList.recipes[id - 1].builded || !recipeList.recipes[id - 2].builded)
                    return;
            }

            if (!recipeList.recipes[id].builded)
            {
                Debug.Log($"Attempting to build {other.tag}");
                bool built = building.Rebuild(recipeList.recipes[id]); // 성공 여부 확인
                if (built) // 재건 성공 시
                {
                    recipeList.recipes[id].builded = true;
                    buildingSpriteRenderer.sprite = sprites; // 스프라이트 변경
                    if(id > 1)
                    {
                        Debug.Log("어둠이 점차 사라져간다");
                        lightManager.vignette.intensity.value -= 0.15f;
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
            recipe.builded = false; // 모든 건물 상태 초기화
        }

        Debug.Log("RecipeList initialized.");
    }
}
