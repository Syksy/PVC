using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    // Unity animator
    private Animator anim;
    // Unity sprite renderer
    private SpriteRenderer spriteRenderer;
    // Cannonball to be shot from the cannon
    public GameObject CannonBall;
    // Cannon ball creation locations for both sides
    public Vector2 leftShot = new Vector2(-3, 0);
    public Vector2 rightShot = new Vector2(3, 0);
    // Initial forces upon cannon shot
    public Vector2 leftForce = new Vector2(-1000, 200);
    public Vector2 rightForce = new Vector2(1000, 200);
    // Add some spin to the cannonball
    public float leftSpin = 100.0f;
    public float rightSpin = 100.0f;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            // Start turning the cannon
            anim.SetTrigger("Turn");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // Attempt to shoot
            anim.SetTrigger("Shoot");
            // Shoot left
            if(spriteRenderer.sprite.name == "cannon1_left")
            {
                GameObject ball = GameObject.Instantiate(this.CannonBall);
                ball.transform.position = this.leftShot;
                Rigidbody2D rb2d = ball.GetComponent<Rigidbody2D>();
                rb2d.AddForce(this.leftForce);
                rb2d.AddTorque(this.leftSpin);
            }
            // Shoot right
            else if (spriteRenderer.sprite.name == "cannon1_right")
            {
                GameObject ball = GameObject.Instantiate(this.CannonBall);
                ball.transform.position = this.rightShot;
                Rigidbody2D rb2d = ball.GetComponent<Rigidbody2D>();
                rb2d.AddForce(this.rightForce);
                rb2d.AddTorque(this.rightSpin);
            }
        }
    }
}
