using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private Image hpBarImage; // ü�¹� �̹���
    private int maxHealth = 100;
    private int currentHealth;
    private bool damaging = false;
    private float damageDelay = 0f;

    public int Health
    {
        get => currentHealth;
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth); // ü���� 0 ���Ϸ� �������� �ʵ���
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Start()
    {
        currentHealth = maxHealth; // ���� ���� �� ü���� �ִ밪���� �ʱ�ȭ
    }

    private void Update()
    {
        // ������ ������ ����
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && !damaging)
        {
            damaging = true;
            TakeDamage(10);
            Debug.Log("������ ����! -10\n" + "���� ������ : " + currentHealth);

        }
    }
    private void UpdateHealthBar()
    {
        // ü�°��� �̹����� fillAmount�� ��ȯ�Ͽ� ü�¹ٸ� ������Ʈ
        hpBarImage.fillAmount = currentHealth * 0.01f; // ü�� ���� ��ȯ
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;  // ������Ƽ�� ���� ü���� ����

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();  // ���ظ� �Ծ��� �� ü�¹� ������Ʈ
    }

    private void Die()
    {
        Debug.Log("Player died.");
        // �÷��̾ �׾��� �� ó��
        gameObject.SetActive(false); // �켱 �÷��̾� ��Ȱ��ȭ
    }

    // ü���� ȸ���� �� ȣ���ϴ� �Լ�
    // ���߿� ȸ������ �߰� �� ���
    /*
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();  // ȸ���� �� ü�¹� ������Ʈ
    }
    */
}
