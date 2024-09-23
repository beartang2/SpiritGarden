using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarPanel : ItemPanel
{
    [SerializeField] ToolBarController toolBarController;

    private void Start()
    {
        Init();
        toolBarController.onChange += Highlight;
        Highlight(0);
    }

    public override void OnClick(int id)
    {
        toolBarController.Set(id);
        Highlight(id);
    }

    int currentSelectTool;

    public void Highlight(int id)
    {
        buttons[currentSelectTool].Highlight(false);
        currentSelectTool = id;
        buttons[currentSelectTool].Highlight(true);
    }
}
