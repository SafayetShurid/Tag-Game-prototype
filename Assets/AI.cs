using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public AIRadius aiRadius;

    public Transform patrolStartPoint;
    public Transform patrolEndPoint;

    private bool isChasing;
    private bool readyToPatrol;

    private bool isAtEndPoint;
    private bool isAtStartingPoint;

    private Player player;

    public float speed;

    public AIState aiState;

    public enum AIState
    {
        chasing, patroling
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (CheckPlayerInRange())
        {
            if (!player.InSafeZone())
            {
                ChasePlayer();
            }
            else
            {
                Patrol();
                //GoBackToPatrolStartingPosition();
            }

        }
        else
        {
            Patrol();
            //GoBackToPatrolStartingPosition();
        }
    }

    private void ChasePlayer()
    {

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        aiState = AIState.chasing;
        isChasing = true;

    }

    private void GoBackToPatrolStartingPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolStartPoint.transform.position, speed * Time.deltaTime);
        isChasing = false;
    }

    private void CheckPatrolCondition()
    {
        if (Vector2.Distance(transform.position, patrolStartPoint.position) < 0.02f)
        {
            readyToPatrol = true;
        }
        else
        {
            readyToPatrol = false;
        }
    }

    private void Patrol(Transform destination)
    {
        if(isAtEndPoint)
        {
            GoBackToPatrolStartingPosition();
        }
        else if(isAtStartingPoint)
        {
            GoToEndPosition();
        }

        if (Vector2.Distance(transform.position, patrolEndPoint.position) < 0.02f)
        {
            isAtEndPoint = true;
            isAtStartingPoint = false;
        }
        else
        {
            isAtEndPoint = false;
            isAtStartingPoint = true;
        }

    }

    private void GoToEndPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolEndPoint.transform.position, speed * Time.deltaTime);
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
}
