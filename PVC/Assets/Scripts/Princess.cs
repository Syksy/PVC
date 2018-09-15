using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour {
    // Unity SpriteRenderer
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer parentSpriteRenderer;
    public string parentsprite;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentSpriteRenderer = gameObject.transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        parentsprite = parentSpriteRenderer.sprite.name;
        // Turn princess together with the cannon
        if (parentSpriteRenderer.sprite.name == "cannon1_left")
        {
            this.spriteRenderer.flipX = true;
        }
        else if (parentSpriteRenderer.sprite.name == "cannon1_right")
        {
            this.spriteRenderer.flipX = false;
        }
    }
}
