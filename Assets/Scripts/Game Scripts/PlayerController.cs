using UnityEngine;
using System.Collections;

[System.Serializable]

// used to determine boundaries
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    public Boundary boundary;                   // boundary the player adheres to
    public float speed;                         // the speed of the craft
    public GameObject shot;                     // the object used to shoot
    private GameControllerScript gameController;// needed to add star score
    public Transform shotSpawn;                 // where the shot spawns
    public float fireRate;                      // the rate of firing
    private float nextTime;                     // the next time it can fire
    public float rotSpeed;                      // rotational speed
    private const int STAR_SCORE = 500;         // score player gets when in contact with a star

	// Use this for initialization
	void Start () {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
    }
	
	// FixedUpdate is called once per frame at regular time intervals
	void FixedUpdate () {

        // get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        // set vector for direction to travel
        Vector2 move = new Vector2(moveHorizontal, moveVertical);

        // set velocity
        GetComponent<Rigidbody2D>().velocity = move * (speed);
        // clamps player to those boundaries
        GetComponent<Rigidbody2D>().position = new Vector2(

            Mathf.Clamp(GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)
 
            );

        // allows for rotation
        if (Input.GetKey(KeyCode.Z))
            transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.X))
            transform.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime);

    }

    void Update()
    {
        // allows and decides when to shoot
        if ((Input.GetKey("space")) && Time.time > nextTime)
        {
            nextTime = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }

    // Detects if certain objects have entered it and their reactions
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Star")
        {
            gameController.addScore(STAR_SCORE);
            Destroy(other.gameObject);
        }
        if (other.tag == "Enemy")
        {
            this.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
        if(other.tag == "Asteroid")
        {
            this.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
        if (other.tag == "EnemyBolt")
        {
            this.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
