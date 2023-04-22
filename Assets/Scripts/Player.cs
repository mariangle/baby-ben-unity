using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject losePanel;
    public Text healthDisplay;

    public float speed;
    private float input;

    Rigidbody2D rb;
    Animator anim;
    AudioSource source;

    public int health;

    public float startDashTime;
    private float dashTime;
    public float extraSpeed;
    private bool isDashing;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthDisplay.text = health.ToString();
    }

    private void Update()
    {
        if (input != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (input < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (input > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false)
        {
            speed += extraSpeed;
            isDashing= true;
            dashTime = startDashTime;
        }

        if (dashTime <= 0 && isDashing == true)
        {
            isDashing= false;
            speed -= extraSpeed;
        } else
        {
            dashTime -= Time.deltaTime; 
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Storing player's input
        input = Input.GetAxisRaw("Horizontal");
        print(input);

        // Moving player
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }

    public void TakeDamage(int damangeAmount)
    {
        source.Play();
        health -= damangeAmount;
        healthDisplay.text = health.ToString();

        if (health <= 0)
        {
            losePanel.SetActive(true);
            Destroy(gameObject);
        }
    }
}
