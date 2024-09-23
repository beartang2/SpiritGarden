using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject toolberPanel;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            panel.SetActive(!panel.activeInHierarchy);
            toolberPanel.SetActive(!toolberPanel.activeInHierarchy);
        }
    }
}
