using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIspawn : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPosition;
    public bool spwan;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            if (spwan == true)// Spawn object at spawnPosition
            {
                GameObject obj = Instantiate(objectToSpawn, spawnPosition.position, Quaternion.identity);
                // Do something with obj, for example:
                /* obj.SetActive(false); */// Hide object initially
                spwan = false;
                Debug.Log("coliided");
            }
        }
    }
}
