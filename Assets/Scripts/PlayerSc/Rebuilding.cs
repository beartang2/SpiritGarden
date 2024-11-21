using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebuilding : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;

    public bool Rebuild(BuildRecipe recipe)
    {
        bool buildable = true;

        for(int i=0; i<recipe.elements.Count; i++)
        {
            if (inventory.CheckItem(recipe.elements[i]) == false)
            {
                Debug.Log("��ᰡ �����մϴ�");
                buildable = false;
                break;
            }
        }

        if (!buildable)
        {
            return false; // ��� ����  
        }

        for (int i=0; i<recipe.elements.Count; i++)
        {
            inventory.Remove(recipe.elements[i].item, recipe.elements[i].count);
        }
        
        Debug.Log("��� �Ϸ�!");
        return true; // ��� ����
    }
}
