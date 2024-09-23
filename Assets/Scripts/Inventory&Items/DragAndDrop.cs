using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] Transform player;   // 플레이어 캐릭터의 Transform
    [SerializeField] ItemSlot itemSlot;  // 아이템 슬롯 (드래그 중인 아이템 정보)
    [SerializeField] GameObject itemIcon;// 드래그 중인 아이템을 표시할 아이콘
    RectTransform iconTrans;             // 아이콘의 RectTransform
    Image itemImage;                     // 아이콘에 표시할 이미지

    private void Start()
    {
        itemSlot = new ItemSlot();                         // 새로운 아이템 슬롯 생성
        iconTrans = itemIcon.GetComponent<RectTransform>(); // 아이콘의 RectTransform 컴포넌트 가져오기
        itemImage = itemIcon.GetComponent<Image>();        // 아이콘의 이미지 컴포넌트 가져오기
    }

    private void Update()
    {
        if (itemIcon.activeInHierarchy == true) // 아이템 아이콘이 활성화되어 있는지 확인
        {
            iconTrans.position = Input.mousePosition; // 아이콘을 마우스 위치로 이동시킴

            if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭을 감지
            {
                if (EventSystem.current.IsPointerOverGameObject() == false) // UI 요소 위가 아닌지 확인
                {
                    // 마우스 클릭 위치를 레이로 변환하여 월드 좌표로 계산
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity)) // 클릭한 지점이 존재할 경우
                    {
                        // 플레이어와 클릭한 지점 사이의 방향을 계산하고, 일정 거리에 아이템을 생성
                        Vector3 direction = (hit.point - player.position).normalized;
                        Vector3 spawnPosition = player.position + direction * 2f; // 캐릭터로부터 2단위 떨어진 지점
                        spawnPosition.y = 0.5f; // y축 고정

                        // 아이템 생성
                        ItemSpawner.instance.SpawnItem(
                            spawnPosition,   // 아이템이 생성될 위치
                            itemSlot.item,   // 드래그 중인 아이템 정보
                            itemSlot.count   // 아이템 개수
                        );

                        // 드래그 중인 아이템 정보 초기화
                        itemSlot.Clear();
                        itemIcon.SetActive(false); // 아이콘 비활성화
                    }
                }
            }
        }
    }

    // 아이템 슬롯 클릭 시 처리
    internal void OnClick(ItemSlot itemSlot)
    {
        // 현재 드래그 중인 아이템이 없으면 클릭한 아이템 슬롯 정보를 가져옴
        if (this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);  // 클릭한 아이템 슬롯의 정보를 복사
            itemSlot.Clear();              // 클릭한 슬롯 초기화
        }
        else // 드래그 중인 아이템이 있는 경우 슬롯 교체
        {
            Item item = itemSlot.item;     // 클릭한 슬롯의 아이템 정보를 저장
            int count = itemSlot.count;    // 클릭한 슬롯의 아이템 개수 저장

            itemSlot.Copy(this.itemSlot);  // 드래그 중인 아이템 정보를 클릭한 슬롯에 복사
            this.itemSlot.Set(item, count);// 클릭한 슬롯의 아이템 정보를 드래그 중인 슬롯에 설정
        }
        UpdateIcon();  // 아이콘 업데이트
    }

    // 아이콘을 업데이트하여 드래그 중인 아이템에 맞게 설정
    private void UpdateIcon()
    {
        if (itemSlot.item == null)  // 드래그 중인 아이템이 없을 경우
        {
            itemIcon.SetActive(false); // 아이콘을 비활성화
        }
        else // 드래그 중인 아이템이 있을 경우
        {
            itemIcon.SetActive(true);      // 아이콘을 활성화
            itemImage.sprite = itemSlot.item.icon; // 아이템의 아이콘을 설정
        }
    }
}
