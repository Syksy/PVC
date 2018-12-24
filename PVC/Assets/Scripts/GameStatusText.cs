using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusText : MonoBehaviour {
    // Speed to shrink away
    public float shrinkSpeed = 1f;
    // Text mesh handler
    private TextMesh textMesh;

    void Start()
    {
        this.textMesh = this.gameObject.GetComponent<TextMesh>();
    }
    // Update is called once per frame
    void Update () {
        // Shrink and destroy the poof once it becomes small enough
        this.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
        if (this.transform.localScale.x < 0.1f)
            Destroy(gameObject);
    }

    // Set the text content of the newly created game update
    public void setText(string newText)
    {
        //this.textMesh.text = newText;
    }
}
