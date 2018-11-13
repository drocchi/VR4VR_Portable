using UnityEngine;
using System.Collections;

public class cameraPosS : MonoBehaviour
{
    public Camera[] cameras;
    public SteamVR_Camera steamCam;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //	foreach (Camera camera in cameras) {
        steamCam.transform.position = transform.position;
        steamCam.transform.rotation = transform.rotation;


    }
}