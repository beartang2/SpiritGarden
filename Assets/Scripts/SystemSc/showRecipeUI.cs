using UnityEngine;

public class showRecipeUI : MonoBehaviour
{
    [SerializeField] private GameObject recipe_text;  // ǥ���� UI ������Ʈ
    //SerializeField] private GameObject image;  // �̹���

    void Start()
    {
        // UI ��Ȱ��ȭ
        recipe_text.SetActive(false);
        //image.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾ Ʈ���ſ� ������ UI Ȱ��ȭ
        if (other.CompareTag("Player"))
        {
            recipe_text.SetActive(true);
            //image.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // �÷��̾ Ʈ���ſ��� ������ UI ��Ȱ��ȭ
        if (other.CompareTag("Player"))
        {
            recipe_text.SetActive(false);
            //image.SetActive(false);
        }
    }
}
