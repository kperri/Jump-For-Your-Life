using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{

    public event Action deathEvent;
    public event Action balloonEvent;
    public Sprite playerWithBoots;
    public Vector2 leftJump;
    public Vector2 rightJump;
    public AudioClip jumpClip;
    public AudioClip bootsClip;

    Rigidbody2D player;
    Vector3 playerPosition;
    float maxJumpHeight = 10;
    float minJumpHeight = 6;
    float currentJumpHeight = 0;
    bool isGrounded;
    bool isJumping;
    bool rightWall;
    bool dead;
    bool fireImmunity;
    bool startGame;
    bool enterRightWall;
    bool enterLeftWall;
    AudioSource sfx;
    SpriteRenderer spriteRendering;
    Sprite playerNoBoots;
    CountdownScript cd;

    void OnEnable ()
    {
        cd = FindObjectOfType<CountdownScript>();
        cd.countdownEvent += OnCountdownFinish;
        cd.pauseEvent += FreezeMovement;
    }

    void OnDisable ()
    {
        cd.countdownEvent -= OnCountdownFinish;
        cd.pauseEvent -= FreezeMovement;
    }

    void Start ()
    {
        player = GetComponent<Rigidbody2D> ();
        GetComponent<BoxCollider2D> ().isTrigger = false;
        enterLeftWall = true;
        rightWall = true;
        isGrounded = true;
        dead = false;
        //		letGo = true;
        //		jumped = false;
        startGame = false;
        //once = false;
        spriteRendering = GetComponent<SpriteRenderer> ();
        playerNoBoots = GetComponent<SpriteRenderer> ().sprite;
        sfx = GetComponent<AudioSource> ();
        print (DataPreservation.data._boots + " boots");
        print (DataPreservation.data._coins + " coins");
        print (DataPreservation.data._deathCount + " deaths");
        print (DataPreservation.data._watchedBootsAD + " bootsAD");
        print (DataPreservation.data._watchedCoinAD + " coinsAD");
        if (DataPreservation.data._boots >= 1)
        {
            DataPreservation.data._boots -= 1;
            DataPreservation.data.SaveData ();
            fireImmunity = true;
            spriteRendering.sprite = playerWithBoots;
        }
    }

    void Update ()
    {
        if (startGame)
        {
            if ((Input.touchCount >= 1 || Input.GetButtonDown("Jump")) && rightWall && isGrounded)
            {
                Jump(leftJump, false);
            }
            else if ((Input.touchCount >= 1 || Input.GetButtonDown("Jump")) && !rightWall && isGrounded)
            {
                Jump(rightJump, true);
            }
            //if (Input.touchCount >= 1 || Input.GetButtonDown("Jump"))
            //    isJumping = true;

            //if (isJumping)
            //{
            //    if (currentJumpHeight < maxJumpHeight)
            //    {
            //        Vector3 newPos = new Vector3(transform.position.x + 0.1f, transform.position.y + 0.5f, transform.position.z);
            //        transform.position = newPos;
            //        currentJumpHeight += 0.5f;
            //    }
            //    else
            //    {
            //        currentJumpHeight = 0;
            //        isJumping = false;
            //    }
            //}
        }
    }

    void Jump (Vector2 jumpDirection, bool direction) // TODO fix bug causing player to stick to the wall
    {
        if (sfx.clip == bootsClip)
        {
            sfx.clip = jumpClip;
        }
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
        rightWall = direction;
        player.AddForce (jumpDirection, ForceMode2D.Impulse);
        isGrounded = false;
        spriteRendering.flipX = direction;
        sfx.Play ();
    }

    void CheckWall (Collision2D coll, float amountToMove, bool whichWall)
    {
        playerPosition.x = coll.gameObject.transform.position.x + amountToMove;
        playerPosition.y = transform.position.y;
        playerPosition.z = -1;
        player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        enterRightWall = !whichWall;
        enterLeftWall = whichWall;
    }

    void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.CompareTag ("RightWall") && enterRightWall)
        {
            CheckWall (coll, -0.69f, true);
        }
        else if (coll.gameObject.CompareTag ("LeftWall") && enterLeftWall)
        {
            CheckWall (coll, 0.69f, false);
        }
        isGrounded = true;
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if ((coll.gameObject.CompareTag ("Death") || coll.gameObject.CompareTag ("Fireball")) && !fireImmunity)
        {
            dead = true;
            EndGame ();
        }
        else if ((coll.gameObject.CompareTag ("Death") || coll.gameObject.CompareTag ("Fireball")) && fireImmunity)
        {
            Invoke ("LoseBoots", 0.3f);
        }
        if (coll.gameObject.CompareTag ("MovingFire"))
        {
            dead = true;
            EndGame ();
        }
        if (coll.gameObject.CompareTag ("CollectableBalloon"))
        {
            if (balloonEvent != null)
            {
                balloonEvent ();
            }
        }
        if (coll.gameObject.CompareTag ("Coin"))
        {
            DataPreservation.data._coins += 10;
            DataPreservation.data.SaveData ();
            print (DataPreservation.data._coins + " coins");
        }
        if (coll.gameObject.CompareTag ("Boots"))
        {
            if (sfx.clip == jumpClip)
            {
                sfx.clip = bootsClip;
            }
            sfx.Play ();
            fireImmunity = true;
            spriteRendering.sprite = playerWithBoots;
        }
    }

    public void BootsPower ()
    {
        fireImmunity = true;
        spriteRendering.sprite = playerWithBoots;
    }

    void LoseBoots ()
    {
        fireImmunity = true;
        spriteRendering.sprite = playerNoBoots;
    }

    void OnCountdownFinish ()
    {
        startGame = true;
        player.constraints = RigidbodyConstraints2D.None;
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (rightWall && !isGrounded)
        {
            player.AddForce (rightJump / 2f, ForceMode2D.Impulse);
        }
        else if (!rightWall && !isGrounded)
        {
            player.AddForce (leftJump / 2f, ForceMode2D.Impulse);
        }
    }

    void FreezeMovement ()
    {
        startGame = false;
        player.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void EndGame ()
    {
        if (dead)
        {
            if (deathEvent != null)
            {
                deathEvent ();
            }
        }
    }
}
