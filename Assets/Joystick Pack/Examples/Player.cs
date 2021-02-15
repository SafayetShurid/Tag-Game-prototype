using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;
    public Rigidbody2D rb;

    private bool inSafeZone;

    public void FixedUpdate()
    {
        float x = fixedJoystick.Horizontal;
        float y = fixedJoystick.Vertical;
        rb.velocity = new Vector2(x * speed, y * speed);      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SafeZone"))
        {
            inSafeZone = true;
            Debug.Log("Entered Safe Zone");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SafeZone"))
        {
            inSafeZone = true;
            Debug.Log("In Safe Zone");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SafeZone"))
        {
            inSafeZone = false;
            Debug.Log("Exit Safe Zone");
        }
    }

    public bool InSafeZone()
    {
        return inSafeZone;
    }


   
}