using UnityEngine;
using System.Collections;

public class RoomStateBase : MonoBehaviour {

	public virtual Animator CurrentAnimator { get; set; }
	public virtual Room CurrentRoom { get; set; } 
	public virtual bool Enabled { get; set; } 
	public GameObject Warning { get { return warning; } } 
	private GameObject warning;
	

	// Use this for initialization
	void Awake () {
		CurrentRoom = GetComponentInParent<Room>();
		CurrentAnimator = gameObject.GetComponent<Animator>();
		Enabled = false;
		Warning warnSign = GetComponentInChildren<Warning>();
		if (warnSign != null) 
		{
			warning = warnSign.gameObject;
			warning.renderer.enabled = false;
		}
	}

	public virtual bool EnableCondition () { return false; } 

	public virtual bool DisableCondition () { return true; } 

	public virtual void StateEnable () 
	{
		Enabled = true;
		if (warning != null) warning.renderer.enabled = true;
		Broadcaster.Instance.tickEvent += Tick;
	} 

	public virtual void StateDisable () 
	{ 
		Enabled = false;
		if (warning != null) warning.renderer.enabled = false;
		Broadcaster.Instance.tickEvent -= Tick;
	}

	public virtual bool AutoEnable () 
	{ 
		bool result = !Enabled && EnableCondition();
		if (result) StateEnable ();
		return result;
	} 

	public virtual bool InitiatedEnable (float amount) { return false; } 

	public virtual void Tick () 
	{
		if (DisableCondition ()) 
		{
			StateDisable (); 
			return;
		}
	}

}
