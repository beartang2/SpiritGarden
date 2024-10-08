using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrowingPlant : MonoBehaviour
{
    [SerializeField] TileBase watered;
    [SerializeField] TileBase dried;
    [SerializeField] private Sprite[] growthSprites; // 각 성장 단계별 스프라이트 배열
    [SerializeField] private GameObject finalPlantPrefab; // 최종적으로 변할 식물 오브젝트
    [SerializeField] private int totalGrowthDays = 3; // 전체 성장에 걸리는 일 수

    private SpriteRenderer spriteRenderer; // 현재 오브젝트의 스프라이트 렌더러
    private int currentGrowthDay = 0; // 현재 성장한 일 수
    private LightingManager timeManager; // 시간을 관리하는 스크립트 참조 (외부에서 연결됨)

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 참조
        timeManager = FindObjectOfType<LightingManager>(); // 시간 관리 스크립트 찾기 (TimeManager가 있다고 가정)
        timeManager.OnDayPassed += Grow; // 하루가 지날 때마다 Grow() 함수 실행

        // 초기 스프라이트 설정
        spriteRenderer.sprite = growthSprites[0];
    }

    // 하루가 지날 때 실행될 함수
    private void Grow()
    {
        currentGrowthDay++;

        if (currentGrowthDay < totalGrowthDays) // 아직 최종 단계가 아닌 경우
        {
            // 다음 단계의 스프라이트로 변경
            spriteRenderer.sprite = growthSprites[currentGrowthDay];
        }
        else if (currentGrowthDay == totalGrowthDays) // 최종 단계에 도달하면
        {
            // 최종 오브젝트로 변환
            TransformToFinalPlant();
        }
    }

    // 최종 식물 오브젝트로 변환하는 함수
    private void TransformToFinalPlant()
    {
        // 현재 오브젝트 제거하고 최종 식물 오브젝트를 생성
        Instantiate(finalPlantPrefab, transform.position, Quaternion.Euler(0, 45, 0));
        Destroy(gameObject); // 현재 오브젝트는 파괴
    }

    private void OnDestroy()
    {
        // 오브젝트가 파괴될 때 이벤트 구독 해제
        timeManager.OnDayPassed -= Grow;
    }
}
