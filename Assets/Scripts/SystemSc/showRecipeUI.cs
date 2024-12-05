using UnityEngine;

public class showRecipeUI : MonoBehaviour
{
    [SerializeField] private GameObject recipe_text;  // 표시할 UI 오브젝트
    //SerializeField] private GameObject image;  // 이미지

    void Start()
    {
        // UI 비활성화
        recipe_text.SetActive(false);
        //image.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 트리거에 들어오면 UI 활성화
        if (other.CompareTag("Player"))
        {
            recipe_text.SetActive(true);
            //image.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 플레이어가 트리거에서 나가면 UI 비활성화
        if (other.CompareTag("Player"))
        {
            recipe_text.SetActive(false);
            //image.SetActive(false);
        }
    }
}
