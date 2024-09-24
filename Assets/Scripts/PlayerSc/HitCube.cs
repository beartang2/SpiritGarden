using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    // 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 물체가 CuttingTree 스크립트를 가진 경우
        CuttingTree cuttingTree = other.GetComponent<CuttingTree>();

        if (cuttingTree != null)
        {
            Debug.Log("큐브가 나무와 충돌함!");
            cuttingTree.Hit(); // CuttingTree 클래스의 Hit() 메서드를 호출
        }
    }

    // 콜라이더가 다른 물체와 충돌을 멈췄을 때 호출됨
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("충돌이 끝남!");
    }
}
