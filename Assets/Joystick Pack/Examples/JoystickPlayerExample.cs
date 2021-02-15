using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;
    public Rigidbody2D rb;

    public float horizontalMove = 0f;
    public float verticalMove = 0f;

    public void FixedUpdate()
    {
        float x = fixedJoystick.Horizontal;
        float y = fixedJoystick.Vertical;
        rb.velocity = new Vector2(x * speed, y * speed);
      
    }
}