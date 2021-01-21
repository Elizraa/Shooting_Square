using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 8f, maxVelocity = 4f, distance, jumpForce = 10f;

    private Rigidbody2D myBody;
    private bool isGrounded = true, canJump = true, lookRight = true;
    BoxCollider2D boxCollider;
    private LayerMask m_WhatIsGround;

    public Transform firePoint;
    public GameObject bullet;

    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip shootSound;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        m_WhatIsGround = LayerMask.GetMask("Ground");
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
    }

    private void Update()
    {
        PlayerJump();
        GroundCheck();
        if (Input.GetButtonDown("Fire1") && LevelControl.levelControl.shot > 0) shoot();
    }

    void shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        audioSource.volume = 0.3f;
        audioSource.PlayOneShot(shootSound);
        LevelControl.levelControl.shot--;
        LevelControl.levelControl.UpdateShot();
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            audioSource.volume = 0.65f;
            myBody.AddForce(new Vector2(0f, jumpForce));
            audioSource.PlayOneShot(jumpSound);
            canJump = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            audioSource.volume = 0.65f;
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
            myBody.AddForce(new Vector2(0f, jumpForce));
            audioSource.PlayOneShot(jumpSound);
            canJump = false;
        }
    }


    void PlayerMove()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);
        float move = Input.GetAxisRaw("Horizontal");
        if (move > 0)
        {
            if(vel <maxVelocity)
            {
                forceX = speed;
            }
            if (!lookRight)
            {
                Flip();
            }
        }
        else if (move < 0)
        {
            if(vel < maxVelocity)
            {
                forceX = -speed;
            }
            if (lookRight)
            {
                Flip();
            }
        }
        myBody.AddForce(new Vector2(forceX, 0));
    }

    void GroundCheck()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, distance, m_WhatIsGround);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
            isGrounded = true;
        }
        else
        {
            rayColor = Color.red;
            isGrounded = false;
        }

        //Buat debug biar bisa liat collidernya
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + distance), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, 0), Vector2.down * (boxCollider.bounds.extents.y + distance), rayColor);
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + distance), Vector2.right * (boxCollider.bounds.extents.x * 2f), rayColor);
    }

    void Flip()
    {
        lookRight = !lookRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Bullet")) canJump = true;
    }
}
