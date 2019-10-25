﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;

    public Text score;

    public Text winText;

    public Text livesText;

    private int scoreValue = 0;

    private int livesValue = 3;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    Animator anim;

    private bool facingRight;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        livesText.text =("Lives: " + livesValue.ToString());

        musicSource.clip = musicClipOne;
        musicSource.Play();

        anim = GetComponent<Animator>();

        facingRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rb2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        Flip(hozMovement);

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);

        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            
            anim.SetInteger("State", 0);

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);

        }

        if (Input.GetKeyUp(KeyCode.A))
        {

            anim.SetInteger("State", 0);

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetInteger("State", 2);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetInteger("State", 2);
            }

        }

        if (Input.GetKeyUp(KeyCode.W))
        {

            anim.SetInteger("State", 0);

        }



    }

    private void Flip(float hozMovement)
    {
        if (hozMovement > 0 && !facingRight || hozMovement < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
            {
                transform.position = new Vector2(25, 0);
                livesValue = 3;
                livesText.text = ("Lives: " + livesValue.ToString());
            }

            if (scoreValue == 8)
            {
                winText.text = "You win! Game created by Gary Carrasco";
                musicSource.clip = musicClipTwo;
                musicSource.PlayOneShot(musicClipTwo);
            }

        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            livesText.text = ("Lives: " + livesValue.ToString());
            Destroy(collision.collider.gameObject);

            if (livesValue == 0)
            {
                winText.text = "You Lose";
                Destroy(rb2d);
            }

        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

      

        if (collision.collider.tag == "Ground")
        {
            

            if (Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }

       
    }
}