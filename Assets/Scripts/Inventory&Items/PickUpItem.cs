using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player; // 플레이어의 위치를 추적할 변수
    [SerializeField] float speed = 6f; // 아이템이 플레이어에게 이동하는 속도
    [SerializeField] float pickDis = 1.5f; // 아이템이 플레이어에게 이동하기 시작하는 거리
    [SerializeField] float ttl = 10f; // 아이템의 Time To Live (생존 시간)
    AudioSource audioSc; // 오디오 소스
    public AudioClip audioClip;

    public Item item; // 아이템 정보를 담는 변수
    public int count = 1; // 아이템 개수

    BoxCollider itemCol; // 아이템 콜라이더

    private float dis; // 거리

    private void Start()
    {
        // GameManager에서 플레이어의 Transform을 가져옴
        player = GameManager.instance.player.transform;
        // BoxCollider 가져옴
        itemCol = gameObject.GetComponent<BoxCollider>();
        audioSc = GetComponent<AudioSource>();
    }

    // 아이템의 종류와 개수를 설정하는 메서드
    public void Set(Item item, int count)
    {
        this.item = item; // 아이템 종류 설정
        this.count = count; // 아이템 개수 설정

        // 아이템의 아이콘을 보여주기 위해 SpriteRenderer를 설정
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon; // 아이템의 아이콘을 스프라이트로 설정
    }

    private void Update()
    {
        ItemTTL();

        CheckDist();

        TakeItem();
    }

    private void ItemTTL()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            Destroy(gameObject);
        }
    }

    private void ItemMove()
    {
        // 플레이어에게 다가가기 위해 아이템의 위치를 이동
        transform.position = Vector3.MoveTowards(
            transform.position, // 현재 아이템의 위치
            player.position,    // 플레이어의 위치
            speed * Time.deltaTime // 이동 속도 조정
        );

        if(itemCol.enabled)
        {
            itemCol.enabled = false; // 플레이어와 부딪히지 않게 비활성화
            audioSc.PlayOneShot(audioClip); // 아이템 먹는 소리
        }
    }

    private void CheckDist()
    {
        // 현재 아이템과 플레이어 사이의 거리를 계산
        dis = Vector3.Distance(transform.position, player.position);

        // 만약 거리가 지정된 거리보다 크다면, 아이템은 플레이어에게 이동하지 않음
        if (dis > pickDis)
        {
            return; // 함수 종료
        }
        else
        {
            ItemMove();
        }
    }

    private void TakeItem()
    {
        // 플레이어에 매우 가까워지면 아이템을 제거
        if (dis < 0.3f)
        {
            // 인벤토리가 null이 아니면
            if (GameManager.instance.inventory != null)
            {
                // 인벤토리에 아이템 종류와 개수 추가
                GameManager.instance.inventory.Add(item, count);
            }
            else
            {
                Debug.LogWarning("No inventory container attached to the game manager");
            }

            Destroy(gameObject); // 아이템 제거
        }
    }
}
