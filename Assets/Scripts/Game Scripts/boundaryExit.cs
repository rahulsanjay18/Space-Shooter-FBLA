using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaryExit : MonoBehaviour {

    private GameControllerScript gameController; // Object needed to carry out overall game tasks

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();
    }
	
    // detects if an object leaves it
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Contains("Bolt"))
        {
            Destroy(other.gameObject);
        }else
        {
            // loses earth health
            gameController.boundaryHit();
        }
    }

}
