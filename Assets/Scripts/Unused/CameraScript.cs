using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject player;   // knows what object to keep track of
    private Vector3 offset;     // the offset at which it needs to be kept track of

	// Use this for initialization
	void Start () {
       offset = this.transform.position - player.transform.position; // used to keep track of player
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(0, player.transform.position.y, 0) + this.offset; // follows player
	}
}
