using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] int barSize = 12;
    int selectTool;

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;

        if (delta != 0)
        {
            if (delta > 0)
            {
                selectTool += 1;
                selectTool = (selectTool >= barSize ? 0 : selectTool);
            }
            else
            {
                selectTool -= 1;
                selectTool = (selectTool <= 0 ? barSize - 1 : selectTool);
            }
            Debug.Log(selectTool);
        }
    }

    internal void Set(int id)
    {
        selectTool = id;
    }
}
