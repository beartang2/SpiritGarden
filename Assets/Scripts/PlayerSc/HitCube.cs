using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    [SerializeField] stoneSpawner stspawner; // �� ������ ��ũ��Ʈ
    [SerializeField] treeSpawner trspawner; // ���� ������ ��ũ��Ʈ
    [SerializeField] DroppingItem dropItemSc; // ������ ��� ��ũ��Ʈ
    [SerializeField] TileMapReadController tileReadCont; // Ÿ���� �д� ��ũ��Ʈ

    private Vector3Int tilePos;

    // �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� ��ũ��Ʈ�� �����´�
        DroppingItem droppingItem = other.GetComponent<DroppingItem>();
        //EnemyHP enemyHp = other.GetComponent<EnemyHP>();

        if (droppingItem != null) // ��������� ��ũ��Ʈ�� �����ϸ� -> ��ü
        {
            if(other.CompareTag("Plant") && tileReadCont != null)
            {
                tilePos = tileReadCont.GetGridPosition(gameObject.transform.position);
                dropItemSc.HarvestPlant(tilePos);
            }
            if(stspawner != null)
            {
                stspawner.stoneList.Remove(other.gameObject);
            }
            if(trspawner != null)
            {
                trspawner.objectList.Remove(other.gameObject);
            }
            Debug.Log("ť�갡 �浹��!");
            droppingItem.Hit(); // DroppingItem Ŭ������ Hit() �޼��带 ȣ��
        }
        /*
         * else if(enemyHp != null) // ü�� ��ũ��Ʈ�� �����ϸ� -> ����
         * {
         *    ������ ������ ���� �ٸ� �������� ����ǵ��� ��
         *    -> ���� ������ �������� toolbar���� ����
         *       ������ �����ؼ� ü�� ���
         *    
         *    + ���� ���Ϳ� �浹�� ����� ü���� 0�� ��쿡�� �������� ����ؾ���
         *    -> ���߿� ���� ü�� ��ũ��Ʈ �ۼ� ��
         *       ü���� 0�� �Ǹ� droppingItem ��ũ��Ʈ�� Hit ȣ��
         *
         *    droppingItem.Hit(); // DroppingItem Ŭ������ Hit() �޼��带 ȣ��
         * }
         */
    }
}
