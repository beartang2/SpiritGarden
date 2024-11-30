using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setRecipeText : MonoBehaviour
{
    [SerializeField] private BuildRecipe recipe;           // 표시할 레시피
    private Text recipeText;         // UI 텍스트 컴포넌트

    void Start()
    {
        this.recipeText = GetComponent<Text>();
        DisplayRecipe();
    }

    void DisplayRecipe()
    {
        if (recipe == null || recipeText == null)
            return;

        // 기존 텍스트 초기화
        recipeText.text = $"필요한 재료\n\n";

        foreach (var items in recipe.elements)
        {
            // 각 재료를 텍스트로 변환
            recipeText.text += $"{items.item.Name} : {items.count}\n";
        }
    }
}
