using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistructionManager : MonoBehaviour
{
    public GameObject destroyEffect; // 파괴 시 효과
    public AudioClip hitSound; // 타격 소리
    public AudioClip destroySound; // 파괴 소리
    private AudioSource audioSc;
    
    public int maxHits = 3; // 타격에 필요한 총 횟수
    public int currentHits;

    private Vector3 effectPos;

    private void Start()
    {
        currentHits = maxHits; // 초기화
        audioSc = GetComponent<AudioSource>();

        effectPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public bool TakeHit()
    {
        currentHits--;

        // 파괴 효과 재생
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, effectPos, Quaternion.identity);
        }

        // 타격음 재생
        if (hitSound != null && audioSc != null)
        {
            audioSc.PlayOneShot(hitSound);
        }

        if (currentHits <= 0)
        {
            return true; // 파괴 가능
        }

        return false; // 아직 파괴되지 않음
    }

    public void PlaySFX()
    {
        // 파괴음 재생
        if (destroySound != null && audioSc != null)
        {
            audioSc.PlayOneShot(destroySound);
        }
    }
}
