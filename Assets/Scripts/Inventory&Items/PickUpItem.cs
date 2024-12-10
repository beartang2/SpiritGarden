using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player; // �÷��̾��� ��ġ�� ������ ����
    [SerializeField] float speed = 6f; // �������� �÷��̾�� �̵��ϴ� �ӵ�
    [SerializeField] float pickDis = 1.5f; // �������� �÷��̾�� �̵��ϱ� �����ϴ� �Ÿ�
    [SerializeField] float ttl = 10f; // �������� Time To Live (���� �ð�)
    AudioSource audioSc; // ����� �ҽ�
    public AudioClip audioClip;

    public Item item; // ������ ������ ��� ����
    public int count = 1; // ������ ����

    BoxCollider itemCol; // ������ �ݶ��̴�

    private float dis; // �Ÿ�

    private void Start()
    {
        // GameManager���� �÷��̾��� Transform�� ������
        player = GameManager.instance.player.transform;
        // BoxCollider ������
        itemCol = gameObject.GetComponent<BoxCollider>();
        audioSc = GetComponent<AudioSource>();
    }

    // �������� ������ ������ �����ϴ� �޼���
    public void Set(Item item, int count)
    {
        this.item = item; // ������ ���� ����
        this.count = count; // ������ ���� ����

        // �������� �������� �����ֱ� ���� SpriteRenderer�� ����
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon; // �������� �������� ��������Ʈ�� ����
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
        // �÷��̾�� �ٰ����� ���� �������� ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(
            transform.position, // ���� �������� ��ġ
            player.position,    // �÷��̾��� ��ġ
            speed * Time.deltaTime // �̵� �ӵ� ����
        );

        if(itemCol.enabled)
        {
            itemCol.enabled = false; // �÷��̾�� �ε����� �ʰ� ��Ȱ��ȭ
            audioSc.PlayOneShot(audioClip); // ������ �Դ� �Ҹ�
        }
    }

    private void CheckDist()
    {
        // ���� �����۰� �÷��̾� ������ �Ÿ��� ���
        dis = Vector3.Distance(transform.position, player.position);

        // ���� �Ÿ��� ������ �Ÿ����� ũ�ٸ�, �������� �÷��̾�� �̵����� ����
        if (dis > pickDis)
        {
            return; // �Լ� ����
        }
        else
        {
            ItemMove();
        }
    }

    private void TakeItem()
    {
        // �÷��̾ �ſ� ��������� �������� ����
        if (dis < 0.3f)
        {
            // �κ��丮�� null�� �ƴϸ�
            if (GameManager.instance.inventory != null)
            {
                // �κ��丮�� ������ ������ ���� �߰�
                GameManager.instance.inventory.Add(item, count);
            }
            else
            {
                Debug.LogWarning("No inventory container attached to the game manager");
            }

            Destroy(gameObject); // ������ ����
        }
    }
}
