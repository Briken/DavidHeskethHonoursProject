using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {

    public int playerNum;
    public GameObject leftPlayer;
    public GameObject rightPlayer;

    Transform opponent;
    Rigidbody2D rbdy;
    Animator animControl;

    public int jumpGravScale;
    public int groundGravScale;

    public float jumpFrc;
    public float jumpDur;
    float horizontal;
    float vertical;
    float speed;
    float privateFrc;
    float privateDur;

    Vector3 movement;

    bool onGround;
    bool jumpPressed;
    bool falling;
    // Use this for initialization
    void Start() {
        rbdy = GetComponent<Rigidbody2D>();
        animControl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal" + playerNum.ToString());
        vertical = Input.GetAxis("Vecrtical" + playerNum.ToString());

        Vector3 movement = new Vector3(horizontal, 0, 0);

        rbdy.AddForce(movement * speed);

        if (vertical > 0.1f)
        {
            if (!jumpPressed)
            {
                privateFrc += Time.deltaTime;
                privateDur += Time.deltaTime;
                if (privateDur < jumpDur)
                {
                    rbdy.velocity = new Vector2(rbdy.velocity.x, jumpFrc);
                }
                else
                {
                    jumpPressed = true;
                }
            }
        }
        if (!onGround && vertical < 0.1f)
        {
            falling = true;

        }

    }

    void UpdateAnimationState()
    {
        animControl.SetBool("OnGround", onGround);
        animControl.SetBool("Falling", falling);
        animControl.SetFloat("Movement", Mathf.Abs(horizontal));

    }

    void GroundGravCheck()
        {
        if (!onGround)
        {
            rbdy.gravityScale = jumpGravScale;
        }
        else
        {
            rbdy.gravityScale = groundGravScale;
        }
        }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            falling = false;
            jumpPressed = false;
            privateDur = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }

}
