using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;
    private Rigidbody2D rb;
    public Vector3 startingPoint;

    private bool inSafeZone;
    private bool endZoneTouched;
    private GameManager gameManger;

    private Animator animator;
    private Vector2 movement;

    public PlayerType playerType;

    public enum PlayerType
    {
        GREEN,RED
    }
    
    public void Awake()
    {
      //  fixedJoystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        speed = 15;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        gameManger = GameManager.instance;
        startingPoint = transform.localPosition;
    }

    public void FixedUpdate()
    {
        // float x = fixedJoystick.Horizontal;
        // float y = fixedJoystick.Vertical;

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //rb.velocity = new Vector2(x * speed, y * speed);
        rb.MovePosition(rb.position + (movement * speed * Time.fixedDeltaTime));

        

        float posX = Mathf.Clamp(transform.position.x, -17f, 10f);
        float posY = Mathf.Clamp(transform.position.y, -7.5f, 13f);

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
            endZoneTouched = true;           

        }

        else if (collision.CompareTag("StartZone"))
        {
            inSafeZone = true;
            if(endZoneTouched)
            {
                if(playerType.Equals(PlayerType.GREEN))
                {
                    gameManger.GreenScoreUp();
                }
                else if(playerType.Equals(PlayerType.RED))
                {
                    gameManger.RedScoreUp();
                }
               
                SoundManager.instance.PlaywinSound();
                StartCoroutine(EndPointReached());
            }

        }
    }

    IEnumerator EndPointReached()
    {
        yield return new WaitForSeconds(0.8f);
        gameManger.SetNextPlayer();
        Destroy(this.GetComponent<Player>());
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