using UnityEngine;
using System.Collections;

public class EnemyShipScript : MonoBehaviour {

    public float speed;         // speed of ship 
    public GameObject shot;     // the bolt used to instantiate
    public Transform shotSpawn; // where the bolt will spawn
    public float fireRate;      // the rate at which the bolt is fired
    private float nextTime;     // variable to keep track of time

    // Use this for initialization
    void Start () {
        transform.Rotate(Vector3.forward * 180); // Fix orientation so it looks correct
    }
	
	// Update is called once per frame
	void Update () {

        // go to the end of the "game board"
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, -10), speed * Time.deltaTime);

        
        // decide when to shoot bolt
        if (Time.time > nextTime)
        {
            nextTime = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }

    }


}
