using UnityEngine;
using System.Collections;

public class moppingAnimationS : MonoBehaviour, AnimatableS {

	private Animator animator;	
	private AnimatorStateInfo currentState;
		
	//static int idleState = Animator.StringToHash("Base Layer.Idle");
	static int taklingState = Animator.StringToHash("Base Layer.Talking");
	static int moppingState = Animator.StringToHash("Base Layer.Mopping");	
	
	public GameObject wetDirtObj;
	public Transform dirtPos;
	
	private GameObject dirtInstance;
	
	private int state = 0;
	private bool animating = false;
	
	public AudioClip[] dubs;

	private bool bypass = false;

	// Use this for initialization
	void Awake () 
	{
		dirtInstance = Instantiate(wetDirtObj, dirtPos.position, Quaternion.identity) as GameObject;
		dirtInstance.transform.parent = transform.parent;
		
		animator = GetComponent<Animator>();
		animating = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			bypass = true;
			GetComponent<AudioSource>().Stop();
		}
	}

	public void PlayAnimation()
	{
		animator.SetInteger("MoppingAnimationState", 1);
		GetComponent<AudioSource>().clip = dubs[0];
		GetComponent<AudioSource>().Play();
		animating = true;
	}
	
	public void PlayTalk(int _index)
	{
		animator.SetInteger("MoppingAnimationState", 1);
		GetComponent<AudioSource>().clip = dubs[_index];
		GetComponent<AudioSource>().Play();			
		state = 3;
		animating = true;
	}
	
	public bool IsPlaying()
	{
		return animating;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		currentState = animator.GetCurrentAnimatorStateInfo(0);
		
		switch(state)
		{
		case 0://talking
				if (!GetComponent<AudioSource>().isPlaying || bypass) 
			{
				animator.SetInteger("MoppingAnimationState", 2);
				state = 1;
			}
			break;
		case 1://mopping before destroying dirt
			if ((currentState.nameHash == moppingState && currentState.normalizedTime > 0.8f) || bypass) 
			{
				Destroy(dirtInstance);
				animator.SetInteger("MoppingAnimationState", 1);
				state = 2;
			}
			break;	
		case 2://mopping before destroying dirt
			if ((!animator.IsInTransition(0) && currentState.nameHash == taklingState) || bypass)
			{
				GetComponent<AudioSource>().clip = dubs[1];
				GetComponent<AudioSource>().Play();		
				state = 3;
			}
			break;				
		case 3://talking
			if (!GetComponent<AudioSource>().isPlaying || bypass) 
			{
				animator.SetInteger("MoppingAnimationState", 0);
				state = 4;
			}
			break;		
		case 4://idle
			animating = false;
			break;				
		}
		bypass = false;
	}
}
