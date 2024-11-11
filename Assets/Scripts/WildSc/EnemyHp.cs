using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private Image enemyHpBarImage;
    private int maxEnemyHealth = 100;
    private int currentEnemyHealth;

    private DroppingItem droppingItem;

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
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;  // ������Ƽ�� ���� ü���� ����

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
