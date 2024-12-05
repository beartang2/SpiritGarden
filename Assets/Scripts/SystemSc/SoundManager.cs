using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource; // 소리를 재생할 AudioSource
    public AudioClip[] toolSounds;  // 도구 효과음 배열 (0: Sword, 1: Pickaxe, 2: Sickle, 3: Default)

    private Dictionary<string, int> toolSoundMapping; // 도구 이름과 배열 인덱스 매핑

    private void Start()
    {
        // 도구 이름과 오디오 클립 인덱스를 매핑
        toolSoundMapping = new Dictionary<string, int>
        {
            { "Sword", 0 },
            { "Pickaxe", 1 },
            { "Sickle", 2 },
            { "Default", 3 } // 기본 효과음
        };
    }

    public void ToolSound(string item)
    {
        if (Input.GetMouseButtonDown(0)) // 좌클릭
        {
            if (audioSource != null && toolSounds.Length > 0)
            {
                if (toolSoundMapping.TryGetValue(item, out int index) && index < toolSounds.Length)
                {
                    // 해당 도구의 효과음을 재생
                    audioSource.PlayOneShot(toolSounds[index]);
                }
                else
                {
                    // 매핑되지 않은 경우 기본 효과음 재생
                    Debug.LogWarning("Unknown tool: " + item);
                    audioSource.PlayOneShot(toolSounds[toolSoundMapping["Default"]]);
                }
            }
        }
    }
}
