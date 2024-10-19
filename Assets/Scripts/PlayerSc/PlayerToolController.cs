using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

public class PlayerToolController : MonoBehaviour
{
    PlayerMovement player; // 플레이어 움직임을 관리하는 스크립트
    Rigidbody rb; // 플레이어의 Rigidbody 컴포넌트 참조
    //[SerializeField] MarkerManager markerManager; // 마커 관리 스크립트 (사용하지 않음)
    [SerializeField] TileMapReadController tileReadCont; // 타일맵을 읽는 컨트롤러
    [SerializeField] Transform interactBox; // 상호작용 박스 위치
    [SerializeField] CropManager cropManager; // 작물 관리 스크립트
    [SerializeField] GameObject plantPrefab; // 식물 오브젝트

    public Vector3Int selectedTilePos; // 선택된 타일의 그리드 좌표
    TileBase currentTile; // 현재 타일맵에서 선택된 타일
    TileBase seedTile; // 씨앗 타일맵에서 선택된 타일
    public TileData currentTileData; // 현재 타일의 데이터 (seedable, waterable 등 속성 정보)

    public List<Vector3Int> plantedPositions = new List<Vector3Int> (); // 식물 심은 위치 정보 리스트

    private void Awake()
    {
        player = GetComponent<PlayerMovement>(); // 플레이어 움직임 스크립트 참조
        rb = GetComponent<Rigidbody>(); // 플레이어의 Rigidbody 참조
    }

    private void Update()
    {
        if(tileReadCont == null || cropManager == null)
        {
            return;
        }

        SelectTile(); // 선택된 타일의 좌표를 가져옴

        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭 시 상호작용
        {
            CanInteract(); // 상호작용 가능한 상태인지 확인
            UseToolGrid(); // 도구 사용 (씨앗 심기 또는 물주기)
        }
    }

    // 플레이어 상호작용 박스가 위치한 타일의 그리드 좌표를 가져옴
    private void SelectTile()
    {
        selectedTilePos = tileReadCont.GetGridPosition(interactBox.position); // 상호작용 박스 위치를 기준으로 타일 좌표 계산
    }

    // 현재 타일과 씨앗 타일맵에서 타일 정보를 가져와 상호작용 가능한지 확인
    private void CanInteract()
    {
        // 기본 타일맵에서 현재 타일 정보를 가져옴
        currentTile = tileReadCont.GetTileBase(selectedTilePos, tileReadCont.tileMap);

        // 현재 타일이 유효한 경우 타일 데이터를 가져옴
        if (currentTile != null)
        {
            currentTileData = tileReadCont.GetTileData(currentTile); // 타일 데이터(예: seedable, waterable) 가져오기
        }

        // 씨앗 타일맵에서 타일 정보를 가져옴
        seedTile = tileReadCont.GetTileBase(selectedTilePos, tileReadCont.seedTileMap);
    }

    // 도구를 사용하여 씨앗을 심거나 물을 줌
    private void UseToolGrid()
    {
        if(tileReadCont.tileMap == null)
        {
            return;
        }

        // 씨앗을 심을 수 있는 타일이고 씨앗이 심어지지 않은 타일이면 씨앗을 심음
        if (currentTileData.seedable == true && seedTile == null)
        {
            // 현재 선택한 타일에 이미 식물이 심어져 있는지 확인
            if (!plantedPositions.Contains(selectedTilePos))
            {
                // 씨앗 심기
                cropManager.Seed(selectedTilePos);
                plantedPositions.Add(selectedTilePos); // 심은 위치 정보 추가
                Debug.Log("씨앗을 심었다!");
            }
            else if(currentTile.name == "Land")
            {
                Debug.Log("이미 식물이 심어져 있습니다");
                cropManager.Watering(selectedTilePos);
            }
        }

        // 씨앗 타일이 존재하고
        if(seedTile != null)
        {
            // 물을 줄 수 있는 타일이면
            if (currentTileData.waterable == true)
            {
                cropManager.Watering(selectedTilePos); // 물주기
            }
        }
    }
}
