using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GrowingPlant : MonoBehaviour
{
    [SerializeField] TileBase watered;
    [SerializeField] TileBase dried;
    [SerializeField] private Sprite[] growthSprites; // �� ���� �ܰ躰 ��������Ʈ �迭
    [SerializeField] private GameObject finalPlantPrefab; // ���������� ���� �Ĺ� ������Ʈ
    [SerializeField] private int totalGrowthDays = 3; // ��ü ���忡 �ɸ��� �� ��

    private SpriteRenderer spriteRenderer; // ���� ������Ʈ�� ��������Ʈ ������
    private int currentGrowthDay = 0; // ���� ������ �� ��
    private LightingManager timeManager; // �ð��� �����ϴ� ��ũ��Ʈ ���� (�ܺο��� �����)

    Vector3 cropPos;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��������Ʈ ������ ����
        timeManager = FindObjectOfType<LightingManager>(); // �ð� ���� ��ũ��Ʈ ã�� (TimeManager�� �ִٰ� ����)
        timeManager.OnDayPassed += Grow; // �Ϸ簡 ���� ������ Grow() �Լ� ����

        // �ʱ� ��������Ʈ ����
        spriteRenderer.sprite = growthSprites[0];
    }

    // �Ϸ簡 ���� �� ����� �Լ�
    private void Grow()
    {
        currentGrowthDay++;

        if (currentGrowthDay < totalGrowthDays) // ���� ���� �ܰ谡 �ƴ� ���
        {
            // ���� �ܰ��� ��������Ʈ�� ����
            spriteRenderer.sprite = growthSprites[currentGrowthDay];
        }
        else if (currentGrowthDay == totalGrowthDays) // ���� �ܰ迡 �����ϸ�
        {
            // ���� ������Ʈ�� ��ȯ
            TransformToFinalPlant();
        }
    }

    // ���� �Ĺ� ������Ʈ�� ��ȯ�ϴ� �Լ�
    private void TransformToFinalPlant()
    {
        cropPos = transform.position;
        cropPos.y = 1f; // ���� �Ĺ� ���̿� ���� y�� ��ġ ����

        // ���� ������Ʈ �����ϰ� ���� �Ĺ� ������Ʈ�� ����
        Instantiate(finalPlantPrefab, cropPos, Quaternion.Euler(0, 45, 0));
        Destroy(gameObject); // ���� ������Ʈ�� �ı�
    }

    private void OnDestroy()
    {
        // ������Ʈ�� �ı��� �� �̺�Ʈ ���� ����
        timeManager.OnDayPassed -= Grow;
    }
}
