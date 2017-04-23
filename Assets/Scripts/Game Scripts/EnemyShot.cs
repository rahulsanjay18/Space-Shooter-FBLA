using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour {

    public float speed; // sets the speed at which the bolt travels 
    public float fireRate; // ensures that one bolt from the enemy is there at once

    // Use this for initialization
    void Start()
    {
        // Sets initial speed
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;

        // Destroys bolt so as to not clog memory
        Destroy(this.gameObject, fireRate);
    }

}
