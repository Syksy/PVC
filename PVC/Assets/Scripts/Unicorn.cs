using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicorn : MonoBehaviour {

    // Collide with an unicorn - steal horn
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger collision!");
    }
}
