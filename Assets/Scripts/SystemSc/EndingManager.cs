using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    // ScriptableObject �迭�� ��� �ǹ� ������ ����
    public List<BuildRecipe> buildingList;
    private bool allBuilt = false;

    private void Update()
    {
        CheckBuilds();
    }

    private void CheckBuilds()
    {
        foreach (var build in buildingList)
        {
            if(!build.builded)
            {
                allBuilt = false;
                break;
            }
            allBuilt = true;
        }
        if(allBuilt)
        {
            // ���� ȣ��
            Debug.Log("��� �ǹ��� ��ǵǾ����ϴ�. ���� ȣ��!");
        }
    }
}
