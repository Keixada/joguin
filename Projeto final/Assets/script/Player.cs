using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Speed;
    Rigidbody2D rb;
    public float Jumpforce;
    public bool isJumping;
    private Animator anime;
    private SpriteRenderer sprite;
    private bool isWalking;

    // vida
    public int playerHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Debug.Log("Life do Player: " + playerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();



    }



    void Move()
    {
        Vector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += moviment * Time.deltaTime * Speed;

        

    }


    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            anime.SetBool("pulo", true);
            rb.AddForce(new Vector2(0, Jumpforce), ForceMode2D.Impulse);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            anime.SetBool("walk", true);
            sprite.flipX = false;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            anime.SetBool("walk", true);
           // transform.eulerAngles = new Vector3(0, 180, 0);
            sprite.flipX = true;
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            anime.SetBool("walk", false);
            //transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            anime.SetBool("pulo", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    public void TakeDamage( int damage)
    {
        playerHealth -= damage;
        Debug.Log("Player tomou " + damage  + " de. Saúde restante: " + playerHealth);
        if (playerHealth<= 0)
        {
            Debug.Log("Player Morreu!");
           // Geme Over
        }
    }

}
