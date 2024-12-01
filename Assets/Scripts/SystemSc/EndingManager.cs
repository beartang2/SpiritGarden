using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    // ScriptableObject 배열로 모든 건물 데이터 관리
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
            // 엔딩 호출
            Debug.Log("모든 건물이 재건되었습니다. 엔딩 호출!");
        }
    }
}
