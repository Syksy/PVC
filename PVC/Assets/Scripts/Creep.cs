using System;
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
    // When dying or stealing, time until destroy and spin speed
    public float untilDisappear = 1.0f;
    public float spinSpeed = 1000f;

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
            this.transform.Rotate(0, 0, this.spinSpeed * Time.deltaTime * this.moveVector.x);
            // Shrink and destroy the poof once it becomes small enough
            this.transform.localScale -= Vector3.one * Time.deltaTime * 1.0f;
            if (this.transform.localScale.x < 0.1f)
                Destroy(gameObject);
        }
	}

    // Collide trigger with cannonball - die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Creep triggered with: " + collision.name);
        // Die if collided with a cannonball
        if (collision.name == "CannonballPrefab(Clone)")
        {
            this.isAlive = false;
            anim.SetTrigger("CreepDie");
            GameObject Counter = GameObject.Find("Counter");
            Counter.GetComponent<TextMesh>().text = Convert.ToString(Convert.ToInt16(Counter.GetComponent<TextMesh>().text) + 1);
            this.spinSpeed = 1000f;
            this.untilDisappear = 1f;
        }
        // Steal unicorn horn if collided with a unicorn
        else if ((collision.name == "Unicorn" | collision.name == "Unicorn(Clone)") & collision.GetComponent<SpriteRenderer>().sprite.name == "unicorn_with_horn")
        {
            this.isAlive = false;
            anim.SetTrigger("CreepSteal");
            this.spinSpeed = 200f;
            this.untilDisappear = 2f;
        }
    }
}
