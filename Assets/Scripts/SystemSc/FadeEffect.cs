using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject[] spirits; // 정령 오브젝트들
    [SerializeField] Transform[] npcPositions; // 이동시킬 위치 좌표
    [SerializeField] GameObject[] UI; // UI들
    [SerializeField] CameraManager camSc; // 카메라 매니저 스크립트

    public Image fadeImage; // 검은색 UI Image
    public float fadeDuration = 1f; // 페이드 시간
    private Vector3 startPos;
    private float timer;

    private void Start()
    {
        startPos = player.transform.position;
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    private void MovePlayer_And_NPC()
    {
        player.transform.position = startPos; // 플레이어를 처음 위치로 이동시킴
        // 정령 NPC들의 위치도 옮김
        for (int i = 0; i < spirits.Length; i++)
        {
            spirits[i].transform.position = npcPositions[i].position;
        }
    }

    private void ChangeBackground()
    {
        camSc.ChangeProfile(1, 4);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha); // 알파 값만 변경
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1); // 완전히 검게 설정

        yield return new WaitForSeconds(1f);
        MovePlayer_And_NPC(); // 플레이어와 npc 위치 이동
        ChangeBackground(); // 배경 프로필 변경

        StartFadeIn();
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha); // 알파 값만 변경
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0); // 완전히 투명하게 설정

        yield return new WaitForSeconds(1.5f);
        UI[0].SetActive(true); // 다음 UI 활성화

        yield return new WaitForSeconds(4f);
        UI[1].SetActive(true); // 다음 UI 활성화
    }
}
