using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [Range(-200f, 200f)]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float secondsTrigger;
    [SerializeField] Vector3 playerVel;
    Rigidbody2D rb;

    public float Speed { get => speed; set => speed = value; }
    public Vector3 PlayerVel1 { get => playerVel; set => playerVel = value; }

    void Start()
    {
        rb= gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // la velocidad no siempre es 0.
        playerVel = Vector3.right * speed;
        playerVel.y = gameObject.GetComponent<Rigidbody2D>().velocity.y;

        gameObject.GetComponent<Rigidbody2D>().velocity = playerVel;

        if (playerVel.x >= 3 || playerVel.x <= -3)
        {

            if (playerVel.x < 0)
            {
                gameObject.GetComponent<Animator>().SetBool("running", true);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;

            }
            else if (playerVel.x > 0)
            {

                gameObject.GetComponent<Animator>().SetBool("running", true);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else if (playerVel.x <= 3 || playerVel.x >= -3)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            gameObject.GetComponent<Animator>().SetBool("running", false);
        }



    }
    IEnumerator deactivateTriggerFor(float seconds)
    {

        // considerar que el personaje pueda apretarlo en el aire,  no bajara ya que es 1 segundo.
        gameObject.GetComponent<BoxCollider2D>().isTrigger= true;
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        yield return null;
    }
    public void ChangeSpeed(string[] signal)
    {
        if (signal.Length ==1)
        {
            if (signal[0] == "up")
            {
                
                rb.velocity = Vector2.up*jumpForce;
            }
            else if (signal[0] == "down")
            {
                rb.velocity = Vector2.down*100;
                IEnumerator trigger=deactivateTriggerFor(secondsTrigger);
                StartCoroutine(trigger);    
            }
            else if (signal[0] == "right")
            {
                if(speed<=0)
                {
                    speed *=-1;

                }
            }
            else if (signal[0] == "left")
            {
                if(speed>=0) {
                
                    speed *=-1;
                }
            }
        }

        else if (signal.Length > 1)
        {


        }
    }
}

