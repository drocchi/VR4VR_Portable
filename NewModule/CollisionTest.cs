using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionTest : MonoBehaviour {

    public SteamVR_TrackedController trackedController;
    public GameObject colTarget;
    private bool colliding = false;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Cube" || col.gameObject.name == "Cube1" || col.gameObject.name == "Cube2" || col.gameObject.name == "Cube3")    //Checks to see if hand is hovering over an answer choice
        {
            colliding = true;
        }
    }

    void OnCollisionExit(Collision col) 
    {
        colliding = false;      
    }

    void Update()
    {
        if (trackedController.triggerPressed == true && colliding == true)      //If trigger is pressed and hand is hovering over option Destroy the objects
        {
            Destroy(colTarget);
        }
    }
}
    