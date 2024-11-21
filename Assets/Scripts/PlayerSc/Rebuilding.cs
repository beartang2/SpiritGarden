using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebuilding : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;

    public void Rebuild(BuildRecipe recipe)
    {
        bool buildable = true;

        for(int i=0; i<recipe.elements.Count; i++)
        {
            if (inventory.CheckItem(recipe.elements[i]) == false)
            {
                Debug.Log("재료가 부족합니다");
                buildable = false;
                break;
            }
        }

        if(buildable == false)
        {
            return;
        }

        for(int i=0; i<recipe.elements.Count; i++)
        {
            inventory.Remove(recipe.elements[i].item, recipe.elements[i].count);
        }
        
        Debug.Log("재건 완료!");
    }
}
