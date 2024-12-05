using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource; // �Ҹ��� ����� AudioSource
    public AudioClip[] toolSounds;  // ���� ȿ���� �迭 (0: Sword, 1: Pickaxe, 2: Sickle, 3: Default)

    private Dictionary<string, int> toolSoundMapping; // ���� �̸��� �迭 �ε��� ����

    private void Start()
    {
        // ���� �̸��� ����� Ŭ�� �ε����� ����
        toolSoundMapping = new Dictionary<string, int>
        {
            { "Sword", 0 },
            { "Pickaxe", 1 },
            { "Sickle", 2 },
            { "Default", 3 } // �⺻ ȿ����
        };
    }

    public void ToolSound(string item)
    {
        if (Input.GetMouseButtonDown(0)) // ��Ŭ��
        {
            if (audioSource != null && toolSounds.Length > 0)
            {
                if (toolSoundMapping.TryGetValue(item, out int index) && index < toolSounds.Length)
                {
                    // �ش� ������ ȿ������ ���
                    audioSource.PlayOneShot(toolSounds[index]);
                }
                else
                {
                    // ���ε��� ���� ��� �⺻ ȿ���� ���
                    Debug.LogWarning("Unknown tool: " + item);
                    audioSource.PlayOneShot(toolSounds[toolSoundMapping["Default"]]);
                }
            }
        }
    }
}
