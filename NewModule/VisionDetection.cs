using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetection : MonoBehaviour {

    public Camera cam;
    private void Start()
    {

    }

    void Update () {
        RaycastHit hit;

        Ray ray = cam.ScreenPointToRay(Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
	}
}
