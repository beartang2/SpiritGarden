using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setRecipeText : MonoBehaviour
{
    [SerializeField] private BuildRecipe recipe;           // ǥ���� ������
    private Text recipeText;         // UI �ؽ�Ʈ ������Ʈ

    void Start()
    {
        this.recipeText = GetComponent<Text>();
        DisplayRecipe();
    }

    void DisplayRecipe()
    {
        if (recipe == null || recipeText == null)
            return;

        // ���� �ؽ�Ʈ �ʱ�ȭ
        recipeText.text = $"�ʿ��� ���\n\n";

        foreach (var items in recipe.elements)
        {
            // �� ��Ḧ �ؽ�Ʈ�� ��ȯ
            recipeText.text += $"{items.item.Name} : {items.count}\n";
        }
    }
}
