using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public AIRadius aiRadius;

    public Transform patrolStartPoint;
    public Transform patrolEndPoint;
    public Transform patrolMiddleStartPoint;
    public Transform patrolMiddleEndPoint;

    private bool isChasing;
    private bool readyToPatrol;

    private bool isAtEndPoint;
    private bool isAtStartingPoint;
    private bool isAtMiddleEndPoint;
    private bool isAtMiddleStartingPoint;

    private bool middleRoundTripped;
    private bool justFromEndPoint;

    public bool ifPlayerInRange;

    public Player player;

    public float speed;

    public AIState aiState;
    public AIType aiType;

    private Transform currentDestination;

    private Animator animator;

    private float lastPositionX;
    private float lastPositionY;
    private float _time = 1.0f;

    public enum AIState
    {
        chasing, patroling
    }

    public enum AIType
    {
        both, single
    }

    void Start()
    {
        //Application.targetFrameRate = 60;
        //isAtEndPoint = true;
        isAtStartingPoint = true;
        lastPositionX = transform.localPosition.x;
        lastPositionY = transform.localPosition.y;

        animator = GetComponent<Animator>();

    }

    void Update()
    {
        CheckActivePlayer();

        if (ifPlayerInRange=CheckPlayerInRange())
        {
            if(player!=null)
            {
                if (!player.InSafeZone())
                {
                    ChasePlayer();
                }
                else
                {
                    Patrol();

                }
            }
           

        }
        else
        {
            Patrol();

        }




        // animator.SetFloat("Horizontal", transform.localPosition.x);
       
        animator.SetBool("isWalking", true);
        _time -= speed * Time.deltaTime;
        if (_time <= 0)
        {
            _time = 1.0f;
             Debug.Log(transform.localPosition.x - lastPositionX);
            animator.SetFloat("Horizontal", transform.localPosition.x - lastPositionX);
            animator.SetFloat("Vertical", transform.localPosition.y - lastPositionY);
            lastPositionY = transform.localPosition.y;
            lastPositionX = transform.localPosition.x;

        }




    }

    private void CheckActivePlayer()
    {
        if (GameManager.instance.currentPlayer != null)
        {
            player = GameManager.instance.currentPlayer.GetComponent<Player>();
        }

    }

    private void ChasePlayer()
    {

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        aiState = AIState.chasing;
        isChasing = true;

    }

    private void Patrol()
    {
        if (aiType.Equals(AIType.single))
        {
            if (isAtEndPoint)
            {
                SetDestination(patrolStartPoint);
            }
            else if (isAtStartingPoint)
            {
                SetDestination(patrolEndPoint);
            }

            if (Vector2.Distance(transform.position, patrolEndPoint.position) < 0.02f && !isAtEndPoint)
            {
                isAtEndPoint = true;
                isAtStartingPoint = false;
            }
            else if (Vector2.Distance(transform.position, patrolStartPoint.position) < 0.02f && !isAtStartingPoint)
            {
                isAtEndPoint = false;
                isAtStartingPoint = true;
            }
        }
        else
        {
            if (isAtEndPoint)
            {
                SetDestination(patrolStartPoint);
               
            }
            else if (isAtStartingPoint)
            {
                SetDestination(patrolMiddleStartPoint);
                middleRoundTripped = false;
                justFromEndPoint = false;
            }
            else if (isAtMiddleStartingPoint)
            {
                if(middleRoundTripped)
                {
                    if(justFromEndPoint)
                    {
                        SetDestination(patrolStartPoint);
                    }
                    else
                    {
                        SetDestination(patrolEndPoint);
                    }
                                       
                }               
                else
                {
                    SetDestination(patrolMiddleEndPoint);
                   
                }
                
            }
            else if (isAtMiddleEndPoint)
            {
                SetDestination(patrolMiddleStartPoint);
                middleRoundTripped = true; ;
            }



            if (Vector2.Distance(transform.position, patrolEndPoint.position) < 0.02f && !isAtEndPoint)
            {
                isAtEndPoint = true;
                isAtStartingPoint = false;
                isAtMiddleEndPoint = false;
                isAtMiddleStartingPoint = false;
                justFromEndPoint = true;
            }
            else if (Vector2.Distance(transform.position, patrolStartPoint.position) < 0.02f && !isAtStartingPoint)
            {
                isAtEndPoint = false;
                isAtStartingPoint = true;
                isAtMiddleEndPoint = false;
                isAtMiddleStartingPoint = false;
            }
            else if (Vector2.Distance(transform.position, patrolMiddleStartPoint.position) < 0.01f && !isAtMiddleStartingPoint)
            {
                isAtEndPoint = false;
                isAtStartingPoint = false;
                isAtMiddleEndPoint = false;
                isAtMiddleStartingPoint = true;
               
            }
            else if (Vector2.Distance(transform.position, patrolMiddleEndPoint.position) < 0.02f && !isAtMiddleEndPoint)
            {
                isAtEndPoint = false;
                isAtStartingPoint = false;
                isAtMiddleEndPoint = true;
                isAtMiddleStartingPoint = false;
            }
        }


    }

    private bool CheckPlayerInRange()
    {
        if (aiRadius.GetTarget() != null)
        {
            return true;
        }
        else
        {
            return false;

        }
    }

    private void SetDestination(Transform destinationPoint)
    {
        currentDestination = destinationPoint;
        transform.position = Vector2.MoveTowards(transform.position, destinationPoint.position, speed * Time.deltaTime);
       
    }

}
