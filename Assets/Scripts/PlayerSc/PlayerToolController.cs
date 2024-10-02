using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

public class PlayerToolController : MonoBehaviour
{
    PlayerMovement player; // �÷��̾� �������� �����ϴ� ��ũ��Ʈ
    Rigidbody rb; // �÷��̾��� Rigidbody ������Ʈ ����
    //[SerializeField] MarkerManager markerManager; // ��Ŀ ���� ��ũ��Ʈ (������� ����)
    [SerializeField] TileMapReadController tileReadCont; // Ÿ�ϸ��� �д� ��Ʈ�ѷ�
    [SerializeField] Transform interactBox; // ��ȣ�ۿ� �ڽ� ��ġ
    [SerializeField] CropManager cropManager; // �۹� ���� ��ũ��Ʈ

    Vector3Int selectedTilePos; // ���õ� Ÿ���� �׸��� ��ǥ
    TileBase currentTile; // ���� Ÿ�ϸʿ��� ���õ� Ÿ��
    TileBase seedTile; // ���� Ÿ�ϸʿ��� ���õ� Ÿ��
    TileData currentTileData; // ���� Ÿ���� ������ (seedable, waterable �� �Ӽ� ����)

    private void Awake()
    {
        player = GetComponent<PlayerMovement>(); // �÷��̾� ������ ��ũ��Ʈ ����
        rb = GetComponent<Rigidbody>(); // �÷��̾��� Rigidbody ����
    }

    private void Update()
    {
        SelectTile(); // ���õ� Ÿ���� ��ǥ�� ������

        if (Input.GetMouseButtonDown(0)) // ���콺 ��Ŭ�� �� ��ȣ�ۿ�
        {
            CanInteract(); // ��ȣ�ۿ� ������ �������� Ȯ��
            UseToolGrid(); // ���� ��� (���� �ɱ� �Ǵ� ���ֱ�)
        }
    }

    // �÷��̾� ��ȣ�ۿ� �ڽ��� ��ġ�� Ÿ���� �׸��� ��ǥ�� ������
    private void SelectTile()
    {
        selectedTilePos = tileReadCont.GetGridPosition(interactBox.position); // ��ȣ�ۿ� �ڽ� ��ġ�� �������� Ÿ�� ��ǥ ���
    }

    // ���� Ÿ�ϰ� ���� Ÿ�ϸʿ��� Ÿ�� ������ ������ ��ȣ�ۿ� �������� Ȯ��
    private void CanInteract()
    {
        // �⺻ Ÿ�ϸʿ��� ���� Ÿ�� ������ ������
        currentTile = tileReadCont.GetTileBase(selectedTilePos, tileReadCont.tileMap);

        // ���� Ÿ���� ��ȿ�� ��� Ÿ�� �����͸� ������
        if (currentTile != null)
        {
            currentTileData = tileReadCont.GetTileData(currentTile); // Ÿ�� ������(��: seedable, waterable) ��������
        }

        // ���� Ÿ�ϸʿ��� Ÿ�� ������ ������
        seedTile = tileReadCont.GetTileBase(selectedTilePos, tileReadCont.seedTileMap);
    }

    // ������ ����Ͽ� ������ �ɰų� ���� ��
    private void UseToolGrid()
    {
        // ������ ���� �� �ִ� Ÿ���̸� ������ ����
        if (currentTileData.seedable == true)
        {
            cropManager.Seed(selectedTilePos); // ���� �ɱ�
        }

        // ���� �� �� �ִ� Ÿ���̸鼭 ���� Ÿ���� �����ϸ� ���ֱ�
        if (currentTileData.waterable == true && seedTile != null)
        {
            cropManager.Watering(selectedTilePos); // ���ֱ�
        }
    }
}
