//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class FollowPlayer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rigid;
    private Transform player;
    private Animator anim;
    
    public float chaseDis = 5f;
    private Vector3 startPos;
    public float stopDis = 1f;
    private float timer_N = 0f;

    //private bool isChasing = false;
    private bool isDashing = false;
    //public float jumpForce = 3f;
    private int rand_num = 99;
    //private bool grounded = true;

    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    private bool isMoving = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rigid = this.gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position;
        agent.Warp(transform.position);
        agent.updateRotation = false;
    }

    void Update()
    {
        if(player == null) // 플레이어가 없으면 건너뛰기
        {
            return;
        }
        float disToPlayer = Vector3.Distance(player.position, transform.position);
        
        Change_Rand_Num();
        UpdateAnimator();

        if (disToPlayer <= chaseDis)
        {
            //isChasing = true;
            //Random_Jumping();
            ChasePlayer(disToPlayer);

            Vector3 moveDirection = player.position - transform.position;
            moveDirection = moveDirection.normalized;

            // 애니메이터 파라미터 업데이트
            anim.SetFloat("MoveX", moveDirection.x);
            anim.SetFloat("MoveY", moveDirection.z); // Z축을 Y축으로 사용
        }
        else
        {
            //isChasing = false;
            Patrol();
        }
    }

    /*
    private void CheckGround()
    {
        RaycastHit hit = new RaycastHit();
        
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            if (hit.transform.tag != null)
            {
                grounded = true;
                agent.enabled = true;
                return;
            }
        }
        grounded = false;
    }
    */

    private void ChasePlayer(float disToPlayer)
    {
        //CheckGround();
        
        if (disToPlayer <= stopDis)
        {
            agent.isStopped = true;
            isMoving = false;
        }
        else
        {
            isMoving = true;
            agent.isStopped = false;
            agent.SetDestination(player.position);

            Random_Attack();
        }
    }

    private void Change_Rand_Num()
    {
        timer_N += Time.deltaTime;
        
        if (timer_N > 1.5f) // 1.5초마다 번호 갱신
        {
            rand_num = Random.Range(1, 16); // 랜덤 값
            Debug.Log("Chenging Num " + rand_num);
            timer_N = 0f;
        }
    }

    private void Random_Attack()
    {
        if (rand_num % 5 == 0 && !isDashing)
        {
            agent.speed = 0f;
            timer_N = 0f;

            if(agent.speed == 0)
            {
                isDashing = true;
                Debug.Log(agent.speed);

                agent.speed = 1000f;
            }
        }
        else
        {
            isDashing = false;
            agent.speed = 3.5f;
            Debug.Log("No Run!");
        }
    }

    /*
    private void Random_Jumping()
    {
        if(isChasing && grounded)
        {
            Vector3 jumpVelocity = Vector3.up * Mathf.Sqrt(jumpForce * -Physics.gravity.y);
            
            if (rand_num % 4 == 0)
            {
                agent.enabled = false;
                Debug.Log("Jumped!");
                rigid.AddForce(jumpVelocity, ForceMode.Impulse);
                grounded = false;
            }
        }
    }
    */

    private void Patrol()
    {
        isMoving = true;
        isDashing = false;
        agent.speed = 3.5f;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // 다음 정찰 지점으로 랜덤 이동
            currentPatrolIndex = Random.Range(1, patrolPoints.Length);
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);

            Vector3 moveDirection = patrolPoints[currentPatrolIndex].position - transform.position;
            moveDirection = moveDirection.normalized;

            // 애니메이터 파라미터 업데이트
            anim.SetFloat("MoveX", moveDirection.x);
            anim.SetFloat("MoveY", moveDirection.z); // Z축을 Y축으로 사용
        }
    }

    private void UpdateAnimator()
    {
        // 캐릭터가 이동하지 않는다면 애니메이션을 중지
        if (!isMoving)
        {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);
        }
    }
}
