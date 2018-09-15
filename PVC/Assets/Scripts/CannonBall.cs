using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
    // If ball hits a creep, spawn a smokey explosion etc
    public GameObject explosion;
    // Destroy cannonballs if they fly too long (i.e. away from game area)
    public float untilDisappear = 10.0f;

    private void Update()
    {
        // Destroy cannonballs that fly in air too long
        untilDisappear -= Time.deltaTime;
        if (untilDisappear < 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject expl = GameObject.Instantiate(explosion);
        //expl.transform.position = collision.transform.position;
        expl.transform.position = this.transform.position;
        Destroy(gameObject);
    }
}
