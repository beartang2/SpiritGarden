using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerToolController : MonoBehaviour
{
    PlayerMovement player;
    Rigidbody rb;
    //[SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileReadCont;
    [SerializeField] Transform interactBox;

    Vector3Int selectedTilePos;
    bool canSelect;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SelectTile();
        if(Input.GetMouseButton(0))
        {
            UseTool();
        }
    }

    private void SelectTile()
    {
        selectedTilePos = tileReadCont.GetGridPosition(interactBox.position, true);
    }

    private void UseTool()
    {
        
    }
}
