using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private Image enemyHpBarImage;
    private DroppingItem droppingItem;
    public AudioClip hitSound;
    private AudioSource audioSource;

    private int maxEnemyHealth = 100;
    private int currentEnemyHealth;
    private bool damaging = false;
    private float damageDelay = 0f;

    public int Health
    {
        get => currentEnemyHealth;
        set
        {
            currentEnemyHealth = Mathf.Clamp(value, 0, maxEnemyHealth); // 체력이 0 이하로 떨어지지 않도록
            if (currentEnemyHealth <= 0)
            {
                Die();
            }
        }
    }

    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;

        droppingItem = GetComponent<DroppingItem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (damaging)
        {
            damageDelay += Time.deltaTime;
            if (damageDelay > 1f)
            {
                damaging = false; // 데미지 상태 해제
                damageDelay = 0f; // 딜레이 초기화
                //Debug.Log("딜레이 초기화");
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (!damaging)
        {
            damaging = true;
            
            Health -= amount;  // 프로퍼티를 통해 체력을 줄임

            audioSource.PlayOneShot(hitSound);

            Debug.Log("데미지 입음!");
        }
        if (currentEnemyHealth < 0)
        {
            currentEnemyHealth = 0;
        }
        UpdateHealthBar();  // 피해를 입었을 때 체력바 업데이트
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        // 에너미가 죽었을 때 처리
        droppingItem.KillEnemy();
        ResetHealth();
    }

    private void UpdateHealthBar()
    {
        // 체력값을 이미지의 fillAmount로 변환하여 체력바를 업데이트
        enemyHpBarImage.fillAmount = currentEnemyHealth * 0.01f; // 체력 비율 변환
    }

    public void ResetHealth()
    {
        currentEnemyHealth = maxEnemyHealth;
    }
}
