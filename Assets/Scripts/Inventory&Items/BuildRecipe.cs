using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Recipe")]
public class BuildRecipe : ScriptableObject
{
    public List<ItemSlot> elements;
    //public ItemSlot output;
}
