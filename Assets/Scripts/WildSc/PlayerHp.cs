using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private Image hpBarImage; // ü�¹� �̹���
    [SerializeField] private GameObject hpCanvas;
    private Vector3 startPos;
    public AudioClip[] sounds;
    private AudioSource audioSource;

    private int maxHealth = 100;
    private float currentHealth;
    private bool damaging = false;
    private float damageDelay = 0f;
    private float healDelay = 0f;
    private bool healing = false;

    public float Health
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
        startPos = transform.position;

        audioSource = GetComponent<AudioSource>();
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
        if (!healing && !damaging && currentHealth < 100f)
        {
            healDelay += Time.deltaTime;
            if (healDelay > 3f)
            {
                Debug.Log("Start heal");
                Heal(1f);
                healDelay = 0f;
                healing = false;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && !damaging)
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

        audioSource.PlayOneShot(sounds[0]);

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
        transform.position = startPos;
        currentHealth = maxHealth;
        hpCanvas.SetActive(false);
    }

    public void Heal(float healAmount)
    {
        healing = true;

        currentHealth += healAmount;

        //audioSource.PlayOneShot(sounds[1]);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();  // ȸ���� �� ü�¹� ������Ʈ
    }

}
