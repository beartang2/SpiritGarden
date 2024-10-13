using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    [SerializeField] objectSpawner spawner; // 스포너 스크립트

    // 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체의 스크립트를 가져온다
        DroppingItem droppingItem = other.GetComponent<DroppingItem>();
        //EnemyHP enemyHp = other.GetComponent<EnemyHP>();

        if (droppingItem != null) // 드랍아이템 스크립트가 존재하면 -> 물체
        {
            spawner.stoneList.Remove(other.gameObject);
            Debug.Log("큐브가 충돌함!");
            droppingItem.Hit(); // DroppingItem 클래스의 Hit() 메서드를 호출
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
