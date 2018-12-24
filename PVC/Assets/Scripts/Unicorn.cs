using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicorn : MonoBehaviour {
    // Unity animator
    private Animator anim;
    // Unity SpriteRenderer
    //private SpriteRenderer spriteRenderer;

    private void Start()
    {
        this.anim = GetComponent<Animator>();
        //this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Collide
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Unicorn triggered with: " + collision.name);
        //if(collision.name == "CreepPrefab(Clone)" & spriteRenderer.sprite.name == "Unicorn_with_horn")
        if (collision.name == "CreepPrefab(Clone)")
        {
            this.anim.SetTrigger("UnicornSteal");
        }
    }
}
