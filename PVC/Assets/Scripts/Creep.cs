using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour {
    // Unity animator
    private Animator anim;
    // Unity SpriteRenderer
    private SpriteRenderer spriteRenderer;
    // Creeps move either left or right; if false, Creep is moving to right
    private bool isAlive = true;
    // Direction for movement
    private Vector3 moveVector;
    // When dying, time until destroy
    public float untilDisappear = 1.0f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // If Creep spawns left from the origin its direction is reversed and it instead walks right
        if (this.transform.position.x < 0)
        {
            this.moveVector = new Vector3(1, 0, 0);
            this.spriteRenderer.flipX = true;
        }
        // Normally Creeps walk left
        else
        {
            this.moveVector = new Vector3(-1, 0, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        // If Creep is alive it moves (scaled by deltaTime)
        if (isAlive)
        {
            this.transform.Translate(this.moveVector*Time.deltaTime);
        }
        else
        {
            // Countdown to destroy self
            untilDisappear -= Time.deltaTime;
            // If explosion poof has existed for long enough destroy it
            if (untilDisappear < 0)
                Destroy(gameObject);
            // Rotate the smoke poof while shrinking (rotation direction depends on which way Creep was moving)
            this.transform.Rotate(0, 0, 1000 * Time.deltaTime * this.moveVector.x);
            // Shrink and destroy the poof once it becomes small enough
            this.transform.localScale -= Vector3.one * Time.deltaTime * 1.0f;
            if (this.transform.localScale.x < 0.1f)
                Destroy(gameObject);
        }
	}

    // Collide with cannonball - die
    void OnCollisionEnter2D(Collision2D collision)
    {

            this.isAlive = false;
            anim.SetTrigger("CreepDie");
    }
}
