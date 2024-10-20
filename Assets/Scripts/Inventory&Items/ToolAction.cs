using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : MonoBehaviour
{
    public virtual bool OnApply()
    {
        Debug.LogWarning("Apply is not implemented");
        return true;
    }
}
