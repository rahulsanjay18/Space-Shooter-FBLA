using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {
    
    public int speed; // speed of asteroid

	// Update is called once per frame
	void FixedUpdate () {

        // moves asteroid
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, -10), speed * Time.deltaTime);

        // destroys if it reaches a certain point
        if (transform.position.y <= -10)
        {
           Destroy(this.gameObject);
        }
    }
}
