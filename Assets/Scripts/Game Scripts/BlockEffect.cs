using UnityEngine;
using System.Collections;

public class BlockEffect : MonoBehaviour {

    public GameObject[] drops;  // keeps track of all possible drops
    public Transform starSpawn; // figures out where the star spawns

    // Detects if a collider comes near it
    void OnTriggerEnter2D(Collider2D other)
    {
        // decided what, if anything, to spawn
        if (other.gameObject.tag == "Bolt")
        {
            int i = Random.Range(0, drops.Length);
            Instantiate(drops[i], this.transform.position, this.transform.rotation);
        }
    }
}
