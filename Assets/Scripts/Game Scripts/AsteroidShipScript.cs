using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidShipScript : MonoBehaviour {

    public bool isHit;          // Needs two hits to kill, this keeps track of the first
    public float speed;         // speed of craft
    public GameObject shot;     // bolt used
    public Transform shotSpawn; // where the bolt spawns
    public float fireRate;      // fires at certain time
    private float nextTime;     // decides the next time

    // Use this for initialization
    void Start()
    {
        transform.Rotate(Vector3.forward * 180); // fixes orientation
        this.isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //moves the ship forward
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, -10), speed * Time.deltaTime);

        // Decides other actions based on if it is hit
        if (isHit)
        {
            this.transform.Rotate(0, 0, 500 * Time.deltaTime);
        }
        else
        {
            FireBolt();
        }
    }

    void FireBolt()
    {
        //decides when to fire
        if (Time.time > nextTime)
        {
            nextTime = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }
}
