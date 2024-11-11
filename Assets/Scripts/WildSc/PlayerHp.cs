using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private Image hpBarImage; // 체력바 이미지
    private int maxHealth = 100;
    private int currentHealth;

    public int Health
    {
        get => currentHealth;
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth); // 체력이 0 이하로 떨어지지 않도록
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Start()
    {
        currentHealth = maxHealth; // 게임 시작 시 체력을 최대값으로 초기화
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            TakeDamage(10);
            Debug.Log("데미지 입음! -10\n" + "현재 데미지 : " + currentHealth);
        }
    }
    private void UpdateHealthBar()
    {
        // 체력값을 이미지의 fillAmount로 변환하여 체력바를 업데이트
        hpBarImage.fillAmount = currentHealth * 0.01f; // 체력 비율 변환
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;  // 프로퍼티를 통해 체력을 줄임

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();  // 피해를 입었을 때 체력바 업데이트
    }

    private void Die()
    {
        Debug.Log("Player died.");
        // 플레이어가 죽었을 때 처리
        gameObject.SetActive(false); // 우선 플레이어 비활성화
    }

    // 체력을 회복할 때 호출하는 함수
    // 나중에 회복물약 추가 시 사용
    /*
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();  // 회복할 때 체력바 업데이트
    }
    */
}
