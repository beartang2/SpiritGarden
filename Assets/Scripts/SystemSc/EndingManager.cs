using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    // ScriptableObject �迭�� ��� �ǹ� ������ ����
    public List<BuildRecipe> buildingList;
    [SerializeField] FadeEffect fadeEffect;
    [SerializeField] Transform player;
    [SerializeField] GameObject[] spirits; // ���� ������Ʈ��
    [SerializeField] Transform[] npcPositions; // �̵���ų ��ġ ��ǥ
    private Rigidbody rb;
    private PlayerMovement movementSc;
    private Animator anim;
    private bool allBuilt = false;
    Vector3 startPos;
    private bool isEnding = false;

    private void Start()
    {
        startPos = player.transform.position;
        rb = player.transform.GetComponent<Rigidbody>();
        movementSc = player.transform.GetComponent<PlayerMovement>();
        anim = player.transform.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckBuilds();
    }

    private void CheckBuilds()
    {
        foreach (var build in buildingList)
        {
            if(!build.builded)
            {
                allBuilt = false;
                break;
            }
            allBuilt = true;
        }
        if(allBuilt && !isEnding)
        {
            isEnding = true;
            // ���� ȣ��
            Debug.Log("��� �ǹ��� ��ǵǾ����ϴ�. ���� ȣ��!");
            EndingGame();
        }
    }

    private void EndingGame()
    {
        movementSc.enabled = false; // �÷��̾� �̵� ����
        rb.isKinematic = false; // ���� ���� ����
        anim.SetFloat("MoveX", 0); // �ִϸ��̼� ����
        anim.SetFloat("MoveY", 0);

        // ���� ����
        fadeEffect.StartFadeOut(); // ���̵� �ƿ�
    }
}
