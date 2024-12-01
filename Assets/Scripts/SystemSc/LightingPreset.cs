using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Data/Lighting Preset", order = 1)]
public class LightingPreset : ScriptableObject
{
    public Gradient colorGradingFilter; // 밝기 및 색상
    public Gradient vignetteIntensity; // 비네트 강도
    public Gradient exposure; // 노출
}
