using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistructionManager : MonoBehaviour
{
    public GameObject destroyEffect; // �ı� �� ȿ��
    public AudioClip hitSound; // Ÿ�� �Ҹ�
    public AudioClip destroySound; // �ı� �Ҹ�
    private AudioSource audioSc;
    
    public int maxHits = 3; // Ÿ�ݿ� �ʿ��� �� Ƚ��
    public int currentHits;

    private Vector3 effectPos;

    private void Start()
    {
        currentHits = maxHits; // �ʱ�ȭ
        audioSc = GetComponent<AudioSource>();

        effectPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public bool TakeHit()
    {
        currentHits--;

        // �ı� ȿ�� ���
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, effectPos, Quaternion.identity);
        }

        // Ÿ���� ���
        if (hitSound != null && audioSc != null)
        {
            audioSc.PlayOneShot(hitSound);
        }

        if (currentHits <= 0)
        {
            return true; // �ı� ����
        }

        return false; // ���� �ı����� ����
    }

    public void PlaySFX()
    {
        // �ı��� ���
        if (destroySound != null && audioSc != null)
        {
            audioSc.PlayOneShot(destroySound);
        }
    }
}
