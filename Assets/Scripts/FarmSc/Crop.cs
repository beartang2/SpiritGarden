using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Crop")]
public class Crop : ScriptableObject
{
    public int growTime = 10;
    public Item yield;
    public int cnt = 1;

    public List<Sprite> sprites;
    public List<int> growthStageTIme;
}
