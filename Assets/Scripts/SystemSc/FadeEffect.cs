using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject[] spirits; // ���� ������Ʈ��
    [SerializeField] Transform[] npcPositions; // �̵���ų ��ġ ��ǥ
    [SerializeField] GameObject[] UI; // UI��
    [SerializeField] CameraManager camSc; // ī�޶� �Ŵ��� ��ũ��Ʈ

    public Image fadeImage; // ������ UI Image
    public float fadeDuration = 1f; // ���̵� �ð�
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
        player.transform.position = startPos; // �÷��̾ ó�� ��ġ�� �̵���Ŵ
        // ���� NPC���� ��ġ�� �ű�
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
            fadeImage.color = new Color(0, 0, 0, alpha); // ���� ���� ����
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1); // ������ �˰� ����

        yield return new WaitForSeconds(1f);
        MovePlayer_And_NPC(); // �÷��̾�� npc ��ġ �̵�
        ChangeBackground(); // ��� ������ ����

        StartFadeIn();
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha); // ���� ���� ����
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0); // ������ �����ϰ� ����

        yield return new WaitForSeconds(1.5f);
        UI[0].SetActive(true); // ���� UI Ȱ��ȭ

        yield return new WaitForSeconds(4f);
        UI[1].SetActive(true); // ���� UI Ȱ��ȭ
    }
}
