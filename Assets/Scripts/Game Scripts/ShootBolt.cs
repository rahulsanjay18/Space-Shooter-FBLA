using UnityEngine;
using System.Collections;

public class ShootBolt : MonoBehaviour {

    public float speed;                             // speed of bolt
    public float fireRate;                          // rate of fire
    private GameControllerScript gameController;    // controller so scores can be added

    // maximum scores of different things that can be shot at
    private const int ASTEROID_SCORE = 100;
    private const int ENEMY_SHIP_SCORE = 250;
    private const int ASTEROID_SHIP_SCORE = 500;

    // chance at getting a box
    private const int CHANCE_TO_DROP_BOX = 10;
    public GameObject questionBox;

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        Destroy(this.gameObject, fireRate);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        gameController = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();

        // The y position at which the enemy was hit is taken which acts as a multiplier to the max scores
        // The goal is to keep enemies as far away as possible
        float mutlt = (this.gameObject.transform.position.y + 2) / 21;

        // if the bolt collides with something it will do a different action
        if (other.tag == "Block")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.tag == "Enemy")
        {
            gameController.addScore((int)(ENEMY_SHIP_SCORE * mutlt));
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            DropBlock();
        }

        if (other.tag == "Asteroid")
        {
            gameController.addScore((int)(ASTEROID_SCORE * mutlt));
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            DropBlock();
        }

        if (other.tag == "AstEnemy")
        {
            // makes it so that the asteroid ship must be hit twice to destroy
            if (other.GetComponent<AsteroidShipScript>().isHit)
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
                gameController.addScore((int)(ASTEROID_SHIP_SCORE * mutlt));
                DropBlock();
            }
            else
            {
                other.GetComponent<AsteroidShipScript>().isHit = true;
                speed = speed * 3;
                Destroy(this.gameObject);
            }

        }
    }

    // determine if a block is dropped to be broken to reveal a prize or an asteroid
    void DropBlock()
    {
        int i = Random.Range(0, CHANCE_TO_DROP_BOX);
        if(i == 5)
        {
            Instantiate(questionBox, this.transform.position, this.transform.rotation);
        }
    }
    
}
