using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;
    public Rigidbody2D rb;

    private bool inSafeZone;

    private GameManager gameManger;

    public Animator animator;

    public void Awake()
    {
        fixedJoystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        speed = 5;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        gameManger = GameManager.instance;
    }

    public void FixedUpdate()
    {
        float x = fixedJoystick.Horizontal;
        float y = fixedJoystick.Vertical;
        rb.velocity = new Vector2(x * speed, y * speed);

        if(rb.velocity.magnitude>0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }


        float posX = Mathf.Clamp(transform.position.x, -23f, 12f);
        float posY = Mathf.Clamp(transform.position.y, -19f, 19f);

        transform.position = new Vector3(posX, posY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SafeZone"))
        {
            inSafeZone = true;

            if (collision.GetComponent<Zone>().dangerZone != null)
            {

                SoundManager.instance.PlayCautionSound();
            }
            else
            {
                SoundManager.instance.PlayEnterSafeZone();
            }

        }

        else if (collision.CompareTag("Enemy"))
        {
            gameManger.RedScoreUp();
            gameManger.SetNextPlayer();
            SoundManager.instance.PlayDamageSound();
            if (!inSafeZone)
            {
                Destroy(this.gameObject);
            }

        }

        else if (collision.CompareTag("EndZone"))
        {
            inSafeZone = true;
            gameManger.GreenScoreUp();
            SoundManager.instance.PlaywinSound();
            StartCoroutine(EndPointReached());

        }
    }

    IEnumerator EndPointReached()
    {
        yield return new WaitForSeconds(0.8f);
        gameManger.SetNextPlayer();
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SafeZone"))
        {
            inSafeZone = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SafeZone"))
        {
            inSafeZone = false;

        }
    }

    public bool InSafeZone()
    {
        return inSafeZone;
    }



}