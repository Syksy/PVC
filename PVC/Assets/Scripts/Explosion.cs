using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public float untilDisappear = 0.5f;
	
	// Update is called once per frame
	void Update () {
        // Countdown to destroy self
        untilDisappear -= Time.deltaTime;
        // If explosion poof has existed for long enough destroy it
        if (untilDisappear < 0)
            Destroy(gameObject);
        // Rotate the smoke poof while shrinking
        this.transform.Rotate(0, 0, 300*Mathf.Sin(Time.deltaTime));
        // Shrink and destroy the poof once it becomes small enough
        this.transform.localScale -= Vector3.one * Time.deltaTime * 1.2f;
        if (this.transform.localScale.x < 0.1f)
            Destroy(gameObject);
    }
}
