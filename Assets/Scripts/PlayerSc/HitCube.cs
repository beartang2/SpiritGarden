using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    [SerializeField] stoneSpawner stspawner; // 돌 스포너 스크립트
    [SerializeField] treeSpawner trspawner; // 나무 스포너 스크립트
    [SerializeField] DroppingItem dropItemSc; // 아이템 드랍 스크립트
    [SerializeField] TileMapReadController tileReadCont; // 타일을 읽는 스크립트

    private Vector3Int tilePos;

    [SerializeField] ToolBarController toolbarCont;

    // 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        // 도구 아이템 정보 가져오기
        Item item = toolbarCont.GetItem;

        // 충돌한 물체의 스크립트를 가져온다
        DroppingItem droppingItem = other.GetComponent<DroppingItem>();
        //EnemyHP enemyHp = other.GetComponent<EnemyHP>();

        if (droppingItem != null) // 드랍아이템 스크립트가 존재하면 -> 물체
        {
            if(other.CompareTag("Plant") && tileReadCont != null)
            {
                tilePos = tileReadCont.GetGridPosition(gameObject.transform.position);
                dropItemSc.HarvestPlant(tilePos);
            }
            if(item.Name == "Pickaxe" && other.CompareTag("Stone") && stspawner != null)
            {
                stspawner.stoneList.Remove(other.gameObject);
                droppingItem.Hit(); // DroppingItem 클래스의 Hit() 메서드를 호출
            }
            if(item.Name == "Pickaxe" && other.CompareTag("Tree") && trspawner != null)
            {
                trspawner.objectList.Remove(other.gameObject);
                droppingItem.Hit(); // DroppingItem 클래스의 Hit() 메서드를 호출
            }
            Debug.Log("큐브가 충돌함!");
        }
        /*
         * else if(enemyHp != null) // 체력 스크립트가 존재하면 -> 몬스터
         * {
         *    도구의 종류에 따라 다른 데미지가 적용되도록 함
         *    -> 현재 도구가 무엇인지 toolbar에서 정보
         *       데미지 적용해서 체력 깎기
         *    
         *    + 만약 몬스터와 충돌한 경우라면 체력이 0인 경우에만 아이템을 드랍해야함
         *    -> 나중에 몬스터 체력 스크립트 작성 시
         *       체력이 0이 되면 droppingItem 스크립트의 Hit 호출
         *
         *    droppingItem.Hit(); // DroppingItem 클래스의 Hit() 메서드를 호출
         * }
         */
    }
}
