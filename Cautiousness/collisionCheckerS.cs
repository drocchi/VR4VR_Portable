using UnityEngine;
using System.Collections;

public class collisionCheckerS : MonoBehaviour {

	public trafficManagerS trafficMan;
	public AudioClip[] hitSounds;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer != 9 && other.gameObject.layer != 10 && other.gameObject.layer != 13)
		{
			if(other.gameObject.name == "Target")
			{
				trafficMan.TargetReached();
				Destroy(other.gameObject);
			}
			else
			{
				string info = "Stable object hit! ";
				if (other.transform.parent.gameObject.name == "Car Instance")
				{
					GetComponent<AudioSource>().PlayOneShot(hitSounds[0]);
					info = "Car hit! ";
				}
				if (other.gameObject.name == "Pedestrain Instance")
				{
					GetComponent<AudioSource>().PlayOneShot(hitSounds[1]);
					info = "Pedestrain hit! ";
				}
				if (other.gameObject.name == "HornRange")
				{
					GetComponent<AudioSource>().PlayOneShot(hitSounds[2]);
					info = "Horn range hit! ";
				}
                if (other.gameObject.tag == "ground")
                {
                    Debug.Log("No collision yet");
                }
                else
                {
                    info += "Collision at: " + transform.position.ToString();
                    Debug.Log("Collision at: " + transform.position.ToString());
                    saverS.SaveText(info);
                    trafficMan.Failed();
                }
            }
		}
	}
}
