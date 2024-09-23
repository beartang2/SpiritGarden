using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolController : MonoBehaviour
{
    PlayerMovement player;
    Rigidbody rb;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            UseTool();
        }
    }

    private void UseTool()
    {
        
    }
}
