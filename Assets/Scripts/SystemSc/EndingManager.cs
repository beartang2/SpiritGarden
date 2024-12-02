using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    // ScriptableObject 배열로 모든 건물 데이터 관리
    public List<BuildRecipe> buildingList;
    [SerializeField] FadeEffect fadeEffect;
    [SerializeField] Transform player;
    [SerializeField] GameObject[] spirits; // 정령 오브젝트들
    [SerializeField] Transform[] npcPositions; // 이동시킬 위치 좌표
    private Rigidbody rb;
    private PlayerMovement movementSc;
    private Animator anim;
    private bool allBuilt = false;
    Vector3 startPos;
    private bool isEnding = false;

    private void Start()
    {
        startPos = player.transform.position;
        rb = player.transform.GetComponent<Rigidbody>();
        movementSc = player.transform.GetComponent<PlayerMovement>();
        anim = player.transform.GetComponent<Animator>();
    }

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
        if(allBuilt && !isEnding)
        {
            isEnding = true;
            // 엔딩 호출
            Debug.Log("모든 건물이 재건되었습니다. 엔딩 호출!");
            EndingGame();
        }
    }

    private void EndingGame()
    {
        movementSc.enabled = false; // 플레이어 이동 멈춤
        rb.isKinematic = false; // 물리 연산 멈춤
        anim.SetFloat("MoveX", 0); // 애니메이션 멈춤
        anim.SetFloat("MoveY", 0);

        // 엔딩 연출
        fadeEffect.StartFadeOut(); // 페이드 아웃
    }
}
