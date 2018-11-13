using UnityEngine;
using System.Collections;

public class vrS : MonoBehaviour
{

    public static Transform VRHead;
    public static Transform HeadCenter;
    public static Transform VRHandRight;
    public static Transform VRHandLeft;
    public static Transform VRFootRight;
    public static Transform FootRight;
    public static Transform VRFootLeft;
    public static Transform FootLeft;
    public static Transform VRNeck;
    public static Transform VRShoulderLeft;
    public static Transform VRShoulderRight;

    public bool restrictXRot = false;
    public bool restrictYRot = false;
    public bool restrictZRot = false;

    // steamvr camera object to substitute middlevr 
    public SteamVR_Camera steamCam;
    public static Vector3 footrightPos;
    public static Vector3 footleftPos ;


    // Use this for initialization
    void Start()
    {
        //  steamCam = SteamVR_Render.Top();

        steamCam = FindObjectOfType<SteamVR_Camera>();
        // find head transform
      //  VRHead = GameObject.Find("VRHeadNode").transform;
   //     Was commented
        VRHead = steamCam.head.transform;

        Debug.Log("VR Head name,,,,,,,,,,,,,,,,,,,, "+VRHead.name.ToString());
        HeadCenter = VRHead.GetChild(0).transform;

             // find right hand tracker transform
             VRHandRight = GameObject.Find("VRHandRightNode").transform;
     //   VRHandRight = GetComponent<SteamVR_TrackedObject>().index==8;
             // find left hand tracker transform
             VRHandLeft = GameObject.Find("VRHandLeftNode").transform;
     
        // find right foot tracker transform
        VRFootRight = GameObject.Find("VRFootRightNode").transform;

        FootRight = VRFootRight.GetChild(0).transform;
    //    FootRight = rightFootTrackerPos.rightFootTransform;
        // find left foot tracker transform
        VRFootLeft = GameObject.Find("VRFootLeftNode").transform;
        FootLeft = VRFootLeft.GetChild(0).transform;
       // FootLeft= leftFootTrackerPos.leftFootTransForm;

        // find vrneck transform, no necktracker necessary ####################
        VRNeck = GameObject.Find("VRNeckNode").transform;


        // find left and right shoulder transform
        VRShoulderRight = GameObject.Find("VRShoulderRightNode").transform;
        VRShoulderLeft = GameObject.Find("VRShoulderLeftNode").transform;
    
    }

    // Update is called once per frame
    void Update()
    {

        // position and rotation of hmd
        footrightPos = rightFootTrackerPos.rightFootPos;
        footleftPos = leftFootTrackerPos.leftFootPos;
        //
     Debug.Log("HMD Transform Position: " + steamCam.head.position.ToString());
     Debug.Log("HMD Local Rotation: " + steamCam.head.localRotation.ToString());
   //     Debug.Log("left foot: (" + leftFootTrackerPos.leftFootPos.ToString());
   //     Debug.Log("right foot: (" + rightFootTrackerPos.rightFootPos.ToString());

    }
}