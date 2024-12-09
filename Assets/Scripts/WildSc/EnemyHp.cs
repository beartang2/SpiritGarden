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
            currentEnemyHealth = Mathf.Clamp(value, 0, maxEnemyHealth); // ü���� 0 ���Ϸ� �������� �ʵ���
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
                damaging = false; // ������ ���� ����
                damageDelay = 0f; // ������ �ʱ�ȭ
                //Debug.Log("������ �ʱ�ȭ");
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (!damaging)
        {
            damaging = true;
            
            Health -= amount;  // ������Ƽ�� ���� ü���� ����

            audioSource.PlayOneShot(hitSound);

            Debug.Log("������ ����!");
        }
        if (currentEnemyHealth < 0)
        {
            currentEnemyHealth = 0;
        }
        UpdateHealthBar();  // ���ظ� �Ծ��� �� ü�¹� ������Ʈ
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        // ���ʹ̰� �׾��� �� ó��
        droppingItem.KillEnemy();
        ResetHealth();
    }

    private void UpdateHealthBar()
    {
        // ü�°��� �̹����� fillAmount�� ��ȯ�Ͽ� ü�¹ٸ� ������Ʈ
        enemyHpBarImage.fillAmount = currentEnemyHealth * 0.01f; // ü�� ���� ��ȯ
    }

    public void ResetHealth()
    {
        currentEnemyHealth = maxEnemyHealth;
    }
}
