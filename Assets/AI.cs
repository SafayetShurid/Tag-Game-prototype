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

    public Player player;

    public float speed;

    public AIState aiState;

    private Transform currentDestination;

    public enum AIState
    {
        chasing, patroling
    }

    void Start()
    {
        isAtStartingPoint = true;
        
    }

    void Update()
    {
        CheckActivePlayer();

        if (CheckPlayerInRange())
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
        else
        {
            Patrol();
          
        }
    }

    private void CheckActivePlayer()
    {        
        player = GameManager.instance.currentPlayer.GetComponent<Player>();    
    }

    private void ChasePlayer()
    {

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        aiState = AIState.chasing;
        isChasing = true;

    }  

    private void Patrol()
    {
        if(isAtEndPoint)
        {            
            SetDestination(patrolStartPoint);
        }
        else if(isAtStartingPoint)
        {
            SetDestination(patrolEndPoint);
        }

        if (Vector2.Distance(transform.position, patrolEndPoint.position) < 0.02f && !isAtEndPoint)
        {
            isAtEndPoint = true;
            isAtStartingPoint = false;
        }
        else if(Vector2.Distance(transform.position, patrolStartPoint.position) < 0.02f && !isAtStartingPoint)
        {
            isAtEndPoint = false;
            isAtStartingPoint = true;
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
