using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private Image hpBarImage; // 체력바 이미지
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
        startPos = transform.position;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 데미지 딜레이 관리
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

        audioSource.PlayOneShot(sounds[0]);

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
        UpdateHealthBar();  // 회복할 때 체력바 업데이트
    }

}
