using UnityEngine;
using System.Collections;

public class RoomStateBase : MonoBehaviour {

	public virtual Animator CurrentAnimator { get; set; }
	public virtual Room CurrentRoom { get; set; } 
	public virtual bool Enabled { get; set; } 

	// Use this for initialization
	void Start () {
		CurrentRoom = GetComponentInParent<Room>();
		CurrentAnimator = gameObject.GetComponent<Animator>();
		Enabled = false;
	}

	public virtual bool EnableCondition () { return false; } 

	public virtual bool DisableCondition () { return true; } 

	public virtual void StateEnable () 
	{
		Enabled = true;
		Broadcaster.Instance.tickEvent += Tick;
	} 

	public virtual void StateDisable () 
	{ 
		Enabled = false;
		Broadcaster.Instance.tickEvent -= Tick;
	}

	public virtual bool AutoEnable () { return false; } 

	public virtual bool InitiatedEnable (float amount) { return false; } 

	public virtual void Tick () { }

}
