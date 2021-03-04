using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;
    private Rigidbody2D rb;
    public Vector3 startingPoint;

    [SerializeField]
    private bool inSafeZone;
    private bool endZoneTouched;
    private GameManager gameManger;

    private Animator animator;
    private Vector2 movement;

    public PlayerType playerType;
    public bool enemyTouchedPlayer;

    private float lastPositionX;
    private float lastPositionY;
    private float _time = 1.1f;

    public enum PlayerType
    {
        GREEN, RED
    }

    public void Awake()
    {
        //  fixedJoystick = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
        speed = 5;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        gameManger = GameManager.instance;
        startingPoint = transform.localPosition;
        lastPositionX = transform.localPosition.x;
        lastPositionY = transform.localPosition.y;
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
        if (!enemyTouchedPlayer)
        {
            rb.MovePosition(rb.position + (movement * speed * Time.fixedDeltaTime));
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startingPoint, Time.deltaTime * speed);
            _time -= speed * Time.deltaTime;
            if (_time <= 0)
            {
                _time = 1.1f;
                Debug.Log(transform.localPosition.x - lastPositionX);
                animator.SetFloat("Horizontal", transform.localPosition.x - lastPositionX);
                animator.SetFloat("Vertical", transform.localPosition.y - lastPositionY);
                lastPositionY = transform.localPosition.y;
                lastPositionX = transform.localPosition.x;

            }
            if (Vector2.Distance(transform.position, startingPoint) < 0.02f)
            {
                enemyTouchedPlayer = false;
                gameManger.SetNextPlayer();
                Destroy(this.GetComponent<Player>());

            }
        }




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


            SoundManager.instance.PlayDamageSound();
            enemyTouchedPlayer = true;
            

        }

        else if (collision.CompareTag("EndZone"))
        {
            inSafeZone = true;
            endZoneTouched = true;

        }

        else if (collision.CompareTag("StartZone"))
        {
            inSafeZone = true;
            if (endZoneTouched)
            {
                if (playerType.Equals(PlayerType.GREEN))
                {
                    gameManger.GreenScoreUp();
                }
                else if (playerType.Equals(PlayerType.RED))
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
        if (collision.CompareTag("SafeZone") || collision.CompareTag("StartZone"))
        {
            inSafeZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SafeZone") || collision.CompareTag("StartZone"))
        {
            inSafeZone = false;
        }
    }

    public bool InSafeZone()
    {
        return inSafeZone;
    }



}